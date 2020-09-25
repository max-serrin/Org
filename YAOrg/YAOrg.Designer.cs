namespace YAOrg
{
    partial class YAOrg
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbCurrentFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.iFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTags = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbFileList = new System.Windows.Forms.ListBox();
            this.bFileListUp = new System.Windows.Forms.Button();
            this.bFileListDown = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCurrentFile
            // 
            this.tbCurrentFile.Location = new System.Drawing.Point(54, 35);
            this.tbCurrentFile.Name = "tbCurrentFile";
            this.tbCurrentFile.Size = new System.Drawing.Size(587, 23);
            this.tbCurrentFile.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "File";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(980, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(980, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // iFile
            // 
            this.iFile.Name = "iFile";
            this.iFile.Size = new System.Drawing.Size(37, 20);
            this.iFile.Text = "File";
            // 
            // tbTags
            // 
            this.tbTags.Location = new System.Drawing.Point(54, 87);
            this.tbTags.Multiline = true;
            this.tbTags.Name = "tbTags";
            this.tbTags.Size = new System.Drawing.Size(587, 296);
            this.tbTags.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tags";
            // 
            // lbFileList
            // 
            this.lbFileList.FormattingEnabled = true;
            this.lbFileList.ItemHeight = 15;
            this.lbFileList.Location = new System.Drawing.Point(705, 34);
            this.lbFileList.Name = "lbFileList";
            this.lbFileList.Size = new System.Drawing.Size(240, 349);
            this.lbFileList.TabIndex = 5;
            this.lbFileList.SelectedIndexChanged += new System.EventHandler(this.lbFileList_SelectedIndexChanged);
            // 
            // bFileListUp
            // 
            this.bFileListUp.Location = new System.Drawing.Point(654, 34);
            this.bFileListUp.Name = "bFileListUp";
            this.bFileListUp.Size = new System.Drawing.Size(37, 23);
            this.bFileListUp.TabIndex = 6;
            this.bFileListUp.Text = "▲";
            this.bFileListUp.UseVisualStyleBackColor = true;
            // 
            // bFileListDown
            // 
            this.bFileListDown.Location = new System.Drawing.Point(654, 63);
            this.bFileListDown.Name = "bFileListDown";
            this.bFileListDown.Size = new System.Drawing.Size(37, 23);
            this.bFileListDown.TabIndex = 6;
            this.bFileListDown.Text = "▼";
            this.bFileListDown.UseVisualStyleBackColor = true;
            // 
            // YAOrg
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 450);
            this.Controls.Add(this.bFileListDown);
            this.Controls.Add(this.bFileListUp);
            this.Controls.Add(this.lbFileList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTags);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCurrentFile);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "YAOrg";
            this.Text = "YAOrg";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.YAOrg_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.YAOrg_DragEnter);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCurrentFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem iFile;
        private System.Windows.Forms.TextBox tbTags;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbFileList;
        private System.Windows.Forms.Button bFileListUp;
        private System.Windows.Forms.Button bFileListDown;
    }
}

