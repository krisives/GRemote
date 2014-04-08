using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GRemote
{
    public partial class VideoScreen : UserControl
    {
        private GRemoteDialog gRemote; 
        private ScaleMode scaleMode = ScaleMode.CENTER;
        private BufferedGraphicsContext bufferContext;
        private BufferedGraphics bg;
        private Graphics g;
        private int width, height;

        public VideoScreen()
        {
            bufferContext = BufferedGraphicsManager.Current;
            width = Width;
            height = Height;
            bg = bufferContext.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            g = bg.Graphics;

            InitializeComponent();
        }

        /// <summary>
        /// Control owner (used to resize window if too small)
        /// </summary>
        public GRemoteDialog GRemote
        {
            get
            {
                return gRemote;
            }
            set
            {
                gRemote = value;
            }
        }

        /// <summary>
        /// Width of the video being displayed
        /// </summary>
        public int VideoWidth
        {
            get
            {
                return width;
            }
        }

        /// <summary>
        /// Height of the video being displayed
        /// </summary>
        public int VideoHeight
        {
            get
            {
                return height;
            }
        }

        /// <summary>
        /// Get or set how to display the video (centered or stretched)
        /// </summary>
        public ScaleMode ScaleMode
        {
            get
            {
                return scaleMode;
            }
            set
            {
                scaleMode = value;

                if (scaleMode == ScaleMode.CENTER)
                {
                    UpdateVideoSize();
                }
            }
        }

        /// <summary>
        /// Change the size of the video being displayed. This may expand the size of
        /// the parent control to fit.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetVideoSize(int width, int height)
        {
            this.width = width;
            this.height = height;

            if (InvokeRequired)
            {
                Invoke(new Action(delegate()
                {
                    UpdateVideoSize();
                }));
            }
            else
            {
                UpdateVideoSize();
            }

            Console.WriteLine("Screen set to {0}x{1}", width, height);
        }

        /// <summary>
        /// Invoked after the video size has been changed. This method exists because
        /// of internal windows threading
        /// </summary>
        protected void UpdateVideoSize()
        {
            int dx;
            int dy;

            if (gRemote == null)
            {
                return;
            }

            if (Width < width || Height < height)
            {
                dx = gRemote.Width - Width;
                dy = gRemote.Height - Height;

                gRemote.Width = width + dx;
                gRemote.Height = height + dy;
            }
        }

        public void RenderDirect(Bitmap screen)
        {
            lock (screen)
            {
                switch (scaleMode)
                {
                    case ScaleMode.CENTER:
                        RenderDirectCentered(screen);
                        break;
                    case ScaleMode.STRETCHED:
                        RenderDirectStretched(screen);
                        break;
                }

                bg.Render();
            }
        }

        protected void RenderDirectCentered(Bitmap screen)
        {
            int cx = (Width / 2) - (screen.Width / 2);
            int cy = (Height / 2) - (screen.Height / 2);
            g.DrawImage(screen, cx, cy);
        }

        protected void RenderDirectStretched(Bitmap screen)
        {
            g.DrawImage(screen, 0, 0, Width, Height);
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            bg = bufferContext.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            g = bg.Graphics;
        }
    }

    /// <summary>
    /// How to layout the screen if it's not the same size as the window
    /// </summary>
    public enum ScaleMode
    {
        /// <summary>
        /// Center screen (no scaling)
        /// </summary>
        CENTER,

        /// <summary>
        /// Stretch the screen to fit (does not respect aspect ratio)
        /// </summary>
        STRETCHED
    }
}
