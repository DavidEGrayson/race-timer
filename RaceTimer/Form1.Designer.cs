namespace RaceTimerApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.simulateSensorAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulateSensorBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.participantControl1 = new RaceTimerApp.ParticipantControl();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.containerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.raceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1029, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // raceToolStripMenuItem
            // 
            this.raceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.simulateSensorAToolStripMenuItem,
            this.simulateSensorBToolStripMenuItem});
            this.raceToolStripMenuItem.Name = "raceToolStripMenuItem";
            this.raceToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.raceToolStripMenuItem.Text = "Race";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 22);
            this.toolStripMenuItem1.Text = "New Race";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // simulateSensorAToolStripMenuItem
            // 
            this.simulateSensorAToolStripMenuItem.Name = "simulateSensorAToolStripMenuItem";
            this.simulateSensorAToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.simulateSensorAToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.simulateSensorAToolStripMenuItem.Tag = "0";
            this.simulateSensorAToolStripMenuItem.Text = "Simulate Sensor A";
            this.simulateSensorAToolStripMenuItem.Click += new System.EventHandler(this.simulateSensorMenuItem_Click);
            // 
            // simulateSensorBToolStripMenuItem
            // 
            this.simulateSensorBToolStripMenuItem.Name = "simulateSensorBToolStripMenuItem";
            this.simulateSensorBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.simulateSensorBToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.simulateSensorBToolStripMenuItem.Tag = "1";
            this.simulateSensorBToolStripMenuItem.Text = "Simulate Sensor B";
            this.simulateSensorBToolStripMenuItem.Click += new System.EventHandler(this.simulateSensorMenuItem_Click);
            // 
            // participantControl1
            // 
            this.participantControl1.Location = new System.Drawing.Point(65, 108);
            this.participantControl1.Name = "participantControl1";
            this.participantControl1.Size = new System.Drawing.Size(456, 420);
            this.participantControl1.TabIndex = 1;
            // 
            // containerPanel
            // 
            this.containerPanel.Controls.Add(this.participantControl1);
            this.containerPanel.Controls.Add(this.menuStrip1);
            this.containerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerPanel.Location = new System.Drawing.Point(0, 0);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(1029, 570);
            this.containerPanel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 570);
            this.Controls.Add(this.containerPanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Race Timer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.containerPanel.ResumeLayout(false);
            this.containerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem simulateSensorAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulateSensorBToolStripMenuItem;
        private ParticipantControl participantControl1;
        private System.Windows.Forms.Panel containerPanel;


    }
}

