namespace WIGC
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
            this.bSetPath = new System.Windows.Forms.Button();
            this.bDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // bSetPath
            // 
            this.bSetPath.Location = new System.Drawing.Point(12, 48);
            this.bSetPath.Name = "bSetPath";
            this.bSetPath.Size = new System.Drawing.Size(75, 23);
            this.bSetPath.TabIndex = 14;
            this.bSetPath.Text = "Set Path";
            this.bSetPath.UseVisualStyleBackColor = true;
            this.bSetPath.Click += new System.EventHandler(this.bSetPath_Click);
            // 
            // bDownload
            // 
            this.bDownload.Location = new System.Drawing.Point(430, 49);
            this.bDownload.Name = "bDownload";
            this.bDownload.Size = new System.Drawing.Size(75, 23);
            this.bDownload.TabIndex = 13;
            this.bDownload.Text = "Build";
            this.bDownload.UseVisualStyleBackColor = true;
            this.bDownload.Click += new System.EventHandler(this.bDownload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "URL (+Fusker)";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(94, 50);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(330, 20);
            this.tbPath.TabIndex = 11;
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(12, 24);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(612, 20);
            this.tbURL.TabIndex = 10;
            // 
            // fbd
            // 
            this.fbd.SelectedPath = "C:\\";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 85);
            this.Controls.Add(this.bSetPath);
            this.Controls.Add(this.bDownload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.tbURL);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSetPath;
        private System.Windows.Forms.Button bDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.FolderBrowserDialog fbd;
    }
}

