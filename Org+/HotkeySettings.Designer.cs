namespace Org_
{
    partial class HotkeySettings
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
            this.tbHotkey = new System.Windows.Forms.TextBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbHotkey
            // 
            this.tbHotkey.Location = new System.Drawing.Point(12, 12);
            this.tbHotkey.Name = "tbHotkey";
            this.tbHotkey.ReadOnly = true;
            this.tbHotkey.Size = new System.Drawing.Size(260, 20);
            this.tbHotkey.TabIndex = 0;
            this.tbHotkey.Text = "Press Any Key...";
            this.tbHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeySettings_KeyPress);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(12, 55);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(260, 20);
            this.tbPath.TabIndex = 1;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(196, 82);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 2;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Relative Path:";
            // 
            // HotkeySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 117);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.tbHotkey);
            this.KeyPreview = true;
            this.Name = "HotkeySettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "HotkeySettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbHotkey;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Label label1;
    }
}