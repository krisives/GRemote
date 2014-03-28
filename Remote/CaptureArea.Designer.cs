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
            this.widthBox = new System.Windows.Forms.TextBox();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.opacityBar = new System.Windows.Forms.TrackBar();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.yBox = new System.Windows.Forms.TextBox();
            this.xBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.innerPanel = new System.Windows.Forms.Panel();
            this.crosshair = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.innerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(56, 33);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(100, 20);
            this.widthBox.TabIndex = 0;
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(209, 33);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(100, 20);
            this.heightBox.TabIndex = 1;
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
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.yBox);
            this.panel1.Controls.Add(this.xBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.widthBox);
            this.panel1.Controls.Add(this.heightBox);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Location = new System.Drawing.Point(228, 257);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 63);
            this.panel1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Width:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Height:";
            // 
            // yBox
            // 
            this.yBox.Location = new System.Drawing.Point(209, 6);
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(100, 20);
            this.yBox.TabIndex = 6;
            // 
            // xBox
            // 
            this.xBox.Location = new System.Drawing.Point(56, 6);
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(100, 20);
            this.xBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
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
            this.innerPanel.Controls.Add(this.crosshair);
            this.innerPanel.Controls.Add(this.panel1);
            this.innerPanel.Controls.Add(this.opacityBar);
            this.innerPanel.Controls.Add(this.closeButton);
            this.innerPanel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.innerPanel.Location = new System.Drawing.Point(12, 12);
            this.innerPanel.Name = "innerPanel";
            this.innerPanel.Size = new System.Drawing.Size(776, 576);
            this.innerPanel.TabIndex = 6;
            this.innerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.innerPanel_MouseDown);
            this.innerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.innerPanel_MouseMove);
            this.innerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.innerPanel_MouseUp);
            // 
            // crosshair
            // 
            this.crosshair.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.crosshair.AutoSize = true;
            this.crosshair.Location = new System.Drawing.Point(371, 218);
            this.crosshair.Name = "crosshair";
            this.crosshair.Size = new System.Drawing.Size(13, 13);
            this.crosshair.TabIndex = 6;
            this.crosshair.Text = "+";
            // 
            // BoundsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.innerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BoundsForm";
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
            this.Move += new System.EventHandler(this.BoundsForm_Move);
            this.Resize += new System.EventHandler(this.BoundsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.opacityBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.innerPanel.ResumeLayout(false);
            this.innerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.TrackBar opacityBar;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel innerPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox yBox;
        private System.Windows.Forms.TextBox xBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label crosshair;
    }
}