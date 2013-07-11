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

        List<Participant> participants;

        public event EventHandler modelUpdated;

        int lapCount;

        public RaceTimer(int participantCount, int lapCount)
        {
            this.lapCount = lapCount;

            participants = new List<Participant>();
            for (int i = 0; i < participantCount; i++)
            {
                participants.Add(new Participant());
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
        public List<UInt32> sensorTimes = new List<UInt32>();
        public bool finished = false;
    }
}
