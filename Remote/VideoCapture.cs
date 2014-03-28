using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace GRemote
{
    class VideoCapture
    {
        bool capturing;
        int x, y;
        int width;
        int height;
        Size size;
        BufferPool buffers = new BufferPool();
        Bitmap captureBuffer;
        Graphics captureGraphics;
        Rectangle bounds, lockBounds;
        Thread captureThread;
        SnapshotListener listener;

        public VideoCapture(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new Exception("Width and Height must be greater than zero");
            }

            if ((width % 2) != 0 || (height % 2) != 0)
            {
                throw new Exception("Capture dimensions must be even (divisible by two)");
            }

            this.width = width;
            this.height = height;
            this.size = new Size(width, height);
            this.bounds = new Rectangle(0, 0, width, height);
            this.lockBounds = new Rectangle(0, 0, width, height);

            captureBuffer = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            captureGraphics = Graphics.FromImage(captureBuffer);
        }

        public delegate void SnapshotListener();

        public SnapshotListener Listener
        {
            set
            {
                listener = value;
            }
        }

        public void SetCapturePos(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new Exception("Capture location cannot be negative");
            }

            this.x = x;
            this.y = y;
            this.bounds.X = x;
            this.bounds.Y = y;
        }

        public Bitmap Buffer
        {
            get
            {
                return captureBuffer;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public void StartCapturing()
        {
            if (IsCapturing())
            {
                return;
            }

            lock (this)
            {
                capturing = true;
            }

            captureThread = new Thread(new ThreadStart(captureThreadMain));
            captureThread.Start();
        }

        public void StopCapturing()
        {
            if (!capturing)
            {
                return;
            }

            lock (this)
            {
                capturing = false;
            }

            captureThread = null;
        }

        public bool IsCapturing()
        {
            lock (this)
            {
                return capturing;
            }
        }

        protected void captureThreadMain()
        {
            int frameDelay = (10000 / 30);
            long last = DateTime.Now.Ticks;
            long target;
            long now;
            int distance;

            while (IsCapturing())
            {
                captureGraphics.CopyFromScreen(x, y, 0, 0, size);

                if (listener != null)
                {
                    listener();
                }

                now = DateTime.Now.Ticks;
                target = last + frameDelay;
                distance = (int)(now - target);
                last = now;

                if (distance > frameDelay)
                {
                    distance = frameDelay;
                }
                else if (distance <= 0)
                {
                    continue;
                }

                //Console.WriteLine(distance / 10);
                Thread.Sleep(distance / 10);
            }
        }
    }
}
