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
        int x, y;
        int width;
        int height;
        Size size;
        BufferPool buffers = new BufferPool();
        Bitmap captureBuffer;
        Graphics captureGraphics;
        Rectangle bounds, lockBounds;

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

        public void Capture()
        {
            captureGraphics.CopyFromScreen(x, y, 0, 0, size);
        }

        public Bitmap Buffer
        {
            get
            {
                return captureBuffer;
            }
        }
    }
}
