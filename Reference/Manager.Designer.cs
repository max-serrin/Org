namespace Reference
{
    partial class Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manager));
            this.tFolders = new System.Windows.Forms.TreeView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainerTextBox = new System.Windows.Forms.SplitContainer();
            this.tFile = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tFilePath = new System.Windows.Forms.ToolStripStatusLabel();
            this.tSave = new System.Windows.Forms.ToolStripDropDownButton();
            this.tOpenFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tOpenSelected = new System.Windows.Forms.ToolStripDropDownButton();
            this.openAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTextBox)).BeginInit();
            this.splitContainerTextBox.Panel1.SuspendLayout();
            this.splitContainerTextBox.Panel2.SuspendLayout();
            this.splitContainerTextBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tFolders
            // 
            this.tFolders.CheckBoxes = true;
            this.tFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tFolders.Location = new System.Drawing.Point(0, 0);
            this.tFolders.Name = "tFolders";
            this.tFolders.Size = new System.Drawing.Size(304, 564);
            this.tFolders.TabIndex = 0;
            this.tFolders.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tFolders_AfterCheck);
            this.tFolders.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tFolders_AfterExpand);
            this.tFolders.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tFolders_NodeMouseClick);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tFolders);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainerTextBox);
            this.splitContainer.Size = new System.Drawing.Size(981, 564);
            this.splitContainer.SplitterDistance = 304;
            this.splitContainer.TabIndex = 1;
            // 
            // splitContainerTextBox
            // 
            this.splitContainerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTextBox.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerTextBox.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTextBox.Name = "splitContainerTextBox";
            this.splitContainerTextBox.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTextBox.Panel1
            // 
            this.splitContainerTextBox.Panel1.Controls.Add(this.tFile);
            // 
            // splitContainerTextBox.Panel2
            // 
            this.splitContainerTextBox.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainerTextBox.Size = new System.Drawing.Size(673, 564);
            this.splitContainerTextBox.SplitterDistance = 535;
            this.splitContainerTextBox.TabIndex = 0;
            // 
            // tFile
            // 
            this.tFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tFile.Enabled = false;
            this.tFile.Location = new System.Drawing.Point(0, 0);
            this.tFile.MaxLength = 2147483646;
            this.tFile.Multiline = true;
            this.tFile.Name = "tFile";
            this.tFile.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tFile.Size = new System.Drawing.Size(673, 535);
            this.tFile.TabIndex = 0;
            this.tFile.TextChanged += new System.EventHandler(this.tFile_TextChanged);
            this.tFile.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tFile_PreviewKeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tFilePath,
            this.tSave,
            this.tOpenFile,
            this.tOpenSelected});
            this.statusStrip1.Location = new System.Drawing.Point(0, 3);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(673, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tFilePath
            // 
            this.tFilePath.Name = "tFilePath";
            this.tFilePath.Size = new System.Drawing.Size(0, 17);
            // 
            // tSave
            // 
            this.tSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tSave.Image = ((System.Drawing.Image)(resources.GetObject("tSave.Image")));
            this.tSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSave.Name = "tSave";
            this.tSave.ShowDropDownArrow = false;
            this.tSave.Size = new System.Drawing.Size(4, 20);
            this.tSave.Click += new System.EventHandler(this.tSave_Click);
            // 
            // tOpenFile
            // 
            this.tOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tOpenFile.Enabled = false;
            this.tOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tOpenFile.Image")));
            this.tOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tOpenFile.Name = "tOpenFile";
            this.tOpenFile.ShowDropDownArrow = false;
            this.tOpenFile.Size = new System.Drawing.Size(108, 20);
            this.tOpenFile.Text = "Open in Reference";
            this.tOpenFile.Click += new System.EventHandler(this.tOpenFile_Click);
            // 
            // tOpenSelected
            // 
            this.tOpenSelected.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tOpenSelected.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.openAllToolStripMenuItem});
            this.tOpenSelected.Image = ((System.Drawing.Image)(resources.GetObject("tOpenSelected.Image")));
            this.tOpenSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tOpenSelected.Name = "tOpenSelected";
            this.tOpenSelected.Size = new System.Drawing.Size(64, 20);
            this.tOpenSelected.Text = "Selected";
            // 
            // openAllToolStripMenuItem
            // 
            this.openAllToolStripMenuItem.Name = "openAllToolStripMenuItem";
            this.openAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openAllToolStripMenuItem.Text = "Open All";
            this.openAllToolStripMenuItem.Click += new System.EventHandler(this.openAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // Manager
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 564);
            this.Controls.Add(this.splitContainer);
            this.Name = "Manager";
            this.Text = "Manager";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.tFile_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.tFile_DragEnter);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainerTextBox.Panel1.ResumeLayout(false);
            this.splitContainerTextBox.Panel1.PerformLayout();
            this.splitContainerTextBox.Panel2.ResumeLayout(false);
            this.splitContainerTextBox.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTextBox)).EndInit();
            this.splitContainerTextBox.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tFolders;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainerTextBox;
        private System.Windows.Forms.TextBox tFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tFilePath;
        private System.Windows.Forms.ToolStripDropDownButton tSave;
        private System.Windows.Forms.ToolStripDropDownButton tOpenFile;
        private System.Windows.Forms.ToolStripDropDownButton tOpenSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openAllToolStripMenuItem;
    }
}