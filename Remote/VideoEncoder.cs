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
        volatile bool started = false;
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
        String fileOutputPath;
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

        public bool IsEncoding
        {
            get
            {
                lock (this)
                {
                    return started;
                }
            }
        }

        public Boolean FileRecordingEnabled
        {
            get
            {
                return enableFileRecording;
            }
        }

        public String FileRecordingPath
        {
            get
            {
                return fileOutputPath;
            }
        }

        public void EnableFileOutput(String path)
        {
            enableFileRecording = true;
            fileOutputPath = path;
        }

        public void DisableFileOutput()
        {
            enableFileRecording = false;
            fileOutputPath = null;
        }

        public int TotalBytes
        {
            get
            {
                return totalBytes;
            }
        }

        public void StartEncoding(EncoderSettings settings)
        {
            if (IsEncoding)
            {
                return;
            }

            lock (this)
            {
                started = true;
            }

            totalBytes = 0;
            buffers.Clear();

            if (enableFileRecording)
            {
                fout = new BufferedStream(File.Open(fileOutputPath, FileMode.Create));
            }

            process = new Process();
            process.StartInfo.Arguments = GetFFMpegArguments(settings);
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
        }

        public string GetFFMpegArguments(EncoderSettings settings)
        {
            String args = "";

            args += " -y ";
            args += " -f rawvideo ";
            args += " -pixel_format bgr24 ";
            args += String.Format(" -video_size {0}x{1} ", width, height);
            args += " -framerate " + settings.framerate;
            args += " -i - ";
            args += String.Format(" -vb {0}K ", settings.videoRate);
            args += " -c:v " + settings.codec;
            args += "  -tune zerolatency ";
            args += String.Format(" -video_size {0}x{1} ", width, height);
            args += " -f mpegts ";
            args += " -bufsize 2M -g 300 -ps 4096 ";
            args += " - ";

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
            if (!started)
            {
                return;
            }

            // Add the buffer to the pool to write to FFMpeg
            buffers.Add(buffer);
        }

        public void StopEncoding()
        {
            if (!IsEncoding)
            {
                return;
            }

            lock (this)
            {
                started = false;
            }

            process.StandardInput.Close();
            process.StandardError.Close();
            process.WaitForExit();
            //process.StandardOutput.Close();
            //process.Close();
            //process = null;

            buffers.Pulse();
            encodedBuffers.Pulse();

            //buffers.Clear();
            //buffers = new BufferPool();

            //encodedBuffers.Clear();
            //encodedBuffers = new BufferPool();
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
               // Console.WriteLine("[ENCODE] {0}", str);
            }
        }

        public void writeThreadMain()
        {
            while (started)
            {
                try
                {
                    buffers.Wait();
                    writeBuffer();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }
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

        /// <summary>
        /// Reads the encoded output of FFMpeg.
        /// </summary>
        public void readThreadMain()
        {
            // Encoded data is read from FFMPeg in 16K chunks
            byte[] readBuffer = new byte[1024 * 16];
            int readCount;
            //int pos = 0;
            Stream stream = new BufferedStream(process.StandardOutput.BaseStream);
            BufferPool encodedBufers = this.encodedBuffers;
            Stream fout = this.fout;

            while (IsEncoding)
            {
                try
                {
                    // Read encoded output of FFMpeg
                    readCount = stream.Read(readBuffer, 0, readBuffer.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }

                if (readCount <= 0)
                {
                    continue;
                }

                //pos += readCount;

                //if (pos < readBuffer.Length)
                //{
                //    // Process 16K data at a time from FFMpeg
                //    continue;
                //}

                //readCount = pos;
                //pos = 0;

                byte[] resized = new byte[readCount];
                Array.Copy(readBuffer, resized, readCount);

                if (enableFileRecording)
                {
                    fout.Write(resized, 0, resized.Length);
                }

                encodedBuffers.Add(resized);
                totalBytes += readCount;

                foreach (Stream s in listenerStreams)
                {
                    s.Write(resized, 0, resized.Length);
                }
            }

            if (fout != null)
            {
                fout.Flush();
                fout.Close();
                fout = null;
            }
        }
    }

    public class EncoderSettings
    {
        public String codec = "libx264";
        public int videoRate = 900;
        public int framerate = 30;
    }

}
