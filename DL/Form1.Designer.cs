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
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControlDL = new System.Windows.Forms.TabControl();
            this.tabFusker = new System.Windows.Forms.TabPage();
            this.tabCoedCherry = new System.Windows.Forms.TabPage();
            this.bAutoDL = new System.Windows.Forms.Button();
            this.bAuto = new System.Windows.Forms.Button();
            this.tbWebsite = new System.Windows.Forms.TextBox();
            this.tbPath2 = new System.Windows.Forms.TextBox();
            this.bSetPath = new System.Windows.Forms.Button();
            this.ldCount = new System.Windows.Forms.Label();
            this.lDownloaded = new System.Windows.Forms.Label();
            this.bDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.tbURL2 = new System.Windows.Forms.TextBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxModelName_CoedCherry = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDownload_CoedCherry = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.downloadCount_CoedCherry = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxId_CoedCherry = new System.Windows.Forms.TextBox();
            this.tabTumblr = new System.Windows.Forms.TabPage();
            this.buttonDownload_Tumblr = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLink_Tumblr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.downloadCount_Tumblr = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericOffset_Tumblr = new System.Windows.Forms.NumericUpDown();
            this.tabSpecial = new System.Windows.Forms.TabPage();
            this.buttonDownload_trishdavis9 = new System.Windows.Forms.Button();
            this.buttonDownload_tdavis = new System.Windows.Forms.Button();
            this.tabControlDL.SuspendLayout();
            this.tabFusker.SuspendLayout();
            this.tabCoedCherry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabTumblr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericOffset_Tumblr)).BeginInit();
            this.tabSpecial.SuspendLayout();
            this.SuspendLayout();
            // 
            // fbd
            // 
            this.fbd.SelectedPath = "C:\\";
            // 
            // tabControlDL
            // 
            this.tabControlDL.Controls.Add(this.tabFusker);
            this.tabControlDL.Controls.Add(this.tabCoedCherry);
            this.tabControlDL.Controls.Add(this.tabTumblr);
            this.tabControlDL.Controls.Add(this.tabSpecial);
            this.tabControlDL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDL.Location = new System.Drawing.Point(0, 0);
            this.tabControlDL.Name = "tabControlDL";
            this.tabControlDL.SelectedIndex = 0;
            this.tabControlDL.Size = new System.Drawing.Size(647, 194);
            this.tabControlDL.TabIndex = 0;
            // 
            // tabFusker
            // 
            this.tabFusker.Controls.Add(this.bAutoDL);
            this.tabFusker.Controls.Add(this.bAuto);
            this.tabFusker.Controls.Add(this.tbWebsite);
            this.tabFusker.Controls.Add(this.tbPath2);
            this.tabFusker.Controls.Add(this.bSetPath);
            this.tabFusker.Controls.Add(this.ldCount);
            this.tabFusker.Controls.Add(this.lDownloaded);
            this.tabFusker.Controls.Add(this.bDownload);
            this.tabFusker.Controls.Add(this.label1);
            this.tabFusker.Controls.Add(this.tbPath);
            this.tabFusker.Controls.Add(this.tbURL2);
            this.tabFusker.Controls.Add(this.tbURL);
            this.tabFusker.Controls.Add(this.pictureBox1);
            this.tabFusker.Location = new System.Drawing.Point(4, 22);
            this.tabFusker.Name = "tabFusker";
            this.tabFusker.Padding = new System.Windows.Forms.Padding(3);
            this.tabFusker.Size = new System.Drawing.Size(639, 168);
            this.tabFusker.TabIndex = 0;
            this.tabFusker.Text = "Fusker";
            this.tabFusker.UseVisualStyleBackColor = true;
            // 
            // tabCoedCherry
            // 
            this.tabCoedCherry.Controls.Add(this.textBoxId_CoedCherry);
            this.tabCoedCherry.Controls.Add(this.label4);
            this.tabCoedCherry.Controls.Add(this.downloadCount_CoedCherry);
            this.tabCoedCherry.Controls.Add(this.label3);
            this.tabCoedCherry.Controls.Add(this.buttonDownload_CoedCherry);
            this.tabCoedCherry.Controls.Add(this.label2);
            this.tabCoedCherry.Controls.Add(this.textBoxModelName_CoedCherry);
            this.tabCoedCherry.Location = new System.Drawing.Point(4, 22);
            this.tabCoedCherry.Name = "tabCoedCherry";
            this.tabCoedCherry.Padding = new System.Windows.Forms.Padding(3);
            this.tabCoedCherry.Size = new System.Drawing.Size(639, 168);
            this.tabCoedCherry.TabIndex = 1;
            this.tabCoedCherry.Text = "CoedCherry";
            this.tabCoedCherry.UseVisualStyleBackColor = true;
            // 
            // bAutoDL
            // 
            this.bAutoDL.Enabled = false;
            this.bAutoDL.Location = new System.Drawing.Point(549, 105);
            this.bAutoDL.Name = "bAutoDL";
            this.bAutoDL.Size = new System.Drawing.Size(75, 23);
            this.bAutoDL.TabIndex = 26;
            this.bAutoDL.Text = "Auto DL";
            this.bAutoDL.UseVisualStyleBackColor = true;
            // 
            // bAuto
            // 
            this.bAuto.Location = new System.Drawing.Point(549, 134);
            this.bAuto.Name = "bAuto";
            this.bAuto.Size = new System.Drawing.Size(75, 23);
            this.bAuto.TabIndex = 25;
            this.bAuto.Text = "Auto Fill";
            this.bAuto.UseVisualStyleBackColor = true;
            // 
            // tbWebsite
            // 
            this.tbWebsite.Location = new System.Drawing.Point(12, 136);
            this.tbWebsite.Name = "tbWebsite";
            this.tbWebsite.Size = new System.Drawing.Size(531, 20);
            this.tbWebsite.TabIndex = 14;
            // 
            // tbPath2
            // 
            this.tbPath2.Location = new System.Drawing.Point(96, 105);
            this.tbPath2.Name = "tbPath2";
            this.tbPath2.Size = new System.Drawing.Size(330, 20);
            this.tbPath2.TabIndex = 24;
            // 
            // bSetPath
            // 
            this.bSetPath.Location = new System.Drawing.Point(14, 77);
            this.bSetPath.Name = "bSetPath";
            this.bSetPath.Size = new System.Drawing.Size(75, 23);
            this.bSetPath.TabIndex = 23;
            this.bSetPath.Text = "Set Path";
            this.bSetPath.UseVisualStyleBackColor = true;
            // 
            // ldCount
            // 
            this.ldCount.AutoSize = true;
            this.ldCount.Location = new System.Drawing.Point(589, 82);
            this.ldCount.Name = "ldCount";
            this.ldCount.Size = new System.Drawing.Size(13, 13);
            this.ldCount.TabIndex = 22;
            this.ldCount.Text = "0";
            // 
            // lDownloaded
            // 
            this.lDownloaded.AutoSize = true;
            this.lDownloaded.Location = new System.Drawing.Point(513, 82);
            this.lDownloaded.Name = "lDownloaded";
            this.lDownloaded.Size = new System.Drawing.Size(70, 13);
            this.lDownloaded.TabIndex = 21;
            this.lDownloaded.Text = "Downloaded:";
            // 
            // bDownload
            // 
            this.bDownload.Location = new System.Drawing.Point(432, 78);
            this.bDownload.Name = "bDownload";
            this.bDownload.Size = new System.Drawing.Size(75, 23);
            this.bDownload.TabIndex = 20;
            this.bDownload.Text = "Download";
            this.bDownload.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "URL (+Fusker)";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(96, 79);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(330, 20);
            this.tbPath.TabIndex = 18;
            // 
            // tbURL2
            // 
            this.tbURL2.Location = new System.Drawing.Point(12, 51);
            this.tbURL2.Name = "tbURL2";
            this.tbURL2.Size = new System.Drawing.Size(612, 20);
            this.tbURL2.TabIndex = 16;
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(12, 25);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(612, 20);
            this.tbURL.TabIndex = 17;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(633, 162);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxModelName_CoedCherry
            // 
            this.textBoxModelName_CoedCherry.Location = new System.Drawing.Point(6, 23);
            this.textBoxModelName_CoedCherry.Name = "textBoxModelName_CoedCherry";
            this.textBoxModelName_CoedCherry.Size = new System.Drawing.Size(287, 20);
            this.textBoxModelName_CoedCherry.TabIndex = 0;
            this.textBoxModelName_CoedCherry.Text = "lia-femjoy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Model";
            // 
            // buttonDownload_CoedCherry
            // 
            this.buttonDownload_CoedCherry.Location = new System.Drawing.Point(556, 21);
            this.buttonDownload_CoedCherry.Name = "buttonDownload_CoedCherry";
            this.buttonDownload_CoedCherry.Size = new System.Drawing.Size(75, 23);
            this.buttonDownload_CoedCherry.TabIndex = 2;
            this.buttonDownload_CoedCherry.Text = "Download";
            this.buttonDownload_CoedCherry.UseVisualStyleBackColor = true;
            this.buttonDownload_CoedCherry.Click += new System.EventHandler(this.buttonDownload_CoedCherry_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Downloaded:";
            // 
            // downloadCount_CoedCherry
            // 
            this.downloadCount_CoedCherry.AutoSize = true;
            this.downloadCount_CoedCherry.Location = new System.Drawing.Point(85, 46);
            this.downloadCount_CoedCherry.Name = "downloadCount_CoedCherry";
            this.downloadCount_CoedCherry.Size = new System.Drawing.Size(13, 13);
            this.downloadCount_CoedCherry.TabIndex = 4;
            this.downloadCount_CoedCherry.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(304, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Id";
            // 
            // textBoxId_CoedCherry
            // 
            this.textBoxId_CoedCherry.Location = new System.Drawing.Point(299, 23);
            this.textBoxId_CoedCherry.Name = "textBoxId_CoedCherry";
            this.textBoxId_CoedCherry.Size = new System.Drawing.Size(251, 20);
            this.textBoxId_CoedCherry.TabIndex = 6;
            // 
            // tabTumblr
            // 
            this.tabTumblr.Controls.Add(this.numericOffset_Tumblr);
            this.tabTumblr.Controls.Add(this.label7);
            this.tabTumblr.Controls.Add(this.downloadCount_Tumblr);
            this.tabTumblr.Controls.Add(this.label6);
            this.tabTumblr.Controls.Add(this.textBoxLink_Tumblr);
            this.tabTumblr.Controls.Add(this.label5);
            this.tabTumblr.Controls.Add(this.buttonDownload_Tumblr);
            this.tabTumblr.Location = new System.Drawing.Point(4, 22);
            this.tabTumblr.Name = "tabTumblr";
            this.tabTumblr.Size = new System.Drawing.Size(639, 168);
            this.tabTumblr.TabIndex = 2;
            this.tabTumblr.Text = "Tumblr";
            this.tabTumblr.UseVisualStyleBackColor = true;
            // 
            // buttonDownload_Tumblr
            // 
            this.buttonDownload_Tumblr.Location = new System.Drawing.Point(556, 19);
            this.buttonDownload_Tumblr.Name = "buttonDownload_Tumblr";
            this.buttonDownload_Tumblr.Size = new System.Drawing.Size(75, 23);
            this.buttonDownload_Tumblr.TabIndex = 0;
            this.buttonDownload_Tumblr.Text = "Download";
            this.buttonDownload_Tumblr.UseVisualStyleBackColor = true;
            this.buttonDownload_Tumblr.Click += new System.EventHandler(this.buttonDownload_Tumblr_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Tumblr Name";
            // 
            // textBoxLink_Tumblr
            // 
            this.textBoxLink_Tumblr.Location = new System.Drawing.Point(8, 21);
            this.textBoxLink_Tumblr.Name = "textBoxLink_Tumblr";
            this.textBoxLink_Tumblr.Size = new System.Drawing.Size(375, 20);
            this.textBoxLink_Tumblr.TabIndex = 2;
            this.textBoxLink_Tumblr.Text = "nakedmoondance";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Downloaded:";
            // 
            // downloadCount_Tumblr
            // 
            this.downloadCount_Tumblr.AutoSize = true;
            this.downloadCount_Tumblr.Location = new System.Drawing.Point(89, 47);
            this.downloadCount_Tumblr.Name = "downloadCount_Tumblr";
            this.downloadCount_Tumblr.Size = new System.Drawing.Size(13, 13);
            this.downloadCount_Tumblr.TabIndex = 4;
            this.downloadCount_Tumblr.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(397, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Offset";
            // 
            // numericOffset_Tumblr
            // 
            this.numericOffset_Tumblr.Location = new System.Drawing.Point(390, 21);
            this.numericOffset_Tumblr.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericOffset_Tumblr.Name = "numericOffset_Tumblr";
            this.numericOffset_Tumblr.Size = new System.Drawing.Size(160, 20);
            this.numericOffset_Tumblr.TabIndex = 7;
            // 
            // tabSpecial
            // 
            this.tabSpecial.Controls.Add(this.buttonDownload_tdavis);
            this.tabSpecial.Controls.Add(this.buttonDownload_trishdavis9);
            this.tabSpecial.Location = new System.Drawing.Point(4, 22);
            this.tabSpecial.Name = "tabSpecial";
            this.tabSpecial.Size = new System.Drawing.Size(639, 168);
            this.tabSpecial.TabIndex = 3;
            this.tabSpecial.Text = "Special";
            this.tabSpecial.UseVisualStyleBackColor = true;
            // 
            // buttonDownload_trishdavis9
            // 
            this.buttonDownload_trishdavis9.Location = new System.Drawing.Point(9, 4);
            this.buttonDownload_trishdavis9.Name = "buttonDownload_trishdavis9";
            this.buttonDownload_trishdavis9.Size = new System.Drawing.Size(171, 23);
            this.buttonDownload_trishdavis9.TabIndex = 0;
            this.buttonDownload_trishdavis9.Text = "Download trishdavis9.tumblr.com";
            this.buttonDownload_trishdavis9.UseVisualStyleBackColor = true;
            this.buttonDownload_trishdavis9.Click += new System.EventHandler(this.buttonDownload_trishdavis9_Click);
            // 
            // buttonDownload_tdavis
            // 
            this.buttonDownload_tdavis.Location = new System.Drawing.Point(186, 4);
            this.buttonDownload_tdavis.Name = "buttonDownload_tdavis";
            this.buttonDownload_tdavis.Size = new System.Drawing.Size(171, 23);
            this.buttonDownload_tdavis.TabIndex = 1;
            this.buttonDownload_tdavis.Text = "Download jhphawaii.com/tdavis";
            this.buttonDownload_tdavis.UseVisualStyleBackColor = true;
            this.buttonDownload_tdavis.Click += new System.EventHandler(this.buttonDownload_tdavis_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 194);
            this.Controls.Add(this.tabControlDL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControlDL.ResumeLayout(false);
            this.tabFusker.ResumeLayout(false);
            this.tabFusker.PerformLayout();
            this.tabCoedCherry.ResumeLayout(false);
            this.tabCoedCherry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabTumblr.ResumeLayout(false);
            this.tabTumblr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericOffset_Tumblr)).EndInit();
            this.tabSpecial.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.TabControl tabControlDL;
        private System.Windows.Forms.TabPage tabFusker;
        private System.Windows.Forms.Button bAutoDL;
        private System.Windows.Forms.Button bAuto;
        private System.Windows.Forms.TextBox tbWebsite;
        private System.Windows.Forms.TextBox tbPath2;
        private System.Windows.Forms.Button bSetPath;
        private System.Windows.Forms.Label ldCount;
        private System.Windows.Forms.Label lDownloaded;
        private System.Windows.Forms.Button bDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.TextBox tbURL2;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabCoedCherry;
        private System.Windows.Forms.Button buttonDownload_CoedCherry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxModelName_CoedCherry;
        private System.Windows.Forms.Label downloadCount_CoedCherry;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxId_CoedCherry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabTumblr;
        private System.Windows.Forms.Label downloadCount_Tumblr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxLink_Tumblr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonDownload_Tumblr;
        private System.Windows.Forms.NumericUpDown numericOffset_Tumblr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabSpecial;
        private System.Windows.Forms.Button buttonDownload_trishdavis9;
        private System.Windows.Forms.Button buttonDownload_tdavis;
    }
}

