namespace GRemote
{
    partial class PreferencesDialog
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
            this.button1 = new System.Windows.Forms.Button();
            this.enableSave = new System.Windows.Forms.CheckBox();
            this.saveFilename = new System.Windows.Forms.TextBox();
            this.browseSaveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(336, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // enableSave
            // 
            this.enableSave.AutoSize = true;
            this.enableSave.Location = new System.Drawing.Point(11, 16);
            this.enableSave.Name = "enableSave";
            this.enableSave.Size = new System.Drawing.Size(84, 17);
            this.enableSave.TabIndex = 2;
            this.enableSave.Text = "Save Video:";
            this.enableSave.UseVisualStyleBackColor = true;
            this.enableSave.CheckedChanged += new System.EventHandler(this.enableSave_CheckedChanged);
            // 
            // saveFilename
            // 
            this.saveFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.saveFilename.Enabled = false;
            this.saveFilename.Location = new System.Drawing.Point(101, 14);
            this.saveFilename.Name = "saveFilename";
            this.saveFilename.Size = new System.Drawing.Size(234, 20);
            this.saveFilename.TabIndex = 3;
            // 
            // browseSaveButton
            // 
            this.browseSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseSaveButton.Enabled = false;
            this.browseSaveButton.Location = new System.Drawing.Point(341, 12);
            this.browseSaveButton.Name = "browseSaveButton";
            this.browseSaveButton.Size = new System.Drawing.Size(75, 23);
            this.browseSaveButton.TabIndex = 4;
            this.browseSaveButton.Text = "Browse";
            this.browseSaveButton.UseVisualStyleBackColor = true;
            this.browseSaveButton.Click += new System.EventHandler(this.browseSaveButton_Click);
            // 
            // PreferencesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 294);
            this.Controls.Add(this.browseSaveButton);
            this.Controls.Add(this.saveFilename);
            this.Controls.Add(this.enableSave);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreferencesDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button browseSaveButton;
        public System.Windows.Forms.CheckBox enableSave;
        public System.Windows.Forms.TextBox saveFilename;
    }
}