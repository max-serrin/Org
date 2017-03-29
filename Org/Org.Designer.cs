namespace Org
{
    partial class Org
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
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDirectory = new System.Windows.Forms.TabPage();
            this.folderBrowser = new System.Windows.Forms.TreeView();
            this.tabPageFiles = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageDirectory.SuspendLayout();
            this.tabPageFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.modeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1166, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Org_KeyDown);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.browseToolStripMenuItem,
            this.organizeToolStripMenuItem,
            this.randomizeToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // browseToolStripMenuItem
            // 
            this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
            this.browseToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.browseToolStripMenuItem.Text = "Browse";
            this.browseToolStripMenuItem.Click += new System.EventHandler(this.browseToolStripMenuItem_Click);
            // 
            // organizeToolStripMenuItem
            // 
            this.organizeToolStripMenuItem.Name = "organizeToolStripMenuItem";
            this.organizeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.organizeToolStripMenuItem.Text = "Organize";
            this.organizeToolStripMenuItem.Click += new System.EventHandler(this.organizeToolStripMenuItem_Click);
            // 
            // randomizeToolStripMenuItem
            // 
            this.randomizeToolStripMenuItem.Name = "randomizeToolStripMenuItem";
            this.randomizeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.randomizeToolStripMenuItem.Text = "Randomize";
            this.randomizeToolStripMenuItem.Click += new System.EventHandler(this.randomizeToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 728);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1166, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox);
            this.splitContainer1.Size = new System.Drawing.Size(1166, 704);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Org_KeyDown);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageDirectory);
            this.tabControl.Controls.Add(this.tabPageFiles);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(153, 704);
            this.tabControl.TabIndex = 0;
            this.tabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Org_KeyDown);
            // 
            // tabPageDirectory
            // 
            this.tabPageDirectory.Controls.Add(this.folderBrowser);
            this.tabPageDirectory.Location = new System.Drawing.Point(4, 22);
            this.tabPageDirectory.Name = "tabPageDirectory";
            this.tabPageDirectory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDirectory.Size = new System.Drawing.Size(145, 678);
            this.tabPageDirectory.TabIndex = 0;
            this.tabPageDirectory.Text = "Directory";
            this.tabPageDirectory.UseVisualStyleBackColor = true;
            // 
            // folderBrowser
            // 
            this.folderBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderBrowser.Location = new System.Drawing.Point(3, 3);
            this.folderBrowser.Name = "folderBrowser";
            this.folderBrowser.Size = new System.Drawing.Size(139, 672);
            this.folderBrowser.TabIndex = 0;
            this.folderBrowser.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.folderBrowser_BeforeExpand);
            this.folderBrowser.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.folderBrowser_NodeMouseClick);
            this.folderBrowser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Org_KeyDown);
            this.folderBrowser.MouseClick += new System.Windows.Forms.MouseEventHandler(this.folderBrowser_MouseClick);
            // 
            // tabPageFiles
            // 
            this.tabPageFiles.Controls.Add(this.listView1);
            this.tabPageFiles.Location = new System.Drawing.Point(4, 22);
            this.tabPageFiles.Name = "tabPageFiles";
            this.tabPageFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFiles.Size = new System.Drawing.Size(145, 678);
            this.tabPageFiles.TabIndex = 1;
            this.tabPageFiles.Text = "Files";
            this.tabPageFiles.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(72, 131);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(121, 97);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Black;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1009, 704);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_DoubleClick);
            this.pictureBox.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            // 
            // Org
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 750);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Org";
            this.Text = "Image Organization Utility";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Org_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Org_MouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageDirectory.ResumeLayout(false);
            this.tabPageFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDirectory;
        private System.Windows.Forms.TreeView folderBrowser;
        private System.Windows.Forms.TabPage tabPageFiles;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

