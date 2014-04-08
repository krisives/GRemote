using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GRemote
{
    public partial class CaptureArea : Form
    {
        private GRemoteDialog gRemote;
        private bool moving = false;
        private Point grab;

        public CaptureArea(GRemoteDialog gRemote)
        {
            this.gRemote = gRemote;
            InitializeComponent();
        }

        public IntPtr TargetWindow
        {
            get
            {
                if (windowByProcess.Checked)
                {
                    return GetWindowByProcess();
                }

                if (windowByTitle.Checked)
                {
                    return GetWindowByTitle();
                }

                return IntPtr.Zero;
            }
        }

        public IntPtr GetWindowByProcess()
        {
            Process p = null;

            if (processComboBox.SelectedItem != null)
            {
                if (processComboBox.SelectedItem.GetType() == typeof(Process))
                {
                    p = processComboBox.SelectedItem as Process;
                }
            }

            if (p != null)
            {
                return p.MainWindowHandle;
            }

            return IntPtr.Zero;
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
            RefreshProcessList();
        }

        private void opacityBar_ValueChanged(object sender, EventArgs e)
        {
            Opacity = (opacityBar.Value / 100.0);
        }

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

        private void OnWindowMoved(object sender, EventArgs e)
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

            if ((Width % 2) == 1)
            {
                Width++;
                return;
            }

            if ((Height % 2) == 1)
            {
                Height++;
                return;
            }

            xBox.Text = Left.ToString();
            yBox.Text = Top.ToString();

            gRemote.SetCapturePos(Left, Top);
            MoveTargetWindow();
        }

        public void MoveTargetWindow()
        {
            if (!moveWindowBox.Checked) {
                return;
            }
            
            IntPtr hwnd = TargetWindow;
            int dx = 0, dy = 0;

            if (cropBorders.Checked)
            {
                dx = 3;
                dy = 22;
            }

            if (hwnd != IntPtr.Zero)
            {
                SetWindowPos(hwnd, (IntPtr)0, Left - dx, Top - dy, 0, 0, SetWindowPosFlags.DoNotActivate | SetWindowPosFlags.IgnoreResize);
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshProcessList();
        }

        public void RefreshProcessList()
        {
            Process[] all = Process.GetProcesses();
            
            Array.Sort(all, delegate(Process a, Process b)
            {
                return a.ProcessName.CompareTo(b.ProcessName);
            });

            processComboBox.Items.Clear();

            foreach (Process p in all)
            {
                if (p.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }

                processComboBox.Items.Add(p);
            }
        }

        private void innerPanel_Click(object sender, EventArgs e)
        {
            // Removes focus from the control so that the keyboard
            // can be used to nudge it around
            ActiveControl = null;
        }

        private void processComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = (e.ListItem as Process).ProcessName;
        }

        private void resizeButton_Click(object sender, EventArgs e)
        {
            ResizeToTargetWindow();
        }

        public void ResizeToTargetWindow()
        {
            IntPtr hwnd = TargetWindow;

            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            RECT rect;
            int w, h;
            GetWindowRect(hwnd, out rect);

            w = rect.Right - rect.Left;
            h = rect.Bottom - rect.Top;

            if (cropBorders.Checked)
            {
                h -= 25;
                w -= 6;
            }

            if ((w % 2) == 1)
            {
                w++;
            }

            if ((h % 2) == 1)
            {
                h++;
            }

            Width = w;
            Height = h;
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            MoveToTargetWindow();
        }

        public void MoveToTargetWindow()
        {
            IntPtr hwnd = TargetWindow;

            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            RECT rect;
            GetWindowRect(hwnd, out rect);

            if (cropBorders.Checked)
            {
                rect.Top += 22;
                rect.Left += 3;
            }

            Top = rect.Top;
            Left = rect.Left;
        }

        private void widthBox_ValueChanged(object sender, EventArgs e)
        {
            if (Width != widthBox.Value)
            {
                Width = (int)widthBox.Value;
            }
        }

        private void heightBox_ValueChanged(object sender, EventArgs e)
        {
            if (Height != heightBox.Value)
            {
                Height = (int)heightBox.Value;
            }
        }

        private void processComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            gRemote.SetWindow(TargetWindow);
        }

        public IntPtr GetWindowByTitle()
        {
            return FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, windowTitle.Text);
        }

        private void noneWindow_CheckedChanged(object sender, EventArgs e)
        {
            gRemote.SetWindow(TargetWindow);
        }

        private void windowByProcess_CheckedChanged(object sender, EventArgs e)
        {
            gRemote.SetWindow(TargetWindow);
        }

        private void windowByTitle_CheckedChanged(object sender, EventArgs e)
        {
            gRemote.SetWindow(TargetWindow);
        }

        private void windowTitle_TextChanged(object sender, EventArgs e)
        {
            gRemote.SetWindow(TargetWindow);
        }

        private void FollowWindowTick(object sender, EventArgs e)
        {
            MoveToTargetWindow();
        }

        private void followWindowBox_CheckedChanged(object sender, EventArgs e)
        {
            followTimer.Enabled = followWindowBox.Checked;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [Flags()]
        private enum SetWindowPosFlags : uint
        {
            AsynchronousWindowPosition = 0x4000,
            DeferErase = 0x2000,
            DrawFrame = 0x0020,
            FrameChanged = 0x0020,
            HideWindow = 0x0080,
            DoNotActivate = 0x0010,
            DoNotCopyBits = 0x0100,
            IgnoreMove = 0x0002,
            DoNotChangeOwnerZOrder = 0x0200,
            DoNotRedraw = 0x0008,
            DoNotReposition = 0x0200,
            DoNotSendChangingEvent = 0x0400,
            IgnoreResize = 0x0001,
            IgnoreZOrder = 0x0004,
            ShowWindow = 0x0040,
        }
    }
}
