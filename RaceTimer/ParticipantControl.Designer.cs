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
            this.lapList = new System.Windows.Forms.ListView();
            this.lapColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.Location = new System.Drawing.Point(3, 3);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(450, 62);
            this.nameBox.TabIndex = 0;
            // 
            // totalTimeBox
            // 
            this.totalTimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalTimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalTimeBox.Location = new System.Drawing.Point(3, 355);
            this.totalTimeBox.Name = "totalTimeBox";
            this.totalTimeBox.ReadOnly = true;
            this.totalTimeBox.Size = new System.Drawing.Size(450, 62);
            this.totalTimeBox.TabIndex = 2;
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
            this.lapList.Location = new System.Drawing.Point(3, 71);
            this.lapList.Name = "lapList";
            this.lapList.Size = new System.Drawing.Size(450, 278);
            this.lapList.TabIndex = 1;
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
            // ParticipantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lapList);
            this.Controls.Add(this.totalTimeBox);
            this.Controls.Add(this.nameBox);
            this.Name = "ParticipantControl";
            this.Size = new System.Drawing.Size(456, 420);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox totalTimeBox;
        public System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.ColumnHeader lapColumn;
        private System.Windows.Forms.ColumnHeader timeColumn;
        public System.Windows.Forms.ListView lapList;

    }
}
