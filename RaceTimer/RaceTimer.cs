using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;

namespace RaceTimerApp
{
    class RaceTimer
    {
        /// <summary>
        /// Helps us estimate what the current time would be.
        /// </summary>
        Stopwatch stopWatch = new Stopwatch();

        /// <summary>
        /// This is a number we add to stopWatch.ElapsedMilliseconds to get an estimate of what the current time
        /// on the Wixel is.
        /// </summary>
        UInt32 stopWatchOffset = 0;

        public readonly List<Participant> participants;

        public event EventHandler modelUpdated;

        public readonly int lapCount;

        public System.IO.StreamWriter log;

        SerialPort port;
        Thread readThread;
        bool continueReading;
        Queue<string> portLineQueue;
        static Regex messageRegex = new Regex(@"\A([AB]),([0-9A-Fa-f]{1,8})\Z");        

        public RaceTimer(int participantCount, int lapCount)
        {
            this.lapCount = lapCount;

            participants = new List<Participant>();
            for (int i = 0; i < participantCount; i++)
            {
                var p = new Participant();
                p.raceTimer = this;
                participants.Add(p);
            }

            stopWatch.Start();

            portLineQueue = new Queue<string>(4);
        }

        public void simulateAllSensors()
        {
            UInt32 time = timeEstimate;
            for (int i = 0; i < participants.Count; i++)
            {
                recordTime(i, time);
            }
        }

        public void simulateSensor(int participantIndex)
        {
            recordTime(participantIndex, timeEstimate);
        }

        public void recordTime(int participantIndex, UInt32 time)
        {
            Participant participant = participants[participantIndex];
            bool recorded = false;
            if (!participant.finished)
            {
                recorded = true;
                participant.sensorTimes.Add(time);
                notifyModelUpdated();
            }
            logInfo(String.Format("Participant {0} sensed at time 0x{1:X}{2}", participantIndex, time, recorded ? "" : " (not recorded!)"));
        }

        private void notifyModelUpdated()
        {
            modelUpdated.Invoke(this, null);
        }

        public Participant getParticipant(int participantIndex)
        {
            return participants[participantIndex];
        }

        public void updateTimeEstimate(UInt32 actualTime)
        {
            stopWatchOffset = actualTime - (UInt32)stopWatch.ElapsedMilliseconds;
        }

        public UInt32 timeEstimate
        {
            get
            {
                 return (UInt32)stopWatch.ElapsedMilliseconds + stopWatchOffset;
            }
        }

        public string portName
        {
            get
            {
                return port == null ? null : port.PortName;
            }
        }

        public void setPort(string newPortName)
        {
            if (port != null)
            {
                continueReading = true;
                readThread.Join();
                port.Close();
                logInfo("Closed serial port " + port.PortName);
                port = null;
            }

            if (newPortName == null)
            {
                return;
            }

            port = new SerialPort(newPortName);
            port.ReadTimeout = 200;
            port.WriteTimeout = 200;
            port.Open();
            continueReading = true;
            readThread = new Thread(readPort);
            readThread.Start();
            logInfo("Opened serial port " + port.PortName);
        }

        void readPort()
        {
            while (continueReading)
            {
                try
                {
                    string line = port.ReadLine();
                    portLineQueue.Enqueue(line);
                }
                catch (TimeoutException) { }
            }
        }

        public void handleSerialPortLines()
        {
            while (portLineQueue.Count != 0)
            {
                string line = portLineQueue.Dequeue();
                handleSerialPortLine(line);
            }
        }

        public void handleSerialPortLine(string line)
        {
            logInfo("From serial: " + line);

            Match match = messageRegex.Match(line);
            if (match == null || match.Groups.Count != 3)
            {
                // Not recognized!
            }
            else
            {
                string participantName = match.Groups[1].Value;
                string hexTime = match.Groups[2].Value;
                int participantIndex;
                if (participantName == "A")
                {
                    participantIndex = 0;
                }
                else if (participantName == "B")
                {
                    participantIndex = 1;
                }
                else
                {
                    // This should NEVER happen because of the regex.
                    throw new FormatException("Unexpected error processing line from serial port: " + line);
                }

                UInt32 timeMs = Convert.ToUInt32(hexTime, 16);
                recordTime(participantIndex, timeMs);
                updateTimeEstimate(timeMs);
            }
        }

        public void newRace()
        {
            foreach (Participant participant in participants)
            {
                participant.sensorTimes.Clear();
            }

            logInfo("New Race");
            notifyModelUpdated();
        }


        public void startLogging(string filename)
        {
            log = new System.IO.StreamWriter(filename);
            log.AutoFlush = true;
            logInfo("Started logging on " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString());
        }

        public void logInfo(string line)
        {
            if (log != null)
            {
                log.WriteLine(line);
            }
        }

    }

    class Participant
    {
        public RaceTimer raceTimer;

        public List<UInt32> sensorTimes = new List<UInt32>();

        public List<Lap> getLaps()
        {
            var list = new List<Lap>();
            if (sensorTimes.Count == 0)
            {
                // No lap even started yet.
                return list;
            }

            int i;
            for (i = 1; i < sensorTimes.Count; i++)
            {
                list.Add(new Lap(raceTimer, sensorTimes[i - 1], sensorTimes[i] - sensorTimes[i - 1]));
            }

            if (i <= raceTimer.lapCount)
            {
                // Add the unfinished lap.
                list.Add(new Lap(raceTimer, sensorTimes[i - 1]));
            }

            return list;
        }

        public bool finished
        {
            get
            {
                return sensorTimes.Count > raceTimer.lapCount;
            }
        }

        public bool started
        {
            get
            {
                return sensorTimes.Count != 0;
            }
        }

        public UInt32? totalTime
        {
            get
            {
                if (!started) { return null; }

                if (finished)
                {
                    return sensorTimes[sensorTimes.Count - 1] - sensorTimes[0];
                }
                else
                {
                    return raceTimer.timeEstimate - sensorTimes[0];
                }
            }
        }


        public UInt32 averageLapTime
        {
            get
            {
                if (sensorTimes.Count >= 2)
                {
                    return totalTime.Value / ((UInt32)sensorTimes.Count - 1);
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    class Lap
    {
        public readonly bool finished = false;
        public readonly UInt32 totalTimeMs = 0;

        RaceTimer raceTimer;
        UInt32 startTime;

        public Lap(RaceTimer raceTimer, UInt32 startTime, UInt32 totalTimeMs)
        {
            finished = true;
            this.raceTimer = raceTimer;
            this.startTime = startTime;
            this.totalTimeMs = totalTimeMs;
        }

        public Lap(RaceTimer raceTimer, UInt32 startTime)
        {
            finished = false;
            this.raceTimer = raceTimer;
            this.startTime = startTime;
        }

        public UInt32 partialTimeMs()
        {
            return raceTimer.timeEstimate - startTime;
        }
    }
}
