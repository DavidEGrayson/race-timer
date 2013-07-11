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

        SerialPort port;
        Thread readThread;
        bool continueReading;
        Queue<string> portLineQueue;
        static Regex messageRegex = new Regex(@"\A([ab]),([0-9A-Fa-f]{1,8})\Z");        

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

        public void simulateSensor(int participantIndex)
        {
            recordTime(participantIndex, timeEstimate);
        }

        public void recordTime(int participantIndex, UInt32 time)
        {
            participants[participantIndex].sensorTimes.Add(time);
            notifyModelUpdated();
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

        public void setPort(string portName)
        {
            if (port != null)
            {
                continueReading = true;
                readThread.Join();
                port.Close();
                port = null;
            }

            if (portName == null)
            {
                return;
            }

            port = new SerialPort(portName);
            port.ReadTimeout = 200;
            port.WriteTimeout = 200;
            port.Open();
            continueReading = true;
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

        public void handleSerialPortMessages()
        {
            while (portLineQueue.Count != 0)
            {
                string line = portLineQueue.Dequeue();
                handleSerialPortLine(line);
            }
        }

        void handleSerialPortLine(string line)
        {
            // TODO: log the line to our log file

            Match match = messageRegex.Match(line);
            if (match == null || match.Captures.Count != 2)
            {
                // Not recognized!
            }
            else
            {
                string participantName = match.Captures[0].Value;
                string hexTime = match.Captures[0].Value;
                int participantIndex;
                if (participantName == "a")
                {
                    participantIndex = 0;
                }
                else if (participantName == "b")
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
                list.Add(new Lap(sensorTimes[i] - sensorTimes[i - 1]));
            }

            if (i <= raceTimer.lapCount)
            {
                // Add the unfinished lap.
                list.Add(new Lap());
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

        public uint? totalTime()
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

    class Lap
    {
        public bool finished = false;
        public UInt32 timeMs = 0;

        public Lap(UInt32 timeMs)
        {
            this.timeMs = timeMs;
            finished = true;
        }

        public Lap()
        {
        }
    }
}
