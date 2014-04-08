using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GRemote
{
    public class VideoDecoder
    {
        private volatile bool started;
        private FFMpeg ffmpeg;
        private Process process;
        private StoppableThread readThread;
        private StoppableThread writeThread;
        private StoppableThread errorThread;
        private BufferPool encodedBuffers = new BufferPool();
        private BufferPool decodedBuffers = new BufferPool();
        private int width, height;
        private int totalBytes = 0;
        private VideoPreview videoPreview;

        public VideoDecoder(FFMpeg ffmpeg, int width, int height)
        {
            if ((width % 2) != 0 || (height % 2) != 0)
            {
                throw new Exception("Capture dimensions must be even (divisible by two)");
            }

            Console.WriteLine("Decoding buffer {0}x{1}", width, height);

            this.ffmpeg = ffmpeg;
            this.width = width;
            this.height = height;
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

        public VideoPreview VideoPreview
        {
            get
            {
                return videoPreview;
            }
            set
            {
                videoPreview = value;
            }
        }

        public int TotalBytes
        {
            get
            {
                return totalBytes;
            }
        }

        public bool IsDecoding
        {
            get
            {
                lock (this)
                {
                    return started;
                }
            }
        }

        public void StartDecoding()
        {
            if (IsDecoding)
            {
                return;
            }

            lock (this)
            {
                started = true;
            }

            totalBytes = 0;

            // Create new buffer pools in case old threads are still doing things
            encodedBuffers = new BufferPool();
            decodedBuffers = new BufferPool();

            process = new Process();
            process.StartInfo.Arguments = GetFFMpegArguments();
            process.StartInfo.FileName = ffmpeg.Path;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();

            readThread = new VideoDecoderReadThread(this, process, decodedBuffers);
            writeThread = new VideoDecoderWriteThread(this, process, encodedBuffers);
            errorThread = new VideoDecoderErrorThread(this, process);

            errorThread.Start();
            readThread.Start();
            writeThread.Start();
            
        }

        public string GetFFMpegArguments()
        {
            String args = "";

            args += " -f mpegts ";
            args += " -i - ";
            args += " -f rawvideo ";
            args += " -c:v rawvideo ";
            args += "  -tune zerolatency ";
            args += " -video_size " + width.ToString() + "x" + height.ToString() + " ";
            args += " -pixel_format bgr24 -pix_fmt bgr24 ";
            args += " - ";

            return args;
        }

        public void Decode(byte[] buffer)
        {
            if (buffer == null)
            {
                return;
            }

            if (buffer.Length <= 0)
            {
                return;
            }

            totalBytes += buffer.Length;
            encodedBuffers.Add(buffer);
        }

        /// <summary>
        /// Stops the decoding process. If the decoder is not running
        /// this does nothing.
        /// </summary>
        public void StopDecoding()
        {
            if (!IsDecoding)
            {
                return;
            }

            lock (this)
            {
                started = false;
            }

            // Give any currently running threads a chance to break
            encodedBuffers.Clear();
            decodedBuffers.Clear();

            try
            {
                process.StandardError.Close();
                process.StandardInput.Close();
                process.StandardOutput.Close();
                process.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            process = null;

            if (readThread != null)
            {
                readThread.Stop();
                readThread = null;
            }

            if (writeThread != null)
            {
                writeThread.Stop();
                writeThread = null;
            }

            if (errorThread != null)
            {
                errorThread.Stop();
                errorThread = null;
            }
        }
    }

    public class VideoDecoderErrorThread : StoppableThread
    {
        private StreamReader reader;

        public VideoDecoderErrorThread(VideoDecoder decoder, Process process)
        {
            reader = process.StandardError;
        }

        protected override void RunThread()
        {
            String msg = reader.ReadLine();
        }
    }

    public class VideoDecoderReadThread : StoppableThread
    {
        private VideoDecoder decoder;
        private BufferPool decodedBuffers;
        private Stream stream;
        private int frameSize;
        private byte[] readBuffer;
        private int pos;
        private VideoPreview preview;
        private Bitmap decodeBuffer;
        private Rectangle lockBounds;

        public VideoDecoderReadThread(VideoDecoder decoder, Process process, BufferPool decodedBuffers)
        {
            this.decoder = decoder;
            this.preview = decoder.VideoPreview;
            this.decodedBuffers = decodedBuffers;
            this.lockBounds = new Rectangle(0, 0, decoder.VideoWidth, decoder.VideoWidth);
            this.decodeBuffer = new Bitmap(decoder.VideoWidth, decoder.VideoWidth, PixelFormat.Format24bppRgb);
            this.stream = process.StandardOutput.BaseStream;// new BufferedStream(process.StandardOutput.BaseStream);
            this.frameSize = decoder.VideoWidth * decoder.VideoHeight * 3;
            this.readBuffer = new byte[frameSize];
            this.pos = 0;
        }

        protected override void RunThread()
        {
            while (HandleBuffer())
            {
                Thread.Yield();
            }
        }

        protected bool HandleBuffer()
        {
            int bytesRead;

            bytesRead = stream.Read(readBuffer, pos, readBuffer.Length - pos);

            if (bytesRead <= 0)
            {
                return false;
            }

            pos += bytesRead;

            if (pos >= frameSize)
            {
                FinishBuffer();
                return true;
            }

            return false;
        }

        protected void FinishBuffer()
        {
            BitmapData data = decodeBuffer.LockBits(lockBounds, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(readBuffer, 0, data.Scan0, readBuffer.Length);
            decodeBuffer.UnlockBits(data);

            if (preview != null)
            {
                preview.RenderDirect(decodeBuffer);
            }

            pos = 0;
        }
    }

    public class VideoDecoderWriteThread : StoppableThread
    {
        private VideoDecoder decoder;
        private BufferPool encodedBuffers;
        private Stream stream;

        public VideoDecoderWriteThread(VideoDecoder decoder, Process process, BufferPool encodedBuffers)
        {
            this.decoder = decoder;
            this.encodedBuffers = encodedBuffers;
            this.stream = process.StandardInput.BaseStream;// new BufferedStream(process.StandardInput.BaseStream);
        }

        protected override void RunThread()
        {
            byte[] nextBuffer;

            encodedBuffers.Wait();

            while ((nextBuffer = encodedBuffers.Remove()) != null)
            {
                HandleBuffer(nextBuffer);
            }
        }

        protected void HandleBuffer(byte[] nextBuffer)
        {
            if (nextBuffer.Length <= 0)
            {
                return;
            }

            stream.Write(nextBuffer, 0, nextBuffer.Length);
            stream.Flush();
        }
    }

    
}
