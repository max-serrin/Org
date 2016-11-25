namespace Org_
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
            this.label1 = new System.Windows.Forms.Label();
            this.leftClickFolder = new System.Windows.Forms.TextBox();
            this.leftClickKey = new System.Windows.Forms.TextBox();
            this.rightClickKey = new System.Windows.Forms.TextBox();
            this.rightClickFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.middleClickKey = new System.Windows.Forms.TextBox();
            this.middleClickFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.deleteKey = new System.Windows.Forms.TextBox();
            this.deleteFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left Click:";
            // 
            // leftClickFolder
            // 
            this.leftClickFolder.Location = new System.Drawing.Point(12, 32);
            this.leftClickFolder.Name = "leftClickFolder";
            this.leftClickFolder.Size = new System.Drawing.Size(272, 20);
            this.leftClickFolder.TabIndex = 1;
            this.leftClickFolder.TextChanged += new System.EventHandler(this.leftClickFolder_TextChanged);
            // 
            // leftClickKey
            // 
            this.leftClickKey.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.leftClickKey.Location = new System.Drawing.Point(184, 6);
            this.leftClickKey.MaxLength = 1;
            this.leftClickKey.Name = "leftClickKey";
            this.leftClickKey.ReadOnly = true;
            this.leftClickKey.Size = new System.Drawing.Size(100, 20);
            this.leftClickKey.TabIndex = 2;
            this.leftClickKey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.leftClickKey_MouseClick);
            // 
            // rightClickKey
            // 
            this.rightClickKey.Location = new System.Drawing.Point(184, 68);
            this.rightClickKey.Name = "rightClickKey";
            this.rightClickKey.ReadOnly = true;
            this.rightClickKey.Size = new System.Drawing.Size(100, 20);
            this.rightClickKey.TabIndex = 5;
            this.rightClickKey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rightClickKey_MouseClick);
            // 
            // rightClickFolder
            // 
            this.rightClickFolder.Location = new System.Drawing.Point(12, 94);
            this.rightClickFolder.Name = "rightClickFolder";
            this.rightClickFolder.Size = new System.Drawing.Size(272, 20);
            this.rightClickFolder.TabIndex = 4;
            this.rightClickFolder.TextChanged += new System.EventHandler(this.rightClickFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Right Click:";
            // 
            // middleClickKey
            // 
            this.middleClickKey.Location = new System.Drawing.Point(184, 128);
            this.middleClickKey.Name = "middleClickKey";
            this.middleClickKey.ReadOnly = true;
            this.middleClickKey.Size = new System.Drawing.Size(100, 20);
            this.middleClickKey.TabIndex = 8;
            this.middleClickKey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.middleClickKey_MouseClick);
            // 
            // middleClickFolder
            // 
            this.middleClickFolder.Location = new System.Drawing.Point(12, 154);
            this.middleClickFolder.Name = "middleClickFolder";
            this.middleClickFolder.Size = new System.Drawing.Size(272, 20);
            this.middleClickFolder.TabIndex = 7;
            this.middleClickFolder.TextChanged += new System.EventHandler(this.middleClickFolder_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Middle Click:";
            // 
            // deleteKey
            // 
            this.deleteKey.Location = new System.Drawing.Point(184, 185);
            this.deleteKey.Name = "deleteKey";
            this.deleteKey.ReadOnly = true;
            this.deleteKey.Size = new System.Drawing.Size(100, 20);
            this.deleteKey.TabIndex = 11;
            this.deleteKey.MouseClick += new System.Windows.Forms.MouseEventHandler(this.deleteKey_MouseClick);
            // 
            // deleteFolder
            // 
            this.deleteFolder.Location = new System.Drawing.Point(12, 211);
            this.deleteFolder.Name = "deleteFolder";
            this.deleteFolder.Size = new System.Drawing.Size(272, 20);
            this.deleteFolder.TabIndex = 10;
            this.deleteFolder.TextChanged += new System.EventHandler(this.deleteFolder_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Delete:";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 240);
            this.Controls.Add(this.deleteKey);
            this.Controls.Add(this.deleteFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.middleClickKey);
            this.Controls.Add(this.middleClickFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rightClickKey);
            this.Controls.Add(this.rightClickFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.leftClickKey);
            this.Controls.Add(this.leftClickFolder);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox leftClickFolder;
        private System.Windows.Forms.TextBox leftClickKey;
        private System.Windows.Forms.TextBox rightClickKey;
        private System.Windows.Forms.TextBox rightClickFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox middleClickKey;
        private System.Windows.Forms.TextBox middleClickFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox deleteKey;
        private System.Windows.Forms.TextBox deleteFolder;
        private System.Windows.Forms.Label label4;
    }
}