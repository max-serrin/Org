namespace CreateShortcut
{
    partial class Form1
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
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.optionsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addOptionButton = new System.Windows.Forms.Button();
            this.copyTypeFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.targetFileNameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileNameTextBox.Location = new System.Drawing.Point(50, 40);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(541, 20);
            this.fileNameTextBox.TabIndex = 0;
            this.fileNameTextBox.TextChanged += new System.EventHandler(this.fileNameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File";
            // 
            // optionsLayoutPanel
            // 
            this.optionsLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionsLayoutPanel.AutoScroll = true;
            this.optionsLayoutPanel.Location = new System.Drawing.Point(50, 97);
            this.optionsLayoutPanel.Name = "optionsLayoutPanel";
            this.optionsLayoutPanel.Size = new System.Drawing.Size(541, 297);
            this.optionsLayoutPanel.TabIndex = 2;
            this.optionsLayoutPanel.Resize += new System.EventHandler(this.optionsLayoutPanel_Resize);
            // 
            // addOptionButton
            // 
            this.addOptionButton.Location = new System.Drawing.Point(0, 0);
            this.addOptionButton.Name = "addOptionButton";
            this.addOptionButton.Size = new System.Drawing.Size(75, 23);
            this.addOptionButton.TabIndex = 0;
            // 
            // copyTypeFlowLayout
            // 
            this.copyTypeFlowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyTypeFlowLayout.Location = new System.Drawing.Point(50, 400);
            this.copyTypeFlowLayout.Name = "copyTypeFlowLayout";
            this.copyTypeFlowLayout.Size = new System.Drawing.Size(541, 32);
            this.copyTypeFlowLayout.TabIndex = 3;
            // 
            // targetFileNameTextBox
            // 
            this.targetFileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetFileNameTextBox.Location = new System.Drawing.Point(50, 71);
            this.targetFileNameTextBox.Name = "targetFileNameTextBox";
            this.targetFileNameTextBox.Size = new System.Drawing.Size(541, 20);
            this.targetFileNameTextBox.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 453);
            this.Controls.Add(this.targetFileNameTextBox);
            this.Controls.Add(this.copyTypeFlowLayout);
            this.Controls.Add(this.optionsLayoutPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileNameTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Click += new System.EventHandler(this.Form1_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel optionsLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel copyTypeFlowLayout;
        private System.Windows.Forms.TextBox targetFileNameTextBox;
    }
}

