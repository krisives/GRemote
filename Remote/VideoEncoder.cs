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
    class VideoEncoder
    {
        FFMpeg ffmpeg;
        int width;
        int height;
        Rectangle lockBounds;
        bool started = false;
        BufferPool buffers = new BufferPool();
        BufferPool encodedBuffers = new BufferPool();
        Process process;
        Thread errorThread;
        Thread writeThread;
        Thread readThread;
        BufferedStream bufferedStream;
        List<Stream> listenerStreams = new List<Stream>();
        volatile int totalBytes = 0;
        Stream fout;
        bool enableFileRecording = false;

        public VideoEncoder(FFMpeg ffmpeg, int width, int height)
        {
            this.ffmpeg = ffmpeg;

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
            this.lockBounds = new Rectangle(0, 0, width, height);
        }

        public int TotalBytes
        {
            get
            {
                return totalBytes;
            }
        }

        public void StartEncoding()
        {
            if (started)
            {
                return;
            }

            started = true;
            totalBytes = 0;

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

            bufferedStream = new BufferedStream(process.StandardInput.BaseStream);

            readThread = new Thread(new ThreadStart(readThreadMain));
            writeThread = new Thread(new ThreadStart(writeThreadMain));
            errorThread = new Thread(new ThreadStart(errorThreadMain));

            errorThread.Start();
            readThread.Start();
            writeThread.Start();

            if (enableFileRecording)
            {
                fout = new BufferedStream(File.Open("X:\\GRemote\\test.avi", FileMode.Create));
            }

            
        }

        public string GetFFMpegArguments()
        {
            String args = "";

            args += " -y ";
            args += " -t 01:00:00 ";
            args += " -f rawvideo ";
            args += " -pixel_format bgr24 ";
            args += " -video_size " + width.ToString() + "x" + height.ToString() + " ";
            args += " -framerate 30 ";
            args += " -i - ";
            args += " -vframes 99999 ";
            args += " -vb 900K ";
            args += " -c:v libxvid ";
            args += "  -tune zerolatency ";
            args += " -video_size " + width.ToString() + "x" + height.ToString() + " ";
            args += " -f avi ";

            args += " - ";
            //args += " X:\\GRemote\\xxx.avi ";

            return args;
        }

        public void Encode(Bitmap bmp)
        {
            // Allocate a buffer for the raw screen image (it cannot be reused, as it gets passed off)
            byte[] dataBuffer = new byte[width * height * 3];

            // Copy the area of the screen into the capture buffer
            //captureGraphics.CopyFromScreen(x, y, 0, 0, size);

            // Lock the Bitmap, copy the pixels, then unlock
            BitmapData data = bmp.LockBits(lockBounds, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(data.Scan0, dataBuffer, 0, dataBuffer.Length);
            bmp.UnlockBits(data);
            data = null;

            Encode(dataBuffer);
        }

        public void Encode(byte[] buffer)
        {
            // Add the buffer to the pool to write to FFMpeg
            buffers.Add(buffer);
        }

        public void StopEncoding()
        {
            if (!started)
            {
                return;
            }

            started = false;
            buffers.Pulse();
            process.StandardInput.Close();
            process.StandardError.Close();
            process.StandardOutput.Close();
            process.Close();
            process.Dispose();
            process = null;
        }

        public byte[] Read()
        {
            return encodedBuffers.Remove();
        }

        
        public void errorThreadMain()
        {
            while (started)
            {
                try
                {
                    readError();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }
            }
        }

        protected void readError()
        {
            String str = process.StandardError.ReadLine();

            if (str != null && str.Length > 0)
            {
                Console.WriteLine("[ENCODE] {0}", str);
            }
        }

        public void writeThreadMain()
        {
            while (started)
            {
                buffers.Wait();
                writeBuffer();
            }
        }

        protected void writeBuffer()
        {
            byte[] nextBuffer = buffers.Remove();

            if (nextBuffer == null)
            {
                return;
            }

            bufferedStream.Write(nextBuffer, 0, nextBuffer.Length);
        }

        public void readThreadMain()
        {
            byte[] readBuffer = new byte[1024 * 16];
            int readCount;
            Stream stream = new BufferedStream(process.StandardOutput.BaseStream);

            while (started)
            {
                readCount = stream.Read(readBuffer, 0, readBuffer.Length);
                
                if (readCount <= 0)
                {
                    continue;
                }

                if (enableFileRecording)
                {
                    fout.Write(readBuffer, 0, readCount);
                }

                byte[] resized = new byte[readCount];
                Array.Copy(readBuffer, resized, readCount);

                encodedBuffers.Add(resized);
                totalBytes += readCount;
                
                foreach (Stream s in listenerStreams)
                {
                    s.Write(resized, 0, readCount);
                }
            }
        }
    }

}
