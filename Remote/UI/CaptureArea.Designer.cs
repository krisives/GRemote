namespace GRemote
{
    partial class CaptureArea
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
            this.opacityBar = new System.Windows.Forms.TrackBar();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.heightBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.innerPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cropBorders = new System.Windows.Forms.CheckBox();
            this.moveButton = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.moveWindowBox = new System.Windows.Forms.RadioButton();
            this.resizeButton = new System.Windows.Forms.Button();
            this.followWindowBox = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.noneWindow = new System.Windows.Forms.RadioButton();
            this.windowByProcess = new System.Windows.Forms.RadioButton();
            this.windowByTitle = new System.Windows.Forms.RadioButton();
            this.processComboBox = new System.Windows.Forms.ComboBox();
            this.windowTitle = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.followTimer = new System.Windows.Forms.Timer(this.components);
            this.xBox = new System.Windows.Forms.NumericUpDown();
            this.yBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
            this.innerPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBox)).BeginInit();
            this.SuspendLayout();
            // 
            // opacityBar
            // 
            this.opacityBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.opacityBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.opacityBar.Location = new System.Drawing.Point(3, 531);
            this.opacityBar.Maximum = 100;
            this.opacityBar.Minimum = 1;
            this.opacityBar.Name = "opacityBar";
            this.opacityBar.Size = new System.Drawing.Size(770, 42);
            this.opacityBar.SmallChange = 5;
            this.opacityBar.TabIndex = 3;
            this.opacityBar.TickFrequency = 10;
            this.opacityBar.Value = 75;
            this.opacityBar.Scroll += new System.EventHandler(this.opacityBar_Scroll);
            this.opacityBar.ValueChanged += new System.EventHandler(this.opacityBar_ValueChanged);
            this.opacityBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opacityBar_KeyDown);
            this.opacityBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.opacityBar_KeyUp);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.closeButton.Location = new System.Drawing.Point(647, 16);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(115, 40);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.yBox);
            this.panel1.Controls.Add(this.xBox);
            this.panel1.Controls.Add(this.heightBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.widthBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Location = new System.Drawing.Point(228, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 63);
            this.panel1.TabIndex = 5;
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(209, 34);
            this.heightBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.heightBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(100, 20);
            this.heightBox.TabIndex = 16;
            this.heightBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.heightBox.ValueChanged += new System.EventHandler(this.heightBox_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Location = new System.Drawing.Point(12, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Width:";
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(56, 34);
            this.widthBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.widthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(100, 20);
            this.widthBox.TabIndex = 15;
            this.widthBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.widthBox.ValueChanged += new System.EventHandler(this.widthBox_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Location = new System.Drawing.Point(162, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Height:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Location = new System.Drawing.Point(186, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Location = new System.Drawing.Point(33, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X:";
            // 
            // innerPanel
            // 
            this.innerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.innerPanel.BackColor = System.Drawing.SystemColors.Control;
            this.innerPanel.Controls.Add(this.groupBox2);
            this.innerPanel.Controls.Add(this.groupBox1);
            this.innerPanel.Controls.Add(this.panel1);
            this.innerPanel.Controls.Add(this.opacityBar);
            this.innerPanel.Controls.Add(this.closeButton);
            this.innerPanel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.innerPanel.Location = new System.Drawing.Point(12, 12);
            this.innerPanel.Name = "innerPanel";
            this.innerPanel.Size = new System.Drawing.Size(776, 576);
            this.innerPanel.TabIndex = 6;
            this.innerPanel.Click += new System.EventHandler(this.innerPanel_Click);
            this.innerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.innerPanel_MouseDown);
            this.innerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.innerPanel_MouseMove);
            this.innerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.innerPanel_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.cropBorders);
            this.groupBox2.Controls.Add(this.moveButton);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.moveWindowBox);
            this.groupBox2.Controls.Add(this.resizeButton);
            this.groupBox2.Controls.Add(this.followWindowBox);
            this.groupBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox2.Location = new System.Drawing.Point(228, 340);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 164);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recording Area";
            // 
            // cropBorders
            // 
            this.cropBorders.AutoSize = true;
            this.cropBorders.Checked = true;
            this.cropBorders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cropBorders.Location = new System.Drawing.Point(12, 128);
            this.cropBorders.Name = "cropBorders";
            this.cropBorders.Size = new System.Drawing.Size(87, 17);
            this.cropBorders.TabIndex = 15;
            this.cropBorders.Text = "Crop Borders";
            this.cropBorders.UseVisualStyleBackColor = true;
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(40, 91);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(120, 23);
            this.moveButton.TabIndex = 14;
            this.moveButton.Text = "Move To Window";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radioButton1.Location = new System.Drawing.Point(12, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(78, 17);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Dont Move";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // moveWindowBox
            // 
            this.moveWindowBox.AutoSize = true;
            this.moveWindowBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.moveWindowBox.Location = new System.Drawing.Point(12, 42);
            this.moveWindowBox.Name = "moveWindowBox";
            this.moveWindowBox.Size = new System.Drawing.Size(120, 17);
            this.moveWindowBox.TabIndex = 11;
            this.moveWindowBox.Text = "Move Window Here";
            this.moveWindowBox.UseVisualStyleBackColor = true;
            // 
            // resizeButton
            // 
            this.resizeButton.Location = new System.Drawing.Point(166, 91);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(128, 23);
            this.resizeButton.TabIndex = 13;
            this.resizeButton.Text = "Resize To Window";
            this.resizeButton.UseVisualStyleBackColor = true;
            this.resizeButton.Click += new System.EventHandler(this.resizeButton_Click);
            // 
            // followWindowBox
            // 
            this.followWindowBox.AutoSize = true;
            this.followWindowBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.followWindowBox.Location = new System.Drawing.Point(12, 65);
            this.followWindowBox.Name = "followWindowBox";
            this.followWindowBox.Size = new System.Drawing.Size(97, 17);
            this.followWindowBox.TabIndex = 12;
            this.followWindowBox.Text = "Follow Window";
            this.followWindowBox.UseVisualStyleBackColor = true;
            this.followWindowBox.CheckedChanged += new System.EventHandler(this.followWindowBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.noneWindow);
            this.groupBox1.Controls.Add(this.windowByProcess);
            this.groupBox1.Controls.Add(this.windowByTitle);
            this.groupBox1.Controls.Add(this.processComboBox);
            this.groupBox1.Controls.Add(this.windowTitle);
            this.groupBox1.Controls.Add(this.refreshButton);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.Location = new System.Drawing.Point(228, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 169);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Window";
            // 
            // noneWindow
            // 
            this.noneWindow.AutoSize = true;
            this.noneWindow.Checked = true;
            this.noneWindow.Location = new System.Drawing.Point(6, 23);
            this.noneWindow.Name = "noneWindow";
            this.noneWindow.Size = new System.Drawing.Size(51, 17);
            this.noneWindow.TabIndex = 19;
            this.noneWindow.TabStop = true;
            this.noneWindow.Text = "None";
            this.noneWindow.UseVisualStyleBackColor = true;
            this.noneWindow.CheckedChanged += new System.EventHandler(this.noneWindow_CheckedChanged);
            // 
            // windowByProcess
            // 
            this.windowByProcess.AutoSize = true;
            this.windowByProcess.Location = new System.Drawing.Point(6, 52);
            this.windowByProcess.Name = "windowByProcess";
            this.windowByProcess.Size = new System.Drawing.Size(119, 17);
            this.windowByProcess.TabIndex = 17;
            this.windowByProcess.Text = "Window by Process";
            this.windowByProcess.UseVisualStyleBackColor = true;
            this.windowByProcess.CheckedChanged += new System.EventHandler(this.windowByProcess_CheckedChanged);
            // 
            // windowByTitle
            // 
            this.windowByTitle.AutoSize = true;
            this.windowByTitle.Location = new System.Drawing.Point(6, 112);
            this.windowByTitle.Name = "windowByTitle";
            this.windowByTitle.Size = new System.Drawing.Size(101, 17);
            this.windowByTitle.TabIndex = 18;
            this.windowByTitle.Text = "Window by Title";
            this.windowByTitle.UseVisualStyleBackColor = true;
            this.windowByTitle.CheckedChanged += new System.EventHandler(this.windowByTitle_CheckedChanged);
            // 
            // processComboBox
            // 
            this.processComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.processComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.processComboBox.FormattingEnabled = true;
            this.processComboBox.Location = new System.Drawing.Point(6, 75);
            this.processComboBox.Name = "processComboBox";
            this.processComboBox.Size = new System.Drawing.Size(213, 21);
            this.processComboBox.TabIndex = 7;
            this.processComboBox.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.processComboBox_Format);
            this.processComboBox.SelectedValueChanged += new System.EventHandler(this.processComboBox_SelectedValueChanged);
            // 
            // windowTitle
            // 
            this.windowTitle.Location = new System.Drawing.Point(6, 135);
            this.windowTitle.Name = "windowTitle";
            this.windowTitle.Size = new System.Drawing.Size(303, 20);
            this.windowTitle.TabIndex = 17;
            this.windowTitle.TextChanged += new System.EventHandler(this.windowTitle_TextChanged);
            // 
            // refreshButton
            // 
            this.refreshButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.refreshButton.Location = new System.Drawing.Point(225, 73);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(84, 23);
            this.refreshButton.TabIndex = 8;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // followTimer
            // 
            this.followTimer.Interval = 500;
            this.followTimer.Tick += new System.EventHandler(this.FollowWindowTick);
            // 
            // xBox
            // 
            this.xBox.Location = new System.Drawing.Point(56, 7);
            this.xBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.xBox.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(100, 20);
            this.xBox.TabIndex = 21;
            this.xBox.ValueChanged += new System.EventHandler(this.xBox_ValueChanged);
            // 
            // yBox
            // 
            this.yBox.Location = new System.Drawing.Point(209, 7);
            this.yBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.yBox.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(100, 20);
            this.yBox.TabIndex = 22;
            this.yBox.ValueChanged += new System.EventHandler(this.yBox_ValueChanged);
            // 
            // CaptureArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.innerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CaptureArea";
            this.Opacity = 0.75D;
            this.Text = "Recording Bounds";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoundsForm_FormClosing);
            this.Shown += new System.EventHandler(this.BoundsForm_Shown);
            this.ResizeEnd += new System.EventHandler(this.BoundsForm_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoundsForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BoundsForm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BoundsForm_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoundsForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BoundsForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BoundsForm_MouseUp);
            this.Move += new System.EventHandler(this.OnWindowMoved);
            this.Resize += new System.EventHandler(this.BoundsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
            this.innerPanel.ResumeLayout(false);
            this.innerPanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar opacityBar;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel innerPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox processComboBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.RadioButton followWindowBox;
        private System.Windows.Forms.RadioButton moveWindowBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button resizeButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.NumericUpDown heightBox;
        private System.Windows.Forms.NumericUpDown widthBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton windowByProcess;
        private System.Windows.Forms.RadioButton windowByTitle;
        private System.Windows.Forms.TextBox windowTitle;
        private System.Windows.Forms.RadioButton noneWindow;
        private System.Windows.Forms.Timer followTimer;
        private System.Windows.Forms.CheckBox cropBorders;
        private System.Windows.Forms.NumericUpDown yBox;
        private System.Windows.Forms.NumericUpDown xBox;
    }
}