namespace RaceTimerApp
{
    partial class ParticipantControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameBox = new System.Windows.Forms.TextBox();
            this.totalTimeBox = new System.Windows.Forms.TextBox();
            this.lapList = new RaceTimerApp.MyListView();
            this.lapColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.totalTimeLabel = new System.Windows.Forms.Label();
            this.bestLapTimeBox = new System.Windows.Forms.TextBox();
            this.bestLapTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.Location = new System.Drawing.Point(4, 4);
            this.nameBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(599, 75);
            this.nameBox.TabIndex = 0;
            // 
            // totalTimeBox
            // 
            this.totalTimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalTimeBox.Location = new System.Drawing.Point(192, 526);
            this.totalTimeBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.totalTimeBox.Name = "totalTimeBox";
            this.totalTimeBox.ReadOnly = true;
            this.totalTimeBox.Size = new System.Drawing.Size(411, 75);
            this.totalTimeBox.TabIndex = 5;
            this.totalTimeBox.TabStop = false;
            this.totalTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lapList
            // 
            this.lapList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lapList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lapColumn,
            this.timeColumn});
            this.lapList.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lapList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lapList.Location = new System.Drawing.Point(4, 87);
            this.lapList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lapList.Name = "lapList";
            this.lapList.Size = new System.Drawing.Size(599, 346);
            this.lapList.TabIndex = 1;
            this.lapList.TabStop = false;
            this.lapList.UseCompatibleStateImageBehavior = false;
            this.lapList.View = System.Windows.Forms.View.Details;
            // 
            // lapColumn
            // 
            this.lapColumn.Text = "#";
            this.lapColumn.Width = 100;
            // 
            // timeColumn
            // 
            this.timeColumn.Text = "Time";
            this.timeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.timeColumn.Width = 300;
            // 
            // totalTimeLabel
            // 
            this.totalTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.totalTimeLabel.AutoSize = true;
            this.totalTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalTimeLabel.Location = new System.Drawing.Point(4, 548);
            this.totalTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalTimeLabel.Name = "totalTimeLabel";
            this.totalTimeLabel.Size = new System.Drawing.Size(109, 46);
            this.totalTimeLabel.TabIndex = 4;
            this.totalTimeLabel.Text = "Total";
            // 
            // bestLapTimeBox
            // 
            this.bestLapTimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bestLapTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bestLapTimeBox.Location = new System.Drawing.Point(192, 442);
            this.bestLapTimeBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bestLapTimeBox.Name = "bestLapTimeBox";
            this.bestLapTimeBox.ReadOnly = true;
            this.bestLapTimeBox.Size = new System.Drawing.Size(411, 75);
            this.bestLapTimeBox.TabIndex = 3;
            this.bestLapTimeBox.TabStop = false;
            this.bestLapTimeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bestLapTimeLabel
            // 
            this.bestLapTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bestLapTimeLabel.AutoSize = true;
            this.bestLapTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bestLapTimeLabel.Location = new System.Drawing.Point(4, 464);
            this.bestLapTimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.bestLapTimeLabel.Name = "bestLapTimeLabel";
            this.bestLapTimeLabel.Size = new System.Drawing.Size(100, 46);
            this.bestLapTimeLabel.TabIndex = 2;
            this.bestLapTimeLabel.Text = "Best";
            // 
            // ParticipantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bestLapTimeLabel);
            this.Controls.Add(this.bestLapTimeBox);
            this.Controls.Add(this.totalTimeLabel);
            this.Controls.Add(this.lapList);
            this.Controls.Add(this.totalTimeBox);
            this.Controls.Add(this.nameBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ParticipantControl";
            this.Size = new System.Drawing.Size(608, 606);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox totalTimeBox;
        public System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ColumnHeader lapColumn;
        private System.Windows.Forms.ColumnHeader timeColumn;
        private System.Windows.Forms.Label totalTimeLabel;
        public System.Windows.Forms.TextBox bestLapTimeBox;
        private System.Windows.Forms.Label bestLapTimeLabel;
        public MyListView lapList;

    }
}
