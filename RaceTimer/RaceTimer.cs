﻿using System;
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

        bool allTimesAreGuesses = true;

        public delegate UInt32 TimeAdjuster(UInt32 time);

        public TimeAdjuster timeAdjuster = t => t;

        public uint raceCount = 1;

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

        public static string formatTime(UInt32 timeMs)
        {
            uint minutes = timeMs / 1000 / 60;
            uint seconds = timeMs / 1000 % 60;
            uint millis = timeMs % 1000;
            return String.Format("{0}:{1:00}.{2:000}", minutes, seconds, millis);
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
            bool wasFinished = participant.finished;
            if (!wasFinished)
            {
                recorded = true;
                participant.sensorTimes.Add(time);
                notifyModelUpdated();
            }
            logInfo(String.Format("Participant {0} sensed at time {1}{2}", participantIndex, time, recorded ? "" : " (not recorded!)"));

            if (!wasFinished && participant.finished)
            {
                var laps = participant.getLaps();
                // This participant just finished!
                logInfo(String.Format("Participant {0} finished!  Name: {1}.  Laps: {2}.  Total: {3}.",
                    participantIndex, participant.name,
                    String.Join(", ", from lap in laps select formatTime(lap.totalTimeMsAdjusted)),
                    formatTime(participant.totalTimeAdjusted.Value)
                    ));
            }
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

            if (allTimesAreGuesses)
            {
                bool adjustedTimes = false;
                foreach (Participant p in participants)
                {
                    if (p.sensorTimes.Count > 0)
                    {
                        adjustedTimes = true;
                        p.sensorTimes = (from time in p.sensorTimes select time + stopWatchOffset).ToList();
                    }
                }

                if (adjustedTimes)
                {
                    logInfo(String.Format("Added {0} to all previous times.", stopWatchOffset));
                }

                allTimesAreGuesses = false;
            }
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
                updateTimeEstimate(timeMs);  // this must be done BEFORE because it might alter time measurements
                recordTime(participantIndex, timeMs);
            }
        }

        public void newRace()
        {
            foreach (Participant participant in participants)
            {
                participant.sensorTimes.Clear();
            }

            raceCount += 1;
            notifyModelUpdated();

            logInfo(String.Format("Race {0} started at {1:HH:MM:ss}.", raceCount, DateTime.Now));
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
                log.WriteLine(logPrefix() + line);
            }
        }

        string logPrefix()
        {
            return "Race " + raceCount + ": ";
        }


        public void resetParticipant(int participantIndex)
        {
            Participant participant = participants[participantIndex];

            participant.sensorTimes.Clear();

            logInfo("Reset participant " + participantIndex);
            notifyModelUpdated();
        }
    }

    class Participant
    {
        public RaceTimer raceTimer;

        public List<UInt32> sensorTimes = new List<UInt32>();

        public String name;

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

        public UInt32? totalTimeAdjusted
        {
            get
            {
                if (!started) { return null; }

                if (finished)
                {
                    return raceTimer.timeAdjuster(sensorTimes[sensorTimes.Count - 1] - sensorTimes[0]);
                }
                else
                {
                    return raceTimer.timeEstimate - sensorTimes[0];
                }
            }
        }


        public UInt32 averageLapTimeAdjusted
        {
            get
            {
                if (sensorTimes.Count >= 2)
                {
                    return totalTimeAdjusted.Value / ((UInt32)sensorTimes.Count - 1);
                }
                else
                {
                    return 0;
                }
            }
        }

        public UInt32 bestLapTimeAdjusted
        {
            get
            {
                if (sensorTimes.Count >= 2)
                {
                    return (from l in getLaps() where l.finished select l.totalTimeMsAdjusted).Min();
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
        private readonly UInt32 totalTimeMs = 0;

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

        public UInt32 totalTimeMsAdjusted
        {
            get
            {
                return raceTimer.timeAdjuster(totalTimeMs);
            }
        }
    }
}
