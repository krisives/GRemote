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
    public class VideoCapture
    {
        private bool capturing;
        private int x, y;
        private int width;
        private int height;
        private BufferPool buffers = new BufferPool();
        private Rectangle bounds, lockBounds;
        private StoppableThread captureThread;
        private SnapshotListener listener;

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

            Console.WriteLine("Capturing buffer {0}x{1}", width, height);

            this.width = width;
            this.height = height;
            this.bounds = new Rectangle(0, 0, width, height);
            this.lockBounds = new Rectangle(0, 0, width, height);
        }

        /// <summary>
        /// A simple callback that is invoked after a snapshot has taken place.
        /// </summary>
        public delegate void SnapshotListener(Bitmap buffer);

        /// <summary>
        /// Get or set the snapshot listener that will be invoked when a snapshot
        /// occurs.
        /// </summary>
        public SnapshotListener Listener
        {
            get
            {
                return listener;
            }
            set
            {
                listener = value;
            }
        }

        /// <summary>
        /// Changes the position of the video capture. This allows moving of the capture area
        /// even after capture has started.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
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

        /// <summary>
        /// Gets the X position of the capture area
        /// </summary>
        public int X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Gets the Y postiion of the capture area
        /// </summary>
        public int Y
        {
            get
            {
                return y;
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
            if (IsCapturing)
            {
                return;
            }

            lock (this)
            {
                capturing = true;
            }

            captureThread = new CaptureThread(this);
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

            if (captureThread != null)
            {
                captureThread.Stop();
                captureThread = null;
            }
        }

        public bool IsCapturing
        {
            get
            {
                lock (this)
                {
                    return capturing;
                }
            }
        }
    }

    public class CaptureThread : StoppableThread
    {
        private VideoCapture videoCapture;
        private Graphics captureGraphics;
        private Size size;
        private int frameDelay = (int)(TimeSpan.TicksPerSecond / 30);
        private long last = DateTime.Now.Ticks;
        private int frameIndex = 0;
        private long lastFrameSample;
        private Bitmap captureBuffer;

        public CaptureThread(VideoCapture videoCapture)
        {
            this.videoCapture = videoCapture;
            this.size = new Size(videoCapture.Width, videoCapture.Height);
        }

        protected override void OnThreadStart()
        {
            captureBuffer = new Bitmap(videoCapture.Width, videoCapture.Height, PixelFormat.Format24bppRgb);
            captureGraphics = Graphics.FromImage(captureBuffer);

            last = DateTime.Now.Ticks;
            lastFrameSample = last;

            Console.WriteLine("{0} ticks per frame", frameDelay);
        }

        protected override void RunThread()
        {
            long target;
            long now;
            int distance;
            int milliseconds;

            // Get the current tick
            now = DateTime.Now.Ticks;

            // Estimate when the next frame should be
            target = last + frameDelay;

            if (now >= target)
            {
                CaptureFrame(now);
                return;
            }

            // Calculate how long until next frame
            distance = (int)(target - now);

            if (distance <= 0)
            {
                Thread.Yield();
                return;
            }

            if (distance > frameDelay)
            {
                distance = frameDelay;
            }

            milliseconds = (int)(distance / TimeSpan.TicksPerMillisecond);

            if (milliseconds <= 0)
            {
                //Thread.Yield();
                //return;
                milliseconds = 1;
            }

            Thread.Sleep(milliseconds);
        }

        protected void CaptureFrame(long now)
        {
            // Save frame that just happened
            last = now;
            frameIndex++;

            if ((last - lastFrameSample) > TimeSpan.TicksPerSecond)
            {
                Console.WriteLine("{0} capture fps", frameIndex);
                frameIndex = 0;
                lastFrameSample = last;
            }

            try
            {
                captureGraphics.CopyFromScreen(videoCapture.X, videoCapture.Y, 0, 0, size);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (videoCapture.Listener != null)
            {
                videoCapture.Listener(captureBuffer);
            }
        }
    }
}
