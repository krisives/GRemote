using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GRemote
{
    public partial class CaptureArea : Form
    {
        GRemoteDialog gRemote;

        public CaptureArea(GRemoteDialog gRemote)
        {
            this.gRemote = gRemote;
            InitializeComponent();
        }

        private void BoundsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        public void updateBoxes()
        {
            widthBox.Text = Width.ToString();
            heightBox.Text = Height.ToString();
        }

        private void BoundsForm_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void BoundsForm_Resize(object sender, EventArgs e)
        {
            updateBoxes();
        }

        private void BoundsForm_Shown(object sender, EventArgs e)
        {
            updateBoxes();
        }

        private void opacityBar_ValueChanged(object sender, EventArgs e)
        {
            Opacity = (opacityBar.Value / 100.0);
        }

        bool moving = false;
        Point grab;

        private void BoundsForm_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void BoundsForm_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void BoundsForm_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void opacityBar_Scroll(object sender, EventArgs e)
        {

        }

        private void innerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moving = true;
                grab = PointToScreen(e.Location);
            }
        }

        private void innerPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                moving = false;
            }
        }

        private void innerPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!moving)
            {
                return;
            }

            Point pos = PointToScreen(e.Location);

            Left += (pos.X - grab.X);
            Top += (pos.Y - grab.Y);

            grab = pos;
        }

        private void BoundsForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void BoundsForm_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void BoundsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                Left += -1;
            }

            if (e.KeyCode == Keys.Right)
            {
                Left += 1;
            }

            if (e.KeyCode == Keys.Down)
            {
                Top += 1;
            }

            if (e.KeyCode == Keys.Up)
            {
                Top += -1;
            }
        }

        private void opacityBar_KeyUp(object sender, KeyEventArgs e)
        {
         //   e.SuppressKeyPress = true;
         //   e.Handled = true;
         //   BoundsForm_KeyUp(this, e);
        }

        private void opacityBar_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            e.Handled = true;
            BoundsForm_KeyDown(this, e);
        }

        private void BoundsForm_Move(object sender, EventArgs e)
        {
            if (Left < 0)
            {
                Left = 0;
                return;
            }

            if (Top < 0)
            {
                Top = 0;
                return;
            }

            if (Top + Height >= SystemInformation.VirtualScreen.Height)
            {
                Top = SystemInformation.VirtualScreen.Height - Height;
                return;
            }

            if (Left + Width >= SystemInformation.VirtualScreen.Width)
            {
                Left = SystemInformation.VirtualScreen.Width - Width;
                return;
            }

            xBox.Text = Left.ToString();
            yBox.Text = Top.ToString();

            gRemote.SetCapturePos(Left, Top);
        }

        public IntPtr TargetWindow
        {
            get
            {
                Point screenPos = PointToScreen(Location);
                POINT p;
                p.X = screenPos.X;
                p.Y = screenPos.Y;
                return WindowFromPoint(p);
            }
        }


        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(POINT Point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }
    }
}
