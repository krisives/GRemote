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
            this.components = new System.ComponentModel.Container();
            this.saveButton = new System.Windows.Forms.Button();
            this.enableSave = new System.Windows.Forms.CheckBox();
            this.saveFilename = new System.Windows.Forms.TextBox();
            this.browseSaveButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ffmpegBox = new System.Windows.Forms.TextBox();
            this.browseFFMpeg = new System.Windows.Forms.Button();
            this.convertFormat = new System.Windows.Forms.CheckBox();
            this.formatBox = new System.Windows.Forms.ComboBox();
            this.helpText = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(312, 110);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(123, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save Preferences";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // enableSave
            // 
            this.enableSave.AutoSize = true;
            this.enableSave.Location = new System.Drawing.Point(11, 37);
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
            this.saveFilename.Location = new System.Drawing.Point(101, 35);
            this.saveFilename.Name = "saveFilename";
            this.saveFilename.Size = new System.Drawing.Size(258, 20);
            this.saveFilename.TabIndex = 3;
            // 
            // browseSaveButton
            // 
            this.browseSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseSaveButton.Enabled = false;
            this.browseSaveButton.Location = new System.Drawing.Point(365, 33);
            this.browseSaveButton.Name = "browseSaveButton";
            this.browseSaveButton.Size = new System.Drawing.Size(75, 23);
            this.browseSaveButton.TabIndex = 4;
            this.browseSaveButton.Text = "Browse";
            this.browseSaveButton.UseVisualStyleBackColor = true;
            this.browseSaveButton.Click += new System.EventHandler(this.browseSaveButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "FFMpeg:";
            // 
            // ffmpegBox
            // 
            this.ffmpegBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ffmpegBox.Location = new System.Drawing.Point(101, 9);
            this.ffmpegBox.Name = "ffmpegBox";
            this.ffmpegBox.Size = new System.Drawing.Size(258, 20);
            this.ffmpegBox.TabIndex = 10;
            this.helpText.SetToolTip(this.ffmpegBox, "If you have a ffmpeg.exe you want to use other than the stock one");
            // 
            // browseFFMpeg
            // 
            this.browseFFMpeg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseFFMpeg.Location = new System.Drawing.Point(365, 6);
            this.browseFFMpeg.Name = "browseFFMpeg";
            this.browseFFMpeg.Size = new System.Drawing.Size(75, 23);
            this.browseFFMpeg.TabIndex = 11;
            this.browseFFMpeg.Text = "Browse";
            this.browseFFMpeg.UseVisualStyleBackColor = true;
            this.browseFFMpeg.Click += new System.EventHandler(this.browseFFMpeg_Click);
            // 
            // convertFormat
            // 
            this.convertFormat.AutoSize = true;
            this.convertFormat.Location = new System.Drawing.Point(101, 61);
            this.convertFormat.Name = "convertFormat";
            this.convertFormat.Size = new System.Drawing.Size(120, 17);
            this.convertFormat.TabIndex = 12;
            this.convertFormat.Text = "Convert File Format:";
            this.helpText.SetToolTip(this.convertFormat, "Convert the saved stream afterwards into a different file format. Streams are\r\nma" +
                    "de in MPEG-TS format but can easily be converted afterwards without re-encoding");
            this.convertFormat.UseVisualStyleBackColor = true;
            // 
            // formatBox
            // 
            this.formatBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatBox.FormattingEnabled = true;
            this.formatBox.Items.AddRange(new object[] {
            "MPEG-TS (.ts)",
            "MPEG-4 (.mp4)",
            "AVI (.avi)",
            "MKV (.mkv)",
            "ASF (.asf)"});
            this.formatBox.Location = new System.Drawing.Point(227, 59);
            this.formatBox.Name = "formatBox";
            this.formatBox.Size = new System.Drawing.Size(132, 21);
            this.formatBox.TabIndex = 13;
            // 
            // PreferencesDialog
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 145);
            this.Controls.Add(this.formatBox);
            this.Controls.Add(this.convertFormat);
            this.Controls.Add(this.browseFFMpeg);
            this.Controls.Add(this.ffmpegBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.browseSaveButton);
            this.Controls.Add(this.saveFilename);
            this.Controls.Add(this.enableSave);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreferencesDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button browseSaveButton;
        public System.Windows.Forms.CheckBox enableSave;
        public System.Windows.Forms.TextBox saveFilename;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox ffmpegBox;
        private System.Windows.Forms.Button browseFFMpeg;
        private System.Windows.Forms.CheckBox convertFormat;
        private System.Windows.Forms.ComboBox formatBox;
        private System.Windows.Forms.ToolTip helpText;
    }
}