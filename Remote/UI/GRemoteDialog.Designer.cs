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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GRemoteDialog));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.gRemoteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.prefsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codecItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x264Item = new System.Windows.Forms.ToolStripMenuItem();
            this.codecItem265 = new System.Windows.Forms.ToolStripMenuItem();
            this.xvidItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codecMpeg2 = new System.Windows.Forms.ToolStripMenuItem();
            this.codecMSMpeg4 = new System.Windows.Forms.ToolStripMenuItem();
            this.theoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fpsItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fpsItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.fpsItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.fpsItem20 = new System.Windows.Forms.ToolStripMenuItem();
            this.fpsItem25 = new System.Windows.Forms.ToolStripMenuItem();
            this.fpsItem30 = new System.Windows.Forms.ToolStripMenuItem();
            this.fpsItem60 = new System.Windows.Forms.ToolStripMenuItem();
            this.downscaleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitrateItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitrateItem1200 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitrateItem900 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitrateItem600 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitrateItem300 = new System.Windows.Forms.ToolStripMenuItem();
            this.bitrateItemCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.previewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncompressedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compressedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAreaItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playbackItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stretchItem = new System.Windows.Forms.ToolStripMenuItem();
            this.macroItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMacroItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopMacroItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.PlayMacroItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.customizeMacroItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkUpdatesItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bandwidthTimer = new System.Windows.Forms.Timer(this.components);
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bandwidthLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.inputDropdown = new System.Windows.Forms.ToolStripDropDownButton();
            this.noInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desktopInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playbackTimer = new System.Windows.Forms.Timer(this.components);
            this.videoScreen = new GRemote.VideoScreen();
            this.menu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gRemoteItem,
            this.recordItem,
            this.playbackItem,
            this.macroItem,
            this.helpItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(792, 24);
            this.menu.TabIndex = 5;
            this.menu.Text = "menuStrip1";
            // 
            // gRemoteItem
            // 
            this.gRemoteItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hostItem,
            this.connectItem,
            this.toolStripSeparator4,
            this.prefsItem,
            this.toolStripSeparator1,
            this.exitItem});
            this.gRemoteItem.Name = "gRemoteItem";
            this.gRemoteItem.Size = new System.Drawing.Size(63, 20);
            this.gRemoteItem.Text = "GRemote";
            // 
            // hostItem
            // 
            this.hostItem.Name = "hostItem";
            this.hostItem.Size = new System.Drawing.Size(132, 22);
            this.hostItem.Text = "Host";
            this.hostItem.Click += new System.EventHandler(this.hostMenuItem_Click);
            // 
            // connectItem
            // 
            this.connectItem.Name = "connectItem";
            this.connectItem.Size = new System.Drawing.Size(132, 22);
            this.connectItem.Text = "Connect";
            this.connectItem.Click += new System.EventHandler(this.joinMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(129, 6);
            // 
            // prefsItem
            // 
            this.prefsItem.Name = "prefsItem";
            this.prefsItem.Size = new System.Drawing.Size(132, 22);
            this.prefsItem.Text = "Preferences";
            this.prefsItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(129, 6);
            // 
            // exitItem
            // 
            this.exitItem.Name = "exitItem";
            this.exitItem.Size = new System.Drawing.Size(132, 22);
            this.exitItem.Text = "Exit";
            this.exitItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // recordItem
            // 
            this.recordItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codecItem,
            this.fpsItem,
            this.downscaleItem,
            this.bitrateItem,
            this.previewItem,
            this.toolStripSeparator2,
            this.selectAreaItem});
            this.recordItem.Name = "recordItem";
            this.recordItem.Size = new System.Drawing.Size(53, 20);
            this.recordItem.Text = "Record";
            // 
            // codecItem
            // 
            this.codecItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x264Item,
            this.codecItem265,
            this.xvidItem,
            this.codecMpeg2,
            this.codecMSMpeg4,
            this.theoraToolStripMenuItem});
            this.codecItem.Name = "codecItem";
            this.codecItem.Size = new System.Drawing.Size(152, 22);
            this.codecItem.Text = "Codec";
            // 
            // x264Item
            // 
            this.x264Item.Checked = true;
            this.x264Item.CheckState = System.Windows.Forms.CheckState.Checked;
            this.x264Item.Name = "x264Item";
            this.x264Item.Size = new System.Drawing.Size(119, 22);
            this.x264Item.Tag = "libx264";
            this.x264Item.Text = "x264";
            this.x264Item.Click += new System.EventHandler(this.codecItem_Click);
            // 
            // codecItem265
            // 
            this.codecItem265.Name = "codecItem265";
            this.codecItem265.Size = new System.Drawing.Size(119, 22);
            this.codecItem265.Tag = "libx265";
            this.codecItem265.Text = "x265";
            // 
            // xvidItem
            // 
            this.xvidItem.Name = "xvidItem";
            this.xvidItem.Size = new System.Drawing.Size(119, 22);
            this.xvidItem.Tag = "libxvid";
            this.xvidItem.Text = "xvid";
            this.xvidItem.Click += new System.EventHandler(this.codecItem_Click);
            // 
            // codecMpeg2
            // 
            this.codecMpeg2.Name = "codecMpeg2";
            this.codecMpeg2.Size = new System.Drawing.Size(119, 22);
            this.codecMpeg2.Tag = "mpeg2video";
            this.codecMpeg2.Text = "mpeg2";
            // 
            // codecMSMpeg4
            // 
            this.codecMSMpeg4.Name = "codecMSMpeg4";
            this.codecMSMpeg4.Size = new System.Drawing.Size(119, 22);
            this.codecMSMpeg4.Tag = "msmpeg4v3";
            this.codecMSMpeg4.Text = "msmpeg4";
            // 
            // theoraToolStripMenuItem
            // 
            this.theoraToolStripMenuItem.Name = "theoraToolStripMenuItem";
            this.theoraToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.theoraToolStripMenuItem.Tag = "theora";
            this.theoraToolStripMenuItem.Text = "theora";
            this.theoraToolStripMenuItem.Click += new System.EventHandler(this.codecItem_Click);
            // 
            // fpsItem
            // 
            this.fpsItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fpsItem10,
            this.fpsItem15,
            this.fpsItem20,
            this.fpsItem25,
            this.fpsItem30,
            this.fpsItem60});
            this.fpsItem.Name = "fpsItem";
            this.fpsItem.Size = new System.Drawing.Size(152, 22);
            this.fpsItem.Text = "Framerate";
            // 
            // fpsItem10
            // 
            this.fpsItem10.Name = "fpsItem10";
            this.fpsItem10.Size = new System.Drawing.Size(86, 22);
            this.fpsItem10.Tag = "10";
            this.fpsItem10.Text = "10";
            // 
            // fpsItem15
            // 
            this.fpsItem15.Name = "fpsItem15";
            this.fpsItem15.Size = new System.Drawing.Size(86, 22);
            this.fpsItem15.Tag = "15";
            this.fpsItem15.Text = "15";
            // 
            // fpsItem20
            // 
            this.fpsItem20.Name = "fpsItem20";
            this.fpsItem20.Size = new System.Drawing.Size(86, 22);
            this.fpsItem20.Tag = "20";
            this.fpsItem20.Text = "20";
            // 
            // fpsItem25
            // 
            this.fpsItem25.Name = "fpsItem25";
            this.fpsItem25.Size = new System.Drawing.Size(86, 22);
            this.fpsItem25.Tag = "25";
            this.fpsItem25.Text = "25";
            // 
            // fpsItem30
            // 
            this.fpsItem30.Checked = true;
            this.fpsItem30.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fpsItem30.Name = "fpsItem30";
            this.fpsItem30.Size = new System.Drawing.Size(86, 22);
            this.fpsItem30.Tag = "30";
            this.fpsItem30.Text = "30";
            // 
            // fpsItem60
            // 
            this.fpsItem60.Name = "fpsItem60";
            this.fpsItem60.Size = new System.Drawing.Size(86, 22);
            this.fpsItem60.Tag = "60";
            this.fpsItem60.Text = "60";
            // 
            // downscaleItem
            // 
            this.downscaleItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.downscaleItem.Name = "downscaleItem";
            this.downscaleItem.Size = new System.Drawing.Size(152, 22);
            this.downscaleItem.Text = "Downscale";
            // 
            // pToolStripMenuItem
            // 
            this.pToolStripMenuItem.Checked = true;
            this.pToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pToolStripMenuItem.Name = "pToolStripMenuItem";
            this.pToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.pToolStripMenuItem.Text = "None";
            this.pToolStripMenuItem.Click += new System.EventHandler(this.pToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem2.Text = "480";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem3.Text = "720";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Enabled = false;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(99, 22);
            this.toolStripMenuItem4.Text = "1080";
            // 
            // bitrateItem
            // 
            this.bitrateItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bitrateItem1200,
            this.bitrateItem900,
            this.bitrateItem600,
            this.bitrateItem300,
            this.bitrateItemCustom});
            this.bitrateItem.Name = "bitrateItem";
            this.bitrateItem.Size = new System.Drawing.Size(152, 22);
            this.bitrateItem.Text = "Bitrate";
            // 
            // bitrateItem1200
            // 
            this.bitrateItem1200.Name = "bitrateItem1200";
            this.bitrateItem1200.Size = new System.Drawing.Size(171, 22);
            this.bitrateItem1200.Tag = "1200";
            this.bitrateItem1200.Text = "1.2 Mbps (150 KB/s)";
            this.bitrateItem1200.Click += new System.EventHandler(this.bitrateItem_Click);
            // 
            // bitrateItem900
            // 
            this.bitrateItem900.Checked = true;
            this.bitrateItem900.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bitrateItem900.Name = "bitrateItem900";
            this.bitrateItem900.Size = new System.Drawing.Size(171, 22);
            this.bitrateItem900.Tag = "900";
            this.bitrateItem900.Text = "900 Kbps (112 KB/s)";
            this.bitrateItem900.Click += new System.EventHandler(this.bitrateItem_Click);
            // 
            // bitrateItem600
            // 
            this.bitrateItem600.Name = "bitrateItem600";
            this.bitrateItem600.Size = new System.Drawing.Size(171, 22);
            this.bitrateItem600.Tag = "600";
            this.bitrateItem600.Text = "600 Kbps (75 KB/s)";
            this.bitrateItem600.Click += new System.EventHandler(this.bitrateItem_Click);
            // 
            // bitrateItem300
            // 
            this.bitrateItem300.Name = "bitrateItem300";
            this.bitrateItem300.Size = new System.Drawing.Size(171, 22);
            this.bitrateItem300.Tag = "300";
            this.bitrateItem300.Text = "300 Kbps (38 KB/s)";
            this.bitrateItem300.Click += new System.EventHandler(this.bitrateItem_Click);
            // 
            // bitrateItemCustom
            // 
            this.bitrateItemCustom.Name = "bitrateItemCustom";
            this.bitrateItemCustom.Size = new System.Drawing.Size(171, 22);
            this.bitrateItemCustom.Tag = "0";
            this.bitrateItemCustom.Text = "Custom";
            this.bitrateItemCustom.Click += new System.EventHandler(this.bitrateItemCustom_Click);
            // 
            // previewItem
            // 
            this.previewItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uncompressedToolStripMenuItem,
            this.compressedToolStripMenuItem,
            this.splitViewToolStripMenuItem,
            this.noneToolStripMenuItem});
            this.previewItem.Name = "previewItem";
            this.previewItem.Size = new System.Drawing.Size(152, 22);
            this.previewItem.Text = "Preview";
            // 
            // uncompressedToolStripMenuItem
            // 
            this.uncompressedToolStripMenuItem.Name = "uncompressedToolStripMenuItem";
            this.uncompressedToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.uncompressedToolStripMenuItem.Text = "Uncompressed";
            this.uncompressedToolStripMenuItem.Click += new System.EventHandler(this.uncompressedToolStripMenuItem_Click);
            // 
            // compressedToolStripMenuItem
            // 
            this.compressedToolStripMenuItem.Checked = true;
            this.compressedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.compressedToolStripMenuItem.Name = "compressedToolStripMenuItem";
            this.compressedToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.compressedToolStripMenuItem.Text = "Compressed";
            this.compressedToolStripMenuItem.Click += new System.EventHandler(this.compressedToolStripMenuItem_Click);
            // 
            // splitViewToolStripMenuItem
            // 
            this.splitViewToolStripMenuItem.Name = "splitViewToolStripMenuItem";
            this.splitViewToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.splitViewToolStripMenuItem.Text = "Split View";
            this.splitViewToolStripMenuItem.Click += new System.EventHandler(this.splitViewToolStripMenuItem_Click);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // selectAreaItem
            // 
            this.selectAreaItem.Name = "selectAreaItem";
            this.selectAreaItem.Size = new System.Drawing.Size(152, 22);
            this.selectAreaItem.Text = "Select Area";
            this.selectAreaItem.Click += new System.EventHandler(this.selectAreaButton_Click);
            // 
            // playbackItem
            // 
            this.playbackItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centerItem,
            this.stretchItem});
            this.playbackItem.Name = "playbackItem";
            this.playbackItem.Size = new System.Drawing.Size(61, 20);
            this.playbackItem.Text = "Playback";
            // 
            // centerItem
            // 
            this.centerItem.Checked = true;
            this.centerItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.centerItem.Name = "centerItem";
            this.centerItem.Size = new System.Drawing.Size(152, 22);
            this.centerItem.Text = "Center";
            this.centerItem.Click += new System.EventHandler(this.centerItem_Click);
            // 
            // stretchItem
            // 
            this.stretchItem.Name = "stretchItem";
            this.stretchItem.Size = new System.Drawing.Size(152, 22);
            this.stretchItem.Text = "Stretched";
            this.stretchItem.Click += new System.EventHandler(this.stretchItem_Click);
            // 
            // macroItem
            // 
            this.macroItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startMacroItem,
            this.stopMacroItem,
            this.toolStripSeparator5,
            this.PlayMacroItem});
            this.macroItem.Name = "macroItem";
            this.macroItem.Size = new System.Drawing.Size(48, 20);
            this.macroItem.Text = "Macro";
            // 
            // startMacroItem
            // 
            this.startMacroItem.Name = "startMacroItem";
            this.startMacroItem.Size = new System.Drawing.Size(152, 22);
            this.startMacroItem.Text = "Start Recording";
            // 
            // stopMacroItem
            // 
            this.stopMacroItem.Name = "stopMacroItem";
            this.stopMacroItem.Size = new System.Drawing.Size(152, 22);
            this.stopMacroItem.Text = "Stop Recording";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // PlayMacroItem
            // 
            this.PlayMacroItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator6,
            this.customizeMacroItem});
            this.PlayMacroItem.Name = "PlayMacroItem";
            this.PlayMacroItem.Size = new System.Drawing.Size(152, 22);
            this.PlayMacroItem.Text = "Play Macro";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(157, 6);
            // 
            // customizeMacroItem
            // 
            this.customizeMacroItem.Name = "customizeMacroItem";
            this.customizeMacroItem.Size = new System.Drawing.Size(160, 22);
            this.customizeMacroItem.Text = "Customize Macros";
            // 
            // helpItem
            // 
            this.helpItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkUpdatesItem,
            this.toolStripSeparator3,
            this.aboutItem});
            this.helpItem.Name = "helpItem";
            this.helpItem.Size = new System.Drawing.Size(40, 20);
            this.helpItem.Text = "Help";
            // 
            // checkUpdatesItem
            // 
            this.checkUpdatesItem.Name = "checkUpdatesItem";
            this.checkUpdatesItem.Size = new System.Drawing.Size(163, 22);
            this.checkUpdatesItem.Text = "Check for Updates";
            this.checkUpdatesItem.Click += new System.EventHandler(this.checkUpdatesItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(160, 6);
            // 
            // aboutItem
            // 
            this.aboutItem.Name = "aboutItem";
            this.aboutItem.Size = new System.Drawing.Size(163, 22);
            this.aboutItem.Text = "About";
            this.aboutItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // bandwidthTimer
            // 
            this.bandwidthTimer.Enabled = true;
            this.bandwidthTimer.Interval = 1000;
            this.bandwidthTimer.Tick += new System.EventHandler(this.bandwidthTimer_Tick);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.toolStripStatusLabel1,
            this.bandwidthLabel,
            this.inputDropdown});
            this.statusBar.Location = new System.Drawing.Point(0, 551);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(792, 22);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(75, 17);
            this.statusLabel.Text = "Not Recording";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(603, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // bandwidthLabel
            // 
            this.bandwidthLabel.Name = "bandwidthLabel";
            this.bandwidthLabel.Size = new System.Drawing.Size(37, 17);
            this.bandwidthLabel.Text = "0 KB/s";
            // 
            // inputDropdown
            // 
            this.inputDropdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.inputDropdown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noInputToolStripMenuItem,
            this.windowInputToolStripMenuItem,
            this.desktopInputToolStripMenuItem});
            this.inputDropdown.Image = ((System.Drawing.Image)(resources.GetObject("inputDropdown.Image")));
            this.inputDropdown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.inputDropdown.Name = "inputDropdown";
            this.inputDropdown.Size = new System.Drawing.Size(62, 20);
            this.inputDropdown.Text = "No Input";
            // 
            // noInputToolStripMenuItem
            // 
            this.noInputToolStripMenuItem.Checked = true;
            this.noInputToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noInputToolStripMenuItem.Name = "noInputToolStripMenuItem";
            this.noInputToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.noInputToolStripMenuItem.Text = "No Input";
            // 
            // windowInputToolStripMenuItem
            // 
            this.windowInputToolStripMenuItem.Name = "windowInputToolStripMenuItem";
            this.windowInputToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.windowInputToolStripMenuItem.Text = "Window Input";
            // 
            // desktopInputToolStripMenuItem
            // 
            this.desktopInputToolStripMenuItem.Enabled = false;
            this.desktopInputToolStripMenuItem.Name = "desktopInputToolStripMenuItem";
            this.desktopInputToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.desktopInputToolStripMenuItem.Text = "Desktop Input";
            // 
            // playbackTimer
            // 
            this.playbackTimer.Interval = 33;
            // 
            // videoScreen
            // 
            this.videoScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.videoScreen.BackColor = System.Drawing.Color.Black;
            this.videoScreen.GRemote = null;
            this.videoScreen.Location = new System.Drawing.Point(0, 27);
            this.videoScreen.Name = "videoScreen";
            this.videoScreen.PreviewMode = GRemote.PreviewMode.COMPRESSED;
            this.videoScreen.ScaleMode = GRemote.ScaleMode.CENTER;
            this.videoScreen.Size = new System.Drawing.Size(792, 521);
            this.videoScreen.TabIndex = 7;
            // 
            // GRemoteDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.videoScreen);
            this.MainMenuStrip = this.menu;
            this.Name = "GRemoteDialog";
            this.Text = "GRemote (x.y.z)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GRemoteDialog_FormClosed);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem gRemoteItem;
        private System.Windows.Forms.ToolStripMenuItem prefsItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitItem;
        private System.Windows.Forms.ToolStripMenuItem recordItem;
        private System.Windows.Forms.ToolStripMenuItem helpItem;
        private System.Windows.Forms.ToolStripMenuItem codecItem;
        private System.Windows.Forms.ToolStripMenuItem xvidItem;
        private System.Windows.Forms.ToolStripMenuItem downscaleItem;
        private System.Windows.Forms.ToolStripMenuItem pToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem bitrateItem;
        private System.Windows.Forms.ToolStripMenuItem bitrateItem900;
        private System.Windows.Forms.ToolStripMenuItem aboutItem;
        private System.Windows.Forms.Timer bandwidthTimer;
        private System.Windows.Forms.ToolStripMenuItem previewItem;
        private System.Windows.Forms.ToolStripMenuItem uncompressedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compressedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem selectAreaItem;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel bandwidthLabel;
        private System.Windows.Forms.ToolStripMenuItem checkUpdatesItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem hostItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private VideoScreen videoScreen;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.Timer playbackTimer;
        private System.Windows.Forms.ToolStripMenuItem x264Item;
        private System.Windows.Forms.ToolStripMenuItem theoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitrateItem1200;
        private System.Windows.Forms.ToolStripMenuItem bitrateItem600;
        private System.Windows.Forms.ToolStripMenuItem bitrateItem300;
        private System.Windows.Forms.ToolStripMenuItem fpsItem;
        private System.Windows.Forms.ToolStripMenuItem fpsItem10;
        private System.Windows.Forms.ToolStripMenuItem fpsItem15;
        private System.Windows.Forms.ToolStripMenuItem fpsItem20;
        private System.Windows.Forms.ToolStripMenuItem fpsItem25;
        private System.Windows.Forms.ToolStripMenuItem fpsItem30;
        private System.Windows.Forms.ToolStripMenuItem fpsItem60;
        private System.Windows.Forms.ToolStripMenuItem bitrateItemCustom;
        private System.Windows.Forms.ToolStripMenuItem codecItem265;
        private System.Windows.Forms.ToolStripMenuItem codecMpeg2;
        private System.Windows.Forms.ToolStripMenuItem codecMSMpeg4;
        private System.Windows.Forms.ToolStripDropDownButton inputDropdown;
        private System.Windows.Forms.ToolStripMenuItem noInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desktopInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playbackItem;
        private System.Windows.Forms.ToolStripMenuItem centerItem;
        private System.Windows.Forms.ToolStripMenuItem stretchItem;
        private System.Windows.Forms.ToolStripMenuItem splitViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem macroItem;
        private System.Windows.Forms.ToolStripMenuItem startMacroItem;
        private System.Windows.Forms.ToolStripMenuItem stopMacroItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem PlayMacroItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem customizeMacroItem;
    }
}

