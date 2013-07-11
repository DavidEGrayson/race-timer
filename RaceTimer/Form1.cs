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
        public Form1()
        {
            InitializeComponent();

            raceTimer = new RaceTimer(participantCount: 2, lapCount: 3);
            raceTimer.modelUpdated += modelUpdated;

            simulateSensorAToolStripMenuItem.Tag = 0;
            simulateSensorBToolStripMenuItem.Tag = 1;
        }

        RaceTimer raceTimer;

        private void containerPanel_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        void modelUpdated(object sender, EventArgs e)
        {
            updateParticipant(0, participantControlA);
            updateParticipant(1, participantControlB);
        }

        private void updateParticipant(int participantIndex, ParticipantControl control)
        {
            Participant participant = raceTimer.getParticipant(participantIndex);
            control.lapList.Items.Clear();
            var items = new List<ListViewItem>();
            var laps = participant.getLaps();
            for(int i = 0; i < laps.Count; i++)
            {
                Lap lap = laps[i];

                UInt32 time = lap.timeMs;

                var item = new ListViewItem(new string[] { (i + 1).ToString(), time.ToString() });
                items.Add(item);
            }
            control.lapList.Items.AddRange(items.ToArray());
        }


        private void simulateSensorMenuItem_Click(object sender, EventArgs e)
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
