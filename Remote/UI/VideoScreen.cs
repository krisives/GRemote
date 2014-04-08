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
        private PreviewMode previewMode = PreviewMode.COMPRESSED;
        private Bitmap uncompressedImage;
        private Bitmap compressedImage;
        private BufferedGraphicsContext bufferContext;
        private BufferedGraphics bg;
        private Graphics g;
        private ScaleMode scaleMode = ScaleMode.CENTER;
        private int width, height;

        public VideoScreen()
        {
            bufferContext = BufferedGraphicsManager.Current;
            InitializeComponent();

            width = Width;
            height = Height;

            bg = bufferContext.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            g = bg.Graphics;
        }

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

        public int VideoWidth
        {
            get
            {
                return width;
            }
        }

        public int VideoHeight
        {
            get
            {
                return height;
            }
        }

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
                    ResizeVideo();
                }
            }
        }

        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;

            if (InvokeRequired)
            {
                Invoke(new Action(delegate()
                {
                    ResizeVideo();
                }));
            }
            else
            {
                ResizeVideo();
            }

            Console.WriteLine("Preview set to {0}x{1}", width, height);
        }

        protected void ResizeVideo()
        {
            int dx;
            int dy;

            if (Width < width || Height < height)
            {
                dx = gRemote.Width - Width;
                dy = gRemote.Height - Height;

                gRemote.Width = width + dx;
                gRemote.Height = height + dy;
            }

            // Width = width;
            // Height = height;
        }

        public void SetBuffers(Bitmap uncompressedImage, Bitmap compressedImage)
        {
            if (uncompressedImage == null || compressedImage == null)
            {
                throw new Exception("Bitmap images cannot be null");
            }

            int w = uncompressedImage.Width;
            int h = uncompressedImage.Height;

            this.uncompressedImage = uncompressedImage;
            this.compressedImage = compressedImage;
        }

        public PreviewMode PreviewMode
        {
            get
            {
                return previewMode;
            }
            set
            {
                previewMode = value;
                Refresh();
            }
        }

        public void RenderDirect(Bitmap screen)
        {
            if (previewMode == PreviewMode.NONE)
            {
                return;
            }

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
            if (Width < 0)
            {
                Width = 2;
            }

            if (Height < 0)
            {
                Height = 2;
            }

            bg = bufferContext.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
            g = bg.Graphics;
        }
    }

    public enum PreviewMode
    {
        NONE,
        COMPRESSED,
        SPLIT,
        UNCOMPRESSED
    }

    public enum ScaleMode
    {
        CENTER,
        STRETCHED
    }
}
