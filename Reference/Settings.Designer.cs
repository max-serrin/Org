namespace Reference
{
    partial class Settings
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
            this.tbTimer = new System.Windows.Forms.TextBox();
            this.tTimerTrackBar = new System.Windows.Forms.TrackBar();
            this.tbMaxWidth = new System.Windows.Forms.TextBox();
            this.tbMaxHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOnTop = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTimerBuffer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbCopy = new System.Windows.Forms.CheckBox();
            this.cbSearchAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tTimerTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTimer
            // 
            this.tbTimer.Location = new System.Drawing.Point(169, 16);
            this.tbTimer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTimer.Name = "tbTimer";
            this.tbTimer.Size = new System.Drawing.Size(92, 20);
            this.tbTimer.TabIndex = 0;
            this.tbTimer.TextChanged += new System.EventHandler(this.tbTimer_TextChanged);
            // 
            // tTimerTrackBar
            // 
            this.tTimerTrackBar.Location = new System.Drawing.Point(8, 35);
            this.tTimerTrackBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tTimerTrackBar.Maximum = 12;
            this.tTimerTrackBar.Name = "tTimerTrackBar";
            this.tTimerTrackBar.Size = new System.Drawing.Size(250, 45);
            this.tTimerTrackBar.TabIndex = 1;
            this.tTimerTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tTimerTrackBar.Scroll += new System.EventHandler(this.tTimerTrackBar_Scroll);
            // 
            // tbMaxWidth
            // 
            this.tbMaxWidth.Location = new System.Drawing.Point(66, 21);
            this.tbMaxWidth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMaxWidth.Name = "tbMaxWidth";
            this.tbMaxWidth.Size = new System.Drawing.Size(52, 20);
            this.tbMaxWidth.TabIndex = 3;
            this.tbMaxWidth.TextChanged += new System.EventHandler(this.tbMaxWidth_TextChanged);
            // 
            // tbMaxHeight
            // 
            this.tbMaxHeight.Location = new System.Drawing.Point(200, 21);
            this.tbMaxHeight.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMaxHeight.Name = "tbMaxHeight";
            this.tbMaxHeight.Size = new System.Drawing.Size(52, 20);
            this.tbMaxHeight.TabIndex = 4;
            this.tbMaxHeight.TextChanged += new System.EventHandler(this.tbMaxHeight_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Max Height";
            // 
            // cbOnTop
            // 
            this.cbOnTop.AutoSize = true;
            this.cbOnTop.Location = new System.Drawing.Point(9, 48);
            this.cbOnTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbOnTop.Name = "cbOnTop";
            this.cbOnTop.Size = new System.Drawing.Size(98, 17);
            this.cbOnTop.TabIndex = 8;
            this.cbOnTop.Text = "Always On Top";
            this.cbOnTop.UseVisualStyleBackColor = true;
            this.cbOnTop.CheckedChanged += new System.EventHandler(this.cbOnTop_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbMaxHeight);
            this.groupBox1.Controls.Add(this.cbOnTop);
            this.groupBox1.Controls.Add(this.tbMaxWidth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(268, 77);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Window";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbTimerBuffer);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbTimer);
            this.groupBox2.Controls.Add(this.tTimerTrackBar);
            this.groupBox2.Location = new System.Drawing.Point(6, 87);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(268, 112);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Timer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Seconds Between Images";
            // 
            // tbTimerBuffer
            // 
            this.tbTimerBuffer.Location = new System.Drawing.Point(169, 85);
            this.tbTimerBuffer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTimerBuffer.Name = "tbTimerBuffer";
            this.tbTimerBuffer.Size = new System.Drawing.Size(92, 20);
            this.tbTimerBuffer.TabIndex = 3;
            this.tbTimerBuffer.TextChanged += new System.EventHandler(this.tbTimerBuffer_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Timer Reset Value";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbSearchAll);
            this.groupBox3.Controls.Add(this.cbCopy);
            this.groupBox3.Location = new System.Drawing.Point(6, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 42);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // cbCopy
            // 
            this.cbCopy.AutoSize = true;
            this.cbCopy.Checked = true;
            this.cbCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCopy.Location = new System.Drawing.Point(9, 18);
            this.cbCopy.Margin = new System.Windows.Forms.Padding(2);
            this.cbCopy.Name = "cbCopy";
            this.cbCopy.Size = new System.Drawing.Size(50, 17);
            this.cbCopy.TabIndex = 9;
            this.cbCopy.Text = "Copy";
            this.cbCopy.UseVisualStyleBackColor = true;
            this.cbCopy.CheckedChanged += new System.EventHandler(this.cbCopy_CheckedChanged);
            // 
            // cbSearchAll
            // 
            this.cbSearchAll.AutoSize = true;
            this.cbSearchAll.Checked = true;
            this.cbSearchAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSearchAll.Location = new System.Drawing.Point(125, 18);
            this.cbSearchAll.Margin = new System.Windows.Forms.Padding(2);
            this.cbSearchAll.Name = "cbSearchAll";
            this.cbSearchAll.Size = new System.Drawing.Size(127, 17);
            this.cbSearchAll.TabIndex = 10;
            this.cbSearchAll.Text = "Search All Directories";
            this.cbSearchAll.UseVisualStyleBackColor = true;
            this.cbSearchAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 256);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tTimerTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbTimer;
        private System.Windows.Forms.TrackBar tTimerTrackBar;
        private System.Windows.Forms.TextBox tbMaxWidth;
        private System.Windows.Forms.TextBox tbMaxHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbOnTop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTimerBuffer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbCopy;
        private System.Windows.Forms.CheckBox cbSearchAll;
    }
}