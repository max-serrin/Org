namespace DL
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bDownload = new System.Windows.Forms.Button();
            this.lDownloaded = new System.Windows.Forms.Label();
            this.ldCount = new System.Windows.Forms.Label();
            this.bSetPath = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.tbPath2 = new System.Windows.Forms.TextBox();
            this.tbURL2 = new System.Windows.Forms.TextBox();
            this.tbWebsite = new System.Windows.Forms.TextBox();
            this.bAuto = new System.Windows.Forms.Button();
            this.bAutoDL = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(636, 168);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(12, 25);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(612, 20);
            this.tbURL.TabIndex = 1;
            this.tbURL.Click += new System.EventHandler(this.tbURL_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(96, 79);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(330, 20);
            this.tbPath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "URL (+Fusker)";
            // 
            // bDownload
            // 
            this.bDownload.Location = new System.Drawing.Point(432, 78);
            this.bDownload.Name = "bDownload";
            this.bDownload.Size = new System.Drawing.Size(75, 23);
            this.bDownload.TabIndex = 6;
            this.bDownload.Text = "Download";
            this.bDownload.UseVisualStyleBackColor = true;
            this.bDownload.Click += new System.EventHandler(this.bDownload_Click);
            // 
            // lDownloaded
            // 
            this.lDownloaded.AutoSize = true;
            this.lDownloaded.Location = new System.Drawing.Point(513, 82);
            this.lDownloaded.Name = "lDownloaded";
            this.lDownloaded.Size = new System.Drawing.Size(70, 13);
            this.lDownloaded.TabIndex = 7;
            this.lDownloaded.Text = "Downloaded:";
            // 
            // ldCount
            // 
            this.ldCount.AutoSize = true;
            this.ldCount.Location = new System.Drawing.Point(589, 82);
            this.ldCount.Name = "ldCount";
            this.ldCount.Size = new System.Drawing.Size(13, 13);
            this.ldCount.TabIndex = 8;
            this.ldCount.Text = "0";
            // 
            // bSetPath
            // 
            this.bSetPath.Location = new System.Drawing.Point(14, 77);
            this.bSetPath.Name = "bSetPath";
            this.bSetPath.Size = new System.Drawing.Size(75, 23);
            this.bSetPath.TabIndex = 9;
            this.bSetPath.Text = "Set Path";
            this.bSetPath.UseVisualStyleBackColor = true;
            this.bSetPath.Click += new System.EventHandler(this.bSetPath_Click);
            // 
            // fbd
            // 
            this.fbd.SelectedPath = "C:\\";
            // 
            // tbPath2
            // 
            this.tbPath2.Location = new System.Drawing.Point(96, 105);
            this.tbPath2.Name = "tbPath2";
            this.tbPath2.Size = new System.Drawing.Size(330, 20);
            this.tbPath2.TabIndex = 10;
            this.tbPath2.Click += new System.EventHandler(this.tbPath2_Click);
            // 
            // tbURL2
            // 
            this.tbURL2.Location = new System.Drawing.Point(12, 51);
            this.tbURL2.Name = "tbURL2";
            this.tbURL2.Size = new System.Drawing.Size(612, 20);
            this.tbURL2.TabIndex = 1;
            // 
            // tbWebsite
            // 
            this.tbWebsite.Location = new System.Drawing.Point(12, 136);
            this.tbWebsite.Name = "tbWebsite";
            this.tbWebsite.Size = new System.Drawing.Size(531, 20);
            this.tbWebsite.TabIndex = 0;
            this.tbWebsite.Click += new System.EventHandler(this.tbWebsite_Click);
            // 
            // bAuto
            // 
            this.bAuto.Location = new System.Drawing.Point(549, 134);
            this.bAuto.Name = "bAuto";
            this.bAuto.Size = new System.Drawing.Size(75, 23);
            this.bAuto.TabIndex = 12;
            this.bAuto.Text = "Auto Fill";
            this.bAuto.UseVisualStyleBackColor = true;
            this.bAuto.Click += new System.EventHandler(this.bAuto_Click);
            // 
            // bAutoDL
            // 
            this.bAutoDL.Enabled = false;
            this.bAutoDL.Location = new System.Drawing.Point(549, 105);
            this.bAutoDL.Name = "bAutoDL";
            this.bAutoDL.Size = new System.Drawing.Size(75, 23);
            this.bAutoDL.TabIndex = 13;
            this.bAutoDL.Text = "Auto DL";
            this.bAutoDL.UseVisualStyleBackColor = true;
            this.bAutoDL.Click += new System.EventHandler(this.bAutoDL_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 168);
            this.Controls.Add(this.bAutoDL);
            this.Controls.Add(this.bAuto);
            this.Controls.Add(this.tbWebsite);
            this.Controls.Add(this.tbPath2);
            this.Controls.Add(this.bSetPath);
            this.Controls.Add(this.ldCount);
            this.Controls.Add(this.lDownloaded);
            this.Controls.Add(this.bDownload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.tbURL2);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bDownload;
        private System.Windows.Forms.Label lDownloaded;
        private System.Windows.Forms.Label ldCount;
        private System.Windows.Forms.Button bSetPath;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.TextBox tbPath2;
        private System.Windows.Forms.TextBox tbURL2;
        private System.Windows.Forms.TextBox tbWebsite;
        private System.Windows.Forms.Button bAuto;
        private System.Windows.Forms.Button bAutoDL;
    }
}

