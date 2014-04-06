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
    class VideoDecoder
    {
        volatile bool started;
        FFMpeg ffmpeg;
        Process process;
        Thread readThread;
        Thread writeThread;
        Thread errorThread;
        BufferPool buffers = new BufferPool();
        BufferPool decodedBuffers = new BufferPool();
        BufferedStream bufferedOutputStream;
        BufferedStream bufferedInputStream;
        int width, height;
        Rectangle lockBounds;
        Bitmap decodeBuffer;
        int totalBytesDecoded = 0;
        VideoPreview videoPreview;

        public VideoDecoder(FFMpeg ffmpeg, int width, int height)
        {
            this.ffmpeg = ffmpeg;
            this.width = width;
            this.height = height;
            this.lockBounds = new Rectangle(0, 0, width, height);
            this.decodeBuffer = new Bitmap(width, height, PixelFormat.Format24bppRgb);
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

            buffers.Clear();
            totalBytesDecoded = 0;

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

            bufferedOutputStream = new BufferedStream(process.StandardInput.BaseStream);
            bufferedInputStream = new BufferedStream(process.StandardOutput.BaseStream);

            readThread = new Thread(new ThreadStart(readThreadMain));
            writeThread = new Thread(new ThreadStart(writeThreadMain));
            errorThread = new Thread(new ThreadStart(errorThreadMain));

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
            buffers.Add(buffer);
        }

        public Bitmap Buffer
        {
            get
            {
                return decodeBuffer;
            }

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

            buffers.Clear();
            decodedBuffers.Clear();

            if (bufferedInputStream != null)
            {
                bufferedInputStream.Close();
                bufferedInputStream = null;
            }

            if (bufferedOutputStream != null)
            {
                bufferedOutputStream.Close();
                bufferedOutputStream = null;
            }

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

            buffers = new BufferPool();
            decodedBuffers = new BufferPool();
        }

        /// <summary>
        /// Reads decoded data buffers from FFMpeg
        /// </summary>
        public void readThreadMain()
        {
            int frameSize = width * height * 3;
            byte[] readBuffer = new byte[frameSize];
            int pos = 0;
            int bytesRead;
            BufferedStream bufferedInputStream = this.bufferedInputStream;

            while (IsDecoding)
            {
                bytesRead = bufferedInputStream.Read(readBuffer, pos, readBuffer.Length - pos);

                if (bytesRead <= 0)
                {
                    continue;
                }

                if (!IsDecoding)
                {
                    break;
                }

                pos += bytesRead;

                if (pos >= frameSize)
                {
                    lock (decodeBuffer)
                    {
                        BitmapData data = decodeBuffer.LockBits(lockBounds, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                        Marshal.Copy(readBuffer, 0, data.Scan0, readBuffer.Length);
                        decodeBuffer.UnlockBits(data);
                    }

                    if (videoPreview != null)
                    {

                        videoPreview.RenderDirect(decodeBuffer);
                    }

                    //readBuffer = new byte[frameSize];
                    pos = 0;
                }
            }
        }

        /// <summary>
        /// Writes encoded buffers to FFMpeg
        /// </summary>
        public void writeThreadMain()
        {
            byte[] nextBuffer = null;

            while (IsDecoding)
            {
                buffers.Wait();
                nextBuffer = buffers.Remove();

                if (nextBuffer == null)
                {
                    continue;
                }

                totalBytesDecoded += nextBuffer.Length;

                try
                {
                    bufferedOutputStream.Write(nextBuffer, 0, nextBuffer.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
            }
        }

        /// <summary>
        /// Reads FFMpeg diagnostics output
        /// </summary>
        protected void errorThreadMain()
        {
            while (IsDecoding)
            {
                try
                {
                    readError();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
            }
        }

        public void readError()
        {
            String msg = process.StandardError.ReadLine();

            if (msg == null || msg.Length <= 0)
            {
                return;
            }

           // Console.WriteLine("[DECODE] {0}", msg);
        }
    }
}
