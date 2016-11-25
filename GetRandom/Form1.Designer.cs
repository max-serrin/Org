namespace GetRandom
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
            this.getRandomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.nextXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextXPopupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.promoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getRandomToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.lastImageToolStripMenuItem,
            this.nextImageToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(493, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // getRandomToolStripMenuItem
            // 
            this.getRandomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFolderToolStripMenuItem,
            this.allDirectoriesToolStripMenuItem});
            this.getRandomToolStripMenuItem.Name = "getRandomToolStripMenuItem";
            this.getRandomToolStripMenuItem.Size = new System.Drawing.Size(29, 20);
            this.getRandomToolStripMenuItem.Text = "◙";
            // 
            // setFolderToolStripMenuItem
            // 
            this.setFolderToolStripMenuItem.Name = "setFolderToolStripMenuItem";
            this.setFolderToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.setFolderToolStripMenuItem.Text = "Set Folder";
            this.setFolderToolStripMenuItem.Click += new System.EventHandler(this.setFolderToolStripMenuItem_Click);
            // 
            // allDirectoriesToolStripMenuItem
            // 
            this.allDirectoriesToolStripMenuItem.Name = "allDirectoriesToolStripMenuItem";
            this.allDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.allDirectoriesToolStripMenuItem.Text = "All Directories";
            this.allDirectoriesToolStripMenuItem.Click += new System.EventHandler(this.allDirectoriesToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileLocationToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.favoritesToolStripMenuItem,
            this.toolStripSeparator1,
            this.nextXToolStripMenuItem,
            this.nextXPopupToolStripMenuItem,
            this.toolStripSeparator3,
            this.promoteToolStripMenuItem,
            this.demoteToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteImageToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // openFileLocationToolStripMenuItem
            // 
            this.openFileLocationToolStripMenuItem.Name = "openFileLocationToolStripMenuItem";
            this.openFileLocationToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openFileLocationToolStripMenuItem.Text = "Open File Location";
            this.openFileLocationToolStripMenuItem.Click += new System.EventHandler(this.openFileLocationToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // favoritesToolStripMenuItem
            // 
            this.favoritesToolStripMenuItem.CheckOnClick = true;
            this.favoritesToolStripMenuItem.Name = "favoritesToolStripMenuItem";
            this.favoritesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.favoritesToolStripMenuItem.Text = "Favorites";
            this.favoritesToolStripMenuItem.Click += new System.EventHandler(this.favoritesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // nextXToolStripMenuItem
            // 
            this.nextXToolStripMenuItem.Name = "nextXToolStripMenuItem";
            this.nextXToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.nextXToolStripMenuItem.Text = "Next X";
            this.nextXToolStripMenuItem.Click += new System.EventHandler(this.nextXToolStripMenuItem_Click);
            // 
            // nextXPopupToolStripMenuItem
            // 
            this.nextXPopupToolStripMenuItem.Name = "nextXPopupToolStripMenuItem";
            this.nextXPopupToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.nextXPopupToolStripMenuItem.Text = "Next X Popup";
            this.nextXPopupToolStripMenuItem.Click += new System.EventHandler(this.nextXPopupToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(170, 6);
            // 
            // promoteToolStripMenuItem
            // 
            this.promoteToolStripMenuItem.Name = "promoteToolStripMenuItem";
            this.promoteToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.promoteToolStripMenuItem.Text = "Promote";
            this.promoteToolStripMenuItem.Click += new System.EventHandler(this.promoteToolStripMenuItem_Click);
            // 
            // demoteToolStripMenuItem
            // 
            this.demoteToolStripMenuItem.Name = "demoteToolStripMenuItem";
            this.demoteToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.demoteToolStripMenuItem.Text = "Demote";
            this.demoteToolStripMenuItem.Click += new System.EventHandler(this.demoteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // deleteImageToolStripMenuItem
            // 
            this.deleteImageToolStripMenuItem.Name = "deleteImageToolStripMenuItem";
            this.deleteImageToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.deleteImageToolStripMenuItem.Text = "Delete Image";
            this.deleteImageToolStripMenuItem.Click += new System.EventHandler(this.deleteImageToolStripMenuItem_Click);
            // 
            // lastImageToolStripMenuItem
            // 
            this.lastImageToolStripMenuItem.Name = "lastImageToolStripMenuItem";
            this.lastImageToolStripMenuItem.Size = new System.Drawing.Size(27, 20);
            this.lastImageToolStripMenuItem.Text = "<";
            this.lastImageToolStripMenuItem.Click += new System.EventHandler(this.lastImageToolStripMenuItem_Click);
            // 
            // nextImageToolStripMenuItem
            // 
            this.nextImageToolStripMenuItem.Name = "nextImageToolStripMenuItem";
            this.nextImageToolStripMenuItem.Size = new System.Drawing.Size(27, 20);
            this.nextImageToolStripMenuItem.Text = ">";
            this.nextImageToolStripMenuItem.Click += new System.EventHandler(this.nextImageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::GetRandom.Properties.Resources.heart_yellow;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(28, 20);
            this.toolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 24);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(493, 341);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 1;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.pbImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 365);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Get Random";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem getRandomToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.ToolStripMenuItem setFolderToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbdBrowser;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem demoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem nextImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem nextXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextXPopupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem favoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allDirectoriesToolStripMenuItem;
    }
}

