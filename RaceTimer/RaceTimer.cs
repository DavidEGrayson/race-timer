using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
