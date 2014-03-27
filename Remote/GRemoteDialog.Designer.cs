namespace GRemote
{
    partial class GRemoteDialog
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
            this.snapshotTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gRemoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codecToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xVidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitrateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncompressedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.recordButton = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAreaButton = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bandwidthTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bandwidthLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.videoPreview = new GRemote.VideoPreview();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // snapshotTimer
            // 
            this.snapshotTimer.Interval = 33;
            this.snapshotTimer.Tick += new System.EventHandler(this.snapshotTimer_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gRemoteToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gRemoteToolStripMenuItem
            // 
            this.gRemoteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hostMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator4,
            this.preferencesToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.gRemoteToolStripMenuItem.Name = "gRemoteToolStripMenuItem";
            this.gRemoteToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.gRemoteToolStripMenuItem.Text = "GRemote";
            // 
            // hostMenuItem
            // 
            this.hostMenuItem.Name = "hostMenuItem";
            this.hostMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hostMenuItem.Text = "Host";
            this.hostMenuItem.Click += new System.EventHandler(this.hostMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Connect";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codecToolStripMenuItem,
            this.resolutionToolStripMenuItem,
            this.bitrateToolStripMenuItem,
            this.previewToolStripMenuItem,
            this.toolStripSeparator2,
            this.recordButton,
            this.selectAreaButton});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.videoToolStripMenuItem.Text = "Record";
            // 
            // codecToolStripMenuItem
            // 
            this.codecToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xVidToolStripMenuItem});
            this.codecToolStripMenuItem.Name = "codecToolStripMenuItem";
            this.codecToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.codecToolStripMenuItem.Text = "Codec";
            // 
            // xVidToolStripMenuItem
            // 
            this.xVidToolStripMenuItem.Checked = true;
            this.xVidToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xVidToolStripMenuItem.Name = "xVidToolStripMenuItem";
            this.xVidToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xVidToolStripMenuItem.Text = "XVid";
            // 
            // resolutionToolStripMenuItem
            // 
            this.resolutionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.resolutionToolStripMenuItem.Name = "resolutionToolStripMenuItem";
            this.resolutionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resolutionToolStripMenuItem.Text = "Downscale";
            // 
            // pToolStripMenuItem
            // 
            this.pToolStripMenuItem.Checked = true;
            this.pToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pToolStripMenuItem.Name = "pToolStripMenuItem";
            this.pToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pToolStripMenuItem.Text = "None";
            this.pToolStripMenuItem.Click += new System.EventHandler(this.pToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "480";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "720";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Enabled = false;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem4.Text = "1080";
            // 
            // bitrateToolStripMenuItem
            // 
            this.bitrateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kToolStripMenuItem});
            this.bitrateToolStripMenuItem.Name = "bitrateToolStripMenuItem";
            this.bitrateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bitrateToolStripMenuItem.Text = "Bitrate";
            // 
            // kToolStripMenuItem
            // 
            this.kToolStripMenuItem.Checked = true;
            this.kToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kToolStripMenuItem.Name = "kToolStripMenuItem";
            this.kToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.kToolStripMenuItem.Text = "900Kbps";
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uncompressedToolStripMenuItem,
            this.compressedToolStripMenuItem,
            this.splitViewToolStripMenuItem,
            this.noneToolStripMenuItem});
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            // 
            // uncompressedToolStripMenuItem
            // 
            this.uncompressedToolStripMenuItem.Name = "uncompressedToolStripMenuItem";
            this.uncompressedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.uncompressedToolStripMenuItem.Text = "Uncompressed";
            this.uncompressedToolStripMenuItem.Click += new System.EventHandler(this.uncompressedToolStripMenuItem_Click);
            // 
            // compressedToolStripMenuItem
            // 
            this.compressedToolStripMenuItem.Checked = true;
            this.compressedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.compressedToolStripMenuItem.Name = "compressedToolStripMenuItem";
            this.compressedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.compressedToolStripMenuItem.Text = "Compressed";
            this.compressedToolStripMenuItem.Click += new System.EventHandler(this.compressedToolStripMenuItem_Click);
            // 
            // splitViewToolStripMenuItem
            // 
            this.splitViewToolStripMenuItem.Name = "splitViewToolStripMenuItem";
            this.splitViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.splitViewToolStripMenuItem.Text = "Split View";
            this.splitViewToolStripMenuItem.Click += new System.EventHandler(this.splitViewToolStripMenuItem_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // recordButton
            // 
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(152, 22);
            this.recordButton.Text = "Start Recording";
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // selectAreaButton
            // 
            this.selectAreaButton.Name = "selectAreaButton";
            this.selectAreaButton.Size = new System.Drawing.Size(152, 22);
            this.selectAreaButton.Text = "Select Area";
            this.selectAreaButton.Click += new System.EventHandler(this.selectAreaButton_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripSeparator3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItem5.Text = "Check for Updates";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(160, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // bandwidthTimer
            // 
            this.bandwidthTimer.Interval = 1000;
            this.bandwidthTimer.Tick += new System.EventHandler(this.bandwidthTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.toolStripStatusLabel1,
            this.bandwidthLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(75, 17);
            this.statusLabel.Text = "Not Recording";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(665, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // bandwidthLabel
            // 
            this.bandwidthLabel.Name = "bandwidthLabel";
            this.bandwidthLabel.Size = new System.Drawing.Size(37, 17);
            this.bandwidthLabel.Text = "0 KB/s";
            // 
            // videoPreview
            // 
            this.videoPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.videoPreview.BackColor = System.Drawing.Color.Black;
            this.videoPreview.Location = new System.Drawing.Point(0, 27);
            this.videoPreview.Name = "videoPreview";
            this.videoPreview.PreviewMode = GRemote.PreviewMode.COMPRESSED;
            this.videoPreview.Size = new System.Drawing.Size(792, 521);
            this.videoPreview.TabIndex = 7;
            // 
            // GRemoteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.videoPreview);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GRemoteDialog";
            this.Text = "GRemote";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer snapshotTimer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gRemoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codecToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xVidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem bitrateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Timer bandwidthTimer;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uncompressedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compressedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem recordButton;
        private System.Windows.Forms.ToolStripMenuItem selectAreaButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel bandwidthLabel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem hostMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private VideoPreview videoPreview;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
    }
}

