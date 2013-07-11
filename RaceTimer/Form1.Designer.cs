﻿namespace RaceTimerApp
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.simulateSensorAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulateSensorBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serialPortBox = new System.Windows.Forms.ToolStripComboBox();
            this.participantControlB = new RaceTimerApp.ParticipantControl();
            this.participantControlA = new RaceTimerApp.ParticipantControl();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serialPortBox,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
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
            this.toolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.toolStripMenuItem1.Text = "New Race";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // simulateSensorAToolStripMenuItem
            // 
            this.simulateSensorAToolStripMenuItem.Name = "simulateSensorAToolStripMenuItem";
            this.simulateSensorAToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.simulateSensorAToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.simulateSensorAToolStripMenuItem.Tag = "0";
            this.simulateSensorAToolStripMenuItem.Text = "Simulate Sensor A";
            this.simulateSensorAToolStripMenuItem.Click += new System.EventHandler(this.simulateSensorMenuItem_Click);
            // 
            // simulateSensorBToolStripMenuItem
            // 
            this.simulateSensorBToolStripMenuItem.Name = "simulateSensorBToolStripMenuItem";
            this.simulateSensorBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.simulateSensorBToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.simulateSensorBToolStripMenuItem.Tag = "1";
            this.simulateSensorBToolStripMenuItem.Text = "Simulate Sensor B";
            this.simulateSensorBToolStripMenuItem.Click += new System.EventHandler(this.simulateSensorMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.raceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(941, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serialPortBox
            // 
            this.serialPortBox.Name = "serialPortBox";
            this.serialPortBox.Size = new System.Drawing.Size(121, 23);
            // 
            // participantControlB
            // 
            this.participantControlB.Location = new System.Drawing.Point(474, 27);
            this.participantControlB.Name = "participantControlB";
            this.participantControlB.Size = new System.Drawing.Size(456, 420);
            this.participantControlB.TabIndex = 2;
            // 
            // participantControlA
            // 
            this.participantControlA.Location = new System.Drawing.Point(12, 27);
            this.participantControlA.Name = "participantControlA";
            this.participantControlA.Size = new System.Drawing.Size(456, 420);
            this.participantControlA.TabIndex = 1;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 470);
            this.Controls.Add(this.participantControlB);
            this.Controls.Add(this.participantControlA);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Race Timer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ParticipantControl participantControlA;
        private ParticipantControl participantControlB;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox serialPortBox;
        private System.Windows.Forms.ToolStripMenuItem raceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem simulateSensorAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulateSensorBToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;


    }
}

