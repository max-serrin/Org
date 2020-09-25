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
            this.components = new System.ComponentModel.Container();
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
            this.cbSearchAll = new System.Windows.Forms.CheckBox();
            this.cbCopy = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbExtensions = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbUseBlacklist = new System.Windows.Forms.CheckBox();
            this.cbUseWhitelist = new System.Windows.Forms.CheckBox();
            this.tbBlacklist = new System.Windows.Forms.TextBox();
            this.tbWhitelist = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbFileNameFilter = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.bOpen = new System.Windows.Forms.Button();
            this.tbListFileContents = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbListFileName = new System.Windows.Forms.TextBox();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.tTimerTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTimer
            // 
            this.tbTimer.Location = new System.Drawing.Point(169, 16);
            this.tbTimer.Margin = new System.Windows.Forms.Padding(2);
            this.tbTimer.Name = "tbTimer";
            this.tbTimer.Size = new System.Drawing.Size(92, 20);
            this.tbTimer.TabIndex = 0;
            this.tbTimer.TextChanged += new System.EventHandler(this.tbTimer_TextChanged);
            this.tbTimer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
            // 
            // tTimerTrackBar
            // 
            this.tTimerTrackBar.Location = new System.Drawing.Point(8, 35);
            this.tTimerTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.tTimerTrackBar.Maximum = 12;
            this.tTimerTrackBar.Name = "tTimerTrackBar";
            this.tTimerTrackBar.Size = new System.Drawing.Size(250, 45);
            this.tTimerTrackBar.TabIndex = 1;
            this.tTimerTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tTimerTrackBar.Scroll += new System.EventHandler(this.tTimerTrackBar_Scroll);
            this.tTimerTrackBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
            // 
            // tbMaxWidth
            // 
            this.tbMaxWidth.Location = new System.Drawing.Point(66, 21);
            this.tbMaxWidth.Margin = new System.Windows.Forms.Padding(2);
            this.tbMaxWidth.Name = "tbMaxWidth";
            this.tbMaxWidth.Size = new System.Drawing.Size(52, 20);
            this.tbMaxWidth.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tbMaxWidth, "Maximum width to which the window will resize.");
            this.tbMaxWidth.TextChanged += new System.EventHandler(this.tbMaxWidth_TextChanged);
            this.tbMaxWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
            // 
            // tbMaxHeight
            // 
            this.tbMaxHeight.Location = new System.Drawing.Point(200, 21);
            this.tbMaxHeight.Margin = new System.Windows.Forms.Padding(2);
            this.tbMaxHeight.Name = "tbMaxHeight";
            this.tbMaxHeight.Size = new System.Drawing.Size(52, 20);
            this.tbMaxHeight.TabIndex = 4;
            this.toolTip1.SetToolTip(this.tbMaxHeight, "Maximum height to which the window will resize.");
            this.tbMaxHeight.TextChanged += new System.EventHandler(this.tbMaxHeight_TextChanged);
            this.tbMaxHeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
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
            this.toolTip1.SetToolTip(this.label2, "Maximum width to which the window will resize.");
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
            this.toolTip1.SetToolTip(this.label3, "Maximum height to which the window will resize.");
            // 
            // cbOnTop
            // 
            this.cbOnTop.AutoSize = true;
            this.cbOnTop.Location = new System.Drawing.Point(9, 48);
            this.cbOnTop.Margin = new System.Windows.Forms.Padding(2);
            this.cbOnTop.Name = "cbOnTop";
            this.cbOnTop.Size = new System.Drawing.Size(98, 17);
            this.cbOnTop.TabIndex = 8;
            this.cbOnTop.Text = "Always On Top";
            this.toolTip1.SetToolTip(this.cbOnTop, "Check to keep the window on top of all other windows.");
            this.cbOnTop.UseVisualStyleBackColor = true;
            this.cbOnTop.CheckedChanged += new System.EventHandler(this.cbOnTop_CheckedChanged);
            this.cbOnTop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbMaxHeight);
            this.groupBox1.Controls.Add(this.cbOnTop);
            this.groupBox1.Controls.Add(this.tbMaxWidth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
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
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
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
            this.toolTip1.SetToolTip(this.label4, "Amount of rest time in seconds between images once the timer runs out.");
            // 
            // tbTimerBuffer
            // 
            this.tbTimerBuffer.Location = new System.Drawing.Point(169, 85);
            this.tbTimerBuffer.Margin = new System.Windows.Forms.Padding(2);
            this.tbTimerBuffer.Name = "tbTimerBuffer";
            this.tbTimerBuffer.Size = new System.Drawing.Size(92, 20);
            this.tbTimerBuffer.TabIndex = 3;
            this.tbTimerBuffer.TextChanged += new System.EventHandler(this.tbTimerBuffer_TextChanged);
            this.tbTimerBuffer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
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
            this.toolTip1.SetToolTip(this.label1, "Enter a timer in the form HH:MM:SS or H:M:S.");
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
            this.toolTip1.SetToolTip(this.cbSearchAll, "When checked all directories within a given directory will be searched.\r\nWhen unc" +
        "hecked, only the top directory will be searched.");
            this.cbSearchAll.UseVisualStyleBackColor = true;
            this.cbSearchAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.cbSearchAll.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
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
            this.toolTip1.SetToolTip(this.cbCopy, "When checked the image will be copied.\r\nWhen unchcekd the image will be cut.\r\n(Cu" +
        "t images require the window to be closed\r\nbefore they can be pasted.)");
            this.cbCopy.UseVisualStyleBackColor = true;
            this.cbCopy.CheckedChanged += new System.EventHandler(this.cbCopy_CheckedChanged);
            this.cbCopy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
            // 
            // tbExtensions
            // 
            this.tbExtensions.Location = new System.Drawing.Point(70, 20);
            this.tbExtensions.Name = "tbExtensions";
            this.tbExtensions.Size = new System.Drawing.Size(202, 20);
            this.tbExtensions.TabIndex = 12;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.tbExtensions);
            this.groupBox4.Location = new System.Drawing.Point(279, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(278, 54);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "File Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Extensions";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbUseBlacklist);
            this.groupBox5.Controls.Add(this.cbUseWhitelist);
            this.groupBox5.Controls.Add(this.tbBlacklist);
            this.groupBox5.Controls.Add(this.tbWhitelist);
            this.groupBox5.Location = new System.Drawing.Point(279, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(278, 180);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tags (JPEG only, seperate by semi-colon or new line)";
            // 
            // cbUseBlacklist
            // 
            this.cbUseBlacklist.AutoSize = true;
            this.cbUseBlacklist.Location = new System.Drawing.Point(142, 21);
            this.cbUseBlacklist.Name = "cbUseBlacklist";
            this.cbUseBlacklist.Size = new System.Drawing.Size(65, 17);
            this.cbUseBlacklist.TabIndex = 17;
            this.cbUseBlacklist.Text = "Blacklist";
            this.cbUseBlacklist.UseVisualStyleBackColor = true;
            this.cbUseBlacklist.CheckedChanged += new System.EventHandler(this.cbUseBlacklist_CheckedChanged);
            // 
            // cbUseWhitelist
            // 
            this.cbUseWhitelist.AutoSize = true;
            this.cbUseWhitelist.Location = new System.Drawing.Point(9, 21);
            this.cbUseWhitelist.Name = "cbUseWhitelist";
            this.cbUseWhitelist.Size = new System.Drawing.Size(66, 17);
            this.cbUseWhitelist.TabIndex = 16;
            this.cbUseWhitelist.Text = "Whitelist";
            this.cbUseWhitelist.UseVisualStyleBackColor = true;
            this.cbUseWhitelist.CheckedChanged += new System.EventHandler(this.cbUseWhitelist_CheckedChanged);
            // 
            // tbBlacklist
            // 
            this.tbBlacklist.Location = new System.Drawing.Point(142, 38);
            this.tbBlacklist.Multiline = true;
            this.tbBlacklist.Name = "tbBlacklist";
            this.tbBlacklist.Size = new System.Drawing.Size(125, 135);
            this.tbBlacklist.TabIndex = 15;
            // 
            // tbWhitelist
            // 
            this.tbWhitelist.Location = new System.Drawing.Point(9, 38);
            this.tbWhitelist.Multiline = true;
            this.tbWhitelist.Name = "tbWhitelist";
            this.tbWhitelist.Size = new System.Drawing.Size(127, 135);
            this.tbWhitelist.TabIndex = 14;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.tbFileNameFilter);
            this.groupBox6.Location = new System.Drawing.Point(6, 252);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(551, 54);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "File Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Filter";
            // 
            // tbFileNameFilter
            // 
            this.tbFileNameFilter.Location = new System.Drawing.Point(70, 20);
            this.tbFileNameFilter.Name = "tbFileNameFilter";
            this.tbFileNameFilter.Size = new System.Drawing.Size(470, 20);
            this.tbFileNameFilter.TabIndex = 12;
            this.tbFileNameFilter.TextChanged += new System.EventHandler(this.tbFileNameFilter_TextChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.bOpen);
            this.groupBox7.Controls.Add(this.tbListFileContents);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.tbListFileName);
            this.groupBox7.Location = new System.Drawing.Point(6, 312);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(551, 224);
            this.groupBox7.TabIndex = 16;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "File";
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(465, 17);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(75, 23);
            this.bOpen.TabIndex = 16;
            this.bOpen.Text = "Open";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.Open_Click);
            // 
            // tbListFileContents
            // 
            this.tbListFileContents.Location = new System.Drawing.Point(6, 46);
            this.tbListFileContents.Multiline = true;
            this.tbListFileContents.Name = "tbListFileContents";
            this.tbListFileContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbListFileContents.Size = new System.Drawing.Size(534, 172);
            this.tbListFileContents.TabIndex = 15;
            this.tbListFileContents.TextChanged += new System.EventHandler(this.tbListFileContents_TextChanged);
            this.tbListFileContents.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
            this.tbListFileContents.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Use List";
            // 
            // tbListFileName
            // 
            this.tbListFileName.AllowDrop = true;
            this.tbListFileName.Location = new System.Drawing.Point(70, 20);
            this.tbListFileName.Name = "tbListFileName";
            this.tbListFileName.Size = new System.Drawing.Size(389, 20);
            this.tbListFileName.TabIndex = 12;
            this.tbListFileName.TextChanged += new System.EventHandler(this.tbListFileName_TextChanged);
            this.tbListFileName.DragDrop += new System.Windows.Forms.DragEventHandler(this.OpenFile_DragDrop);
            this.tbListFileName.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
            // 
            // bOk
            // 
            this.bOk.BackColor = System.Drawing.SystemColors.Highlight;
            this.bOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bOk.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.bOk.Location = new System.Drawing.Point(15, 545);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(259, 23);
            this.bOk.TabIndex = 17;
            this.bOk.Text = "Save";
            this.bOk.UseVisualStyleBackColor = false;
            this.bOk.Click += new System.EventHandler(this.Ok_Click);
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.Color.IndianRed;
            this.bCancel.FlatAppearance.BorderColor = System.Drawing.Color.Brown;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCancel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.bCancel.Location = new System.Drawing.Point(292, 545);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(259, 23);
            this.bCancel.TabIndex = 18;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = false;
            this.bCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Settings
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 580);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tTimerTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
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
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tbExtensions;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbBlacklist;
        private System.Windows.Forms.TextBox tbWhitelist;
        private System.Windows.Forms.CheckBox cbUseBlacklist;
        private System.Windows.Forms.CheckBox cbUseWhitelist;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbFileNameFilter;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tbListFileContents;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbListFileName;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}