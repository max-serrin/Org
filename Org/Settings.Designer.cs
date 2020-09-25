namespace Org
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
            this.settingsContainer = new System.Windows.Forms.SplitContainer();
            this.settingsList = new System.Windows.Forms.ListBox();
            this.settingsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.settingsContainer)).BeginInit();
            this.settingsContainer.Panel1.SuspendLayout();
            this.settingsContainer.Panel2.SuspendLayout();
            this.settingsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsContainer
            // 
            this.settingsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsContainer.Location = new System.Drawing.Point(0, 0);
            this.settingsContainer.Name = "settingsContainer";
            // 
            // settingsContainer.Panel1
            // 
            this.settingsContainer.Panel1.Controls.Add(this.settingsList);
            // 
            // settingsContainer.Panel2
            // 
            this.settingsContainer.Panel2.Controls.Add(this.settingsLayoutPanel);
            this.settingsContainer.Size = new System.Drawing.Size(403, 444);
            this.settingsContainer.SplitterDistance = 83;
            this.settingsContainer.TabIndex = 0;
            // 
            // settingsList
            // 
            this.settingsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsList.FormattingEnabled = true;
            this.settingsList.Location = new System.Drawing.Point(0, 0);
            this.settingsList.Name = "settingsList";
            this.settingsList.Size = new System.Drawing.Size(83, 444);
            this.settingsList.TabIndex = 0;
            this.settingsList.SelectedIndexChanged += new System.EventHandler(this.SettingsList_SelectedIndexChanged);
            // 
            // settingsLayoutPanel
            // 
            this.settingsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsLayoutPanel.Name = "settingsLayoutPanel";
            this.settingsLayoutPanel.Size = new System.Drawing.Size(316, 444);
            this.settingsLayoutPanel.TabIndex = 0;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 444);
            this.Controls.Add(this.settingsContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.settingsContainer.Panel1.ResumeLayout(false);
            this.settingsContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.settingsContainer)).EndInit();
            this.settingsContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer settingsContainer;
        private System.Windows.Forms.ListBox settingsList;
        private System.Windows.Forms.FlowLayoutPanel settingsLayoutPanel;
    }
}