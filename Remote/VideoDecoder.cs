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
        volatile bool started;
        FFMpeg ffmpeg;
        Process process;
        StoppableThread readThread;
        StoppableThread writeThread;
        StoppableThread errorThread;
        BufferPool encodedBuffers = new BufferPool();
        BufferPool decodedBuffers = new BufferPool();
        int width, height;
        int totalBytesDecoded = 0;
        VideoPreview videoPreview;

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

        public int TotalBytesDecoded
        {
            get
            {
                return totalBytesDecoded;
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

            totalBytesDecoded = 0;

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
        StreamReader reader;

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
        VideoDecoder decoder;
        BufferPool decodedBuffers;
        Stream stream;
        int frameSize;
        byte[] readBuffer;
        int pos;
        VideoPreview preview;
        Bitmap decodeBuffer;
        Rectangle lockBounds;

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
            int bytesRead;
            
            bytesRead = stream.Read(readBuffer, pos, readBuffer.Length - pos);

            if (bytesRead <= 0)
            {
                return;
            }

            pos += bytesRead;

            if (pos >= frameSize)
            {
                //lock (decodeBuffer)
                //{
                    BitmapData data = decodeBuffer.LockBits(lockBounds, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                    Marshal.Copy(readBuffer, 0, data.Scan0, readBuffer.Length);
                    decodeBuffer.UnlockBits(data);
                //}

                if (preview != null)
                {
                    preview.RenderDirect(decodeBuffer);
                }

                //readBuffer = new byte[frameSize];
                pos = 0;
            }
        }
    }

    public class VideoDecoderWriteThread : StoppableThread
    {
        VideoDecoder decoder;
        BufferPool encodedBuffers;
        Stream stream;

        public VideoDecoderWriteThread(VideoDecoder decoder, Process process, BufferPool encodedBuffers)
        {
            this.decoder = decoder;
            this.encodedBuffers = encodedBuffers;
            this.stream = process.StandardInput.BaseStream;// new BufferedStream(process.StandardInput.BaseStream);
        }

        protected override void RunThread()
        {
            byte[] nextBuffer;

            nextBuffer = encodedBuffers.Remove();

            if (nextBuffer == null)
            {
                return;
            }

            stream.Write(nextBuffer, 0, nextBuffer.Length);
        }
    }

    
}
