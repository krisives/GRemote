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
            RefreshProcessList();
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

            if (moveWindowBox.Checked && processComboBox.SelectedIndex > 0)
            {
                Process p = processComboBox.SelectedItem as Process;

                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    SetWindowPos(p.MainWindowHandle, (IntPtr)0, Left, Top, 0, 0, SetWindowPosFlags.DoNotActivate | SetWindowPosFlags.IgnoreResize);
                }
            }
        }

        bool hack = false;

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
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);
        
        [Flags()]
        private enum SetWindowPosFlags : uint
        {
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from
            /// blocking its execution while other threads process the request.</summary>
            /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
            AsynchronousWindowPosition = 0x4000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            /// <remarks>SWP_DEFERERASE</remarks>
            DeferErase = 0x2000,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            /// <remarks>SWP_DRAWFRAME</remarks>
            DrawFrame = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
            /// is sent only when the window's size is being changed.</summary>
            /// <remarks>SWP_FRAMECHANGED</remarks>
            FrameChanged = 0x0020,
            /// <summary>Hides the window.</summary>
            /// <remarks>SWP_HIDEWINDOW</remarks>
            HideWindow = 0x0080,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
            /// parameter).</summary>
            /// <remarks>SWP_NOACTIVATE</remarks>
            DoNotActivate = 0x0010,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
            /// contents of the client area are saved and copied back into the client area after the window is sized or
            /// repositioned.</summary>
            /// <remarks>SWP_NOCOPYBITS</remarks>
            DoNotCopyBits = 0x0100,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            /// <remarks>SWP_NOMOVE</remarks>
            IgnoreMove = 0x0002,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            /// <remarks>SWP_NOOWNERZORDER</remarks>
            DoNotChangeOwnerZOrder = 0x0200,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
            /// window uncovered as a result of the window being moved. When this flag is set, the application must
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            /// <remarks>SWP_NOREDRAW</remarks>
            DoNotRedraw = 0x0008,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            /// <remarks>SWP_NOREPOSITION</remarks>
            DoNotReposition = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            /// <remarks>SWP_NOSENDCHANGING</remarks>
            DoNotSendChangingEvent = 0x0400,
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            /// <remarks>SWP_NOSIZE</remarks>
            IgnoreResize = 0x0001,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            /// <remarks>SWP_NOZORDER</remarks>
            IgnoreZOrder = 0x0004,
            /// <summary>Displays the window.</summary>
            /// <remarks>SWP_SHOWWINDOW</remarks>
            ShowWindow = 0x0040,
        }

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        //This is the Import for the SetWindowsHookEx function.
        //Use this function to install a thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

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

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        private void resizeButton_Click(object sender, EventArgs e)
        {
            if (processComboBox.SelectedItem == null)
            {
                return;
            }

            if (processComboBox.SelectedItem.GetType() != typeof(Process))
            {
                return;
            }

            Process p = processComboBox.SelectedItem as Process;

            if (p.MainWindowHandle == IntPtr.Zero)
            {
                return;
            }
            
            RECT rect;

            GetWindowRect(p.MainWindowHandle, out rect);

            Width = rect.Right - rect.Left;
            Height = rect.Bottom - rect.Top;

            if ((Width % 2) == 1)
            {
                Width++;
                
            }

            if ((Height % 2) == 1)
            {
                Height++;
               
            }
        }

        private void moveButton_Click(object sender, EventArgs e)
        {
            if (processComboBox.SelectedItem == null)
            {
                return;
            }

            if (processComboBox.SelectedItem.GetType() != typeof(Process))
            {
                return;
            }

            Process p = processComboBox.SelectedItem as Process;

            if (p.MainWindowHandle == IntPtr.Zero)
            {
                return;
            }

            RECT rect;

            GetWindowRect(p.MainWindowHandle, out rect);

            Top = rect.Top;
            Left = rect.Left;
        }
    }
}
