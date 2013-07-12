using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace RaceTimerApp
{
    public partial class Form1 : Form
    {
        RaceTimer raceTimer;
        System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();
            Icon = RaceTimerApp.Properties.Resources.racecar_from_pifmgr28;

            raceTimer = new RaceTimer(participantCount: 2, lapCount: 3);
            raceTimer.modelUpdated += modelUpdated;

            // Account for the Wixel's clock which runs a bit slower than it really should.
            // (see https://github.com/pololu/wixel-sdk/blob/master/libraries/src/wixel/time.c#L16 )
            raceTimer.timeAdjuster = t => t * 376 / 375;

            simulateSensorAToolStripMenuItem.Tag = 0;
            simulateSensorBToolStripMenuItem.Tag = 1;
            resetAMenuItem.Tag = 0;
            resetBMenuItem.Tag = 1;

            timer = new Timer();

            participantControl0.nameBox.TextChanged += delegate(object s, EventArgs e) { raceTimer.participants[0].name = participantControl0.nameBox.Text; };
            participantControl1.nameBox.TextChanged += delegate(object s, EventArgs e) { raceTimer.participants[1].name = participantControl1.nameBox.Text; };
        }

        void Form1_Load(object sender, EventArgs e)
        {
            timer.Interval = 40;
            timer.Start();
            timer.Tick += timer_Tick;
        }

        void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            updateSerialPorts();

            startLoggingToolStripMenuItem.Enabled = (raceTimer.log == null);
        }

        void startLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (raceTimer.log != null)
            {
                MessageBox.Show("We are already logging.", "Already Logging", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.CheckFileExists = false;
            dialog.CreatePrompt = false;
            dialog.AddExtension = true;
            dialog.DefaultExt = "txt";
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            dialog.ShowDialog();
            if (dialog.FileName == "")
            {
                return;
            }

            raceTimer.startLogging(dialog.FileName);
        }

        void updateSerialPorts()
        {
            serialPortBox.Items.Clear();
            serialPortBox.Items.Add("Not connected");

            foreach (string portName in SerialPort.GetPortNames())
            {
                serialPortBox.Items.Add(portName);

                if (portName == raceTimer.portName)
                {
                    serialPortBox.SelectedIndex = serialPortBox.Items.Count - 1;
                }
            }

            if (raceTimer.portName == null)
            {
                serialPortBox.SelectedIndex = 0;
            }
            else if (!serialPortBox.Items.Contains(raceTimer.portName))
            {
                serialPortBox.Items.Add(raceTimer.portName + " (gone)");
                serialPortBox.SelectedIndex = serialPortBox.Items.Count - 1;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            raceTimer.handleSerialPortLines();
            updateParticipantTickingTimes();
        }

        void updateParticipantTickingTimes()
        {
            updateParticipantTickingTimes(0, participantControl0);
            updateParticipantTickingTimes(1, participantControl1);
        }


        void modelUpdated(object sender, EventArgs e)
        {
            updateParticipant(0, participantControl0);
            updateParticipant(1, participantControl1);

            if (raceTimer.participants.All(p => p.finished))
            {
                UInt32 time0 = raceTimer.participants[0].totalTimeAdjusted.Value;
                UInt32 time1 = raceTimer.participants[1].totalTimeAdjusted.Value;

                if (time0 < time1)
                {
                    styleWin(participantControl0);
                    styleLoss(participantControl1);
                }
                else if (time0 > time1)
                {
                    styleLoss(participantControl0);
                    styleWin(participantControl1);
                }
                else
                {
                    styleTie(participantControl0);
                    styleTie(participantControl1);
                }

                UInt32 bestLapTime0 = raceTimer.participants[0].bestLapTimeAdjusted;
                UInt32 bestLapTime1 = raceTimer.participants[1].bestLapTimeAdjusted;

                if (bestLapTime0 < bestLapTime1)
                {
                    styleBestLapTime(participantControl0);
                    styleNotBestLapTime(participantControl1);
                }
                else if (time0 > time1)
                {
                    styleNotBestLapTime(participantControl0);
                    styleBestLapTime(participantControl1);
                }
                else
                {
                    styleNotBestLapTime(participantControl0);
                    styleNotBestLapTime(participantControl1);
                }
            }
            else
            {
                styleNotBestLapTime(participantControl0);
                styleNotBestLapTime(participantControl1);
            }

            // Do this or else we see a slight flicker for an unfinished lap when we get a sensor
            // reading from the other participant.
            updateParticipantTickingTimes();
        }

        void styleUndecided(ParticipantControl control)
        {
            control.totalTimeBox.BackColor = SystemColors.Control;
        }

        void styleLoss(ParticipantControl control)
        {
            styleUndecided(control);
        }

        void styleWin(ParticipantControl control)
        {
            control.totalTimeBox.BackColor = Color.PaleGreen;
        }

        void styleTie(ParticipantControl control)
        {
            styleWin(control);
        }

        void styleBestLapTime(ParticipantControl control)
        {
            control.bestLapTimeBox.BackColor = Color.PaleGreen;
        }

        void styleNotBestLapTime(ParticipantControl control)
        {
            control.bestLapTimeBox.BackColor = SystemColors.Control;
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

                UInt32 time = lap.totalTimeMsAdjusted;

                var item = new ListViewItem(new string[] { (i + 1).ToString(), formatTime(time) });
                item.Tag = lap;
                items.Add(item);
            }
            control.lapList.Items.AddRange(items.ToArray());

            if (participant.finished)
            {
                control.bestLapTimeBox.Text = formatTime(participant.bestLapTimeAdjusted);
            }
            else
            {
                control.bestLapTimeBox.Text = "";
            }
        }

        void updateParticipantTickingTimes(int participantIndex, ParticipantControl control)
        {
            Participant participant = raceTimer.getParticipant(participantIndex);
            UInt32? t = participant.totalTimeAdjusted;

            if (t == null)
            {
                control.totalTimeBox.Text = "";
            }
            else
            {
                control.totalTimeBox.Text = formatTime(t.Value);
            }

            foreach (ListViewItem item in control.lapList.Items)
            {
                Lap lap = (Lap)item.Tag;
                if (!lap.finished)
                {
                    item.SubItems[1].Text = formatTime(lap.partialTimeMs());
                }
            }

        }

        string formatTime(UInt32 timeMs)
        {
            return RaceTimer.formatTime(timeMs);
        }

        void simulateSensorMenuItem_Click(object sender, EventArgs e)
        {
            raceTimer.simulateSensor(determineParticipantIndex(sender));
        }

        void simulateAllSensorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            raceTimer.simulateAllSensors();
        }

        int determineParticipantIndex(object o)
        {
            var t = (ToolStripItem)o;
            return (int)t.Tag;
        }

        void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            raceTimer.newRace();
        }

        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void serialPortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string portName = (string)serialPortBox.SelectedItem;
            if (portName == "Not connected")
            {
                portName = null;
            }
            if (raceTimer.portName != portName)
            {
                raceTimer.setPort(portName);
            }
        }

        void resetParticipantMenuItem_Click(object sender, EventArgs e)
        {
            int participantIndex = determineParticipantIndex(sender);
            raceTimer.resetParticipant(participantIndex);
        }


    }
}
