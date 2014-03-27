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
        PreviewMode previewMode = PreviewMode.COMPRESSED;
        Bitmap uncompressedImage;
        Bitmap compressedImage;
        BufferedGraphicsContext bufferContext;
        BufferedGraphics bg;
        Graphics g;
        Rectangle leftHalf = new Rectangle();
        Rectangle rightHalf = new Rectangle();

        public VideoPreview()
        {
            bufferContext = BufferedGraphicsManager.Current;
            InitializeComponent();
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

            this.bg = bufferContext.Allocate(CreateGraphics(), new Rectangle(0, 0, w, h));
            this.g = bg.Graphics;

            leftHalf.X = 0;
            leftHalf.Y = 0;
            leftHalf.Width = w / 2;
            leftHalf.Height = h;

            rightHalf.X = leftHalf.Width;
            rightHalf.Y = 0;
            rightHalf.Width = leftHalf.Width;
            rightHalf.Height = h;
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
            }
        }

        public void Render()
        {
            switch (previewMode)
            {
                case PreviewMode.NONE:
                    break;
                case PreviewMode.COMPRESSED:
                    lock (compressedImage)
                    {
                        g.DrawImage(compressedImage, 0, 0);
                    }

                    break;
                case PreviewMode.UNCOMPRESSED:
                    lock (uncompressedImage)
                    {
                        g.DrawImage(uncompressedImage, 0, 0);
                    }

                    break;
                case PreviewMode.SPLIT:
                    lock (compressedImage)
                    {
                        lock (uncompressedImage)
                        {
                            g.DrawImage(uncompressedImage, 0, 0);
                            g.DrawImage(compressedImage, rightHalf.Location.X, rightHalf.Location.Y, rightHalf, GraphicsUnit.Pixel);
                        }
                    }

                    
                    break;
            }

            //lock (compressedImage) {
            //    g.DrawImage(compressedImage, 0, 0);
           // }

            bg.Render();
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
