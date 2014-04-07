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
    public partial class VideoPreview : UserControl
    {
        private GRemoteDialog gRemote; 
        private PreviewMode previewMode = PreviewMode.COMPRESSED;
        private Bitmap uncompressedImage;
        private Bitmap compressedImage;
        private BufferedGraphicsContext bufferContext;
        private BufferedGraphics bg;
        private Graphics g;
        private Rectangle leftHalf = new Rectangle();
        private Rectangle rightHalf = new Rectangle();

        public VideoPreview()
        {
            bufferContext = BufferedGraphicsManager.Current;
            InitializeComponent();
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

        public void SetSize(int width, int height)
        {
            bg = bufferContext.Allocate(CreateGraphics(), new Rectangle(0, 0, width, height));
            g = bg.Graphics;

            leftHalf.X = 0;
            leftHalf.Y = 0;
            leftHalf.Width = width / 2;
            leftHalf.Height = height;

            rightHalf.X = leftHalf.Width;
            rightHalf.Y = 0;
            rightHalf.Width = leftHalf.Width;
            rightHalf.Height = height;

            if (InvokeRequired)
            {
                Invoke(new Action(delegate()
                {
                    ResizeVideo(width, height);
                }));
            }
            else
            {
                ResizeVideo(width, height);
            }

            Console.WriteLine("Preview set to {0}x{1}", width, height);
        }

        protected void ResizeVideo(int width, int height)
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

            Width = width;
            Height = height;
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
                g.DrawImage(screen, 0, 0);
                bg.Render();
            }
        }
    }

    public enum PreviewMode
    {
        NONE,
        COMPRESSED,
        SPLIT,
        UNCOMPRESSED
    }
}
