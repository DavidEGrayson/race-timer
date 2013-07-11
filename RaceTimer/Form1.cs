using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaceTimerApp
{
    public partial class Form1 : Form
    {
        RaceTimer raceTimer;
        System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();

            raceTimer = new RaceTimer(participantCount: 2, lapCount: 3);
            raceTimer.modelUpdated += modelUpdated;

            simulateSensorAToolStripMenuItem.Tag = 0;
            simulateSensorBToolStripMenuItem.Tag = 1;

            timer = new Timer();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            timer.Interval = 40;
            timer.Start();
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            updateParticipantTickingTimes(0, participantControlA);
            updateParticipantTickingTimes(1, participantControlB);
        }


        void modelUpdated(object sender, EventArgs e)
        {
            updateParticipant(0, participantControlA);
            updateParticipant(1, participantControlB);
        }

        void updateParticipant(int participantIndex, ParticipantControl control)
        {
            Participant participant = raceTimer.getParticipant(participantIndex);
            control.lapList.Items.Clear();
            var items = new List<ListViewItem>();
            var laps = participant.getLaps();
            for(int i = 0; i < laps.Count; i++)
            {
                Lap lap = laps[i];

                UInt32 time = lap.timeMs;

                var item = new ListViewItem(new string[] { (i + 1).ToString(), formatTime(time) });
                items.Add(item);
            }
            control.lapList.Items.AddRange(items.ToArray());
        }

        void updateParticipantTickingTimes(int participantIndex, ParticipantControl control)
        {
            Participant participant = raceTimer.getParticipant(participantIndex);
            UInt32? t = participant.totalTime();

            if (t == null)
            {
                control.totalTimeBox.Text = "";
            }
            else
            {
                control.totalTimeBox.Text = formatTime(t.Value);
            }

        }

        string formatTime(UInt32 timeMs)
        {
            uint minutes = timeMs / 1000 / 60;
            uint seconds = timeMs / 1000 % 60;
            uint millis = timeMs % 1000;
            return String.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, millis);
        }

        void simulateSensorMenuItem_Click(object sender, EventArgs e)
        {
            raceTimer.simulateSensor(determineParticipantIndex(sender));
        }

        int determineParticipantIndex(object o)
        {
            var t = (ToolStripItem)o;
            return (int)t.Tag;
        }

    }
}
