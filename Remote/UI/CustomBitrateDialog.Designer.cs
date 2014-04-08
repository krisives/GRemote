namespace GRemote
{
    partial class CustomBitrateDialog
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
            this.bitsBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bytesBox = new System.Windows.Forms.NumericUpDown();
            this.megaBitsLabel = new System.Windows.Forms.Label();
            this.megaBytesLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bitsBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bytesBox)).BeginInit();
            this.SuspendLayout();
            // 
            // bitsBox
            // 
            this.bitsBox.Location = new System.Drawing.Point(12, 15);
            this.bitsBox.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.bitsBox.Name = "bitsBox";
            this.bitsBox.Size = new System.Drawing.Size(88, 20);
            this.bitsBox.TabIndex = 0;
            this.bitsBox.Value = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.bitsBox.ValueChanged += new System.EventHandler(this.bitsBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kbps";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(100, 56);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(87, 25);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Change Bitrate";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "KB/s";
            // 
            // bytesBox
            // 
            this.bytesBox.Location = new System.Drawing.Point(143, 15);
            this.bytesBox.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.bytesBox.Name = "bytesBox";
            this.bytesBox.Size = new System.Drawing.Size(88, 20);
            this.bytesBox.TabIndex = 3;
            this.bytesBox.Value = new decimal(new int[] {
            112,
            0,
            0,
            0});
            this.bytesBox.ValueChanged += new System.EventHandler(this.bytesBox_ValueChanged);
            // 
            // megaBitsLabel
            // 
            this.megaBitsLabel.Location = new System.Drawing.Point(12, 38);
            this.megaBitsLabel.Name = "megaBitsLabel";
            this.megaBitsLabel.Size = new System.Drawing.Size(125, 15);
            this.megaBitsLabel.TabIndex = 5;
            this.megaBitsLabel.Text = "X Mbps";
            this.megaBitsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // megaBytesLabel
            // 
            this.megaBytesLabel.Location = new System.Drawing.Point(143, 38);
            this.megaBytesLabel.Name = "megaBytesLabel";
            this.megaBytesLabel.Size = new System.Drawing.Size(125, 15);
            this.megaBytesLabel.TabIndex = 6;
            this.megaBytesLabel.Text = "X MB/s";
            this.megaBytesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(193, 56);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 25);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // CustomBitrateDialog
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 90);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.megaBytesLabel);
            this.Controls.Add(this.megaBitsLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bytesBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bitsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomBitrateDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Custom Bitrate";
            ((System.ComponentModel.ISupportInitialize)(this.bitsBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bytesBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown bitsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown bytesBox;
        private System.Windows.Forms.Label megaBitsLabel;
        private System.Windows.Forms.Label megaBytesLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}