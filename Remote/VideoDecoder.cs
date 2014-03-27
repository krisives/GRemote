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
        bool started;
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

        public VideoDecoder(FFMpeg ffmpeg, int width, int height)
        {
            this.ffmpeg = ffmpeg;
            this.width = width;
            this.height = height;
            this.lockBounds = new Rectangle(0, 0, width, height);
            this.decodeBuffer = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Console.WriteLine("{0}x{1}", width, height);

        }

        public void StartDecoding()
        {
            if (started)
            {
                return;
            }

            started = true;

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

           // args += " -y ";
          //  args += " -t 01:00:00 ";
        //    args += " -f rawvideo ";
         //   args += " -pixel_format bgr24 ";
           // args += " -video_size " + width.ToString() + "x" + height.ToString() + " ";
        //    args += " -framerate 30 ";
            args += " -f avi ";
            //args += " -video_size " + width.ToString() + "x" + height.ToString() + " ";
            //args += " -c:v libxvid ";
            args += " -i - ";
          //  args += " -vframes 99999 ";
          //  args += " -vb 900K ";
         //   args += " -c:v libxvid ";
            args += " -f rawvideo ";
            args += " -c:v rawvideo ";
            args += "  -tune zerolatency ";
            args += " -video_size " + width.ToString() + "x" + height.ToString() + " ";

            args += " -pixel_format bgr24 -pix_fmt bgr24 ";
           // args += " -framerate 30 ";

            args += " - ";
            //args += " X:\\GRemote\\xxx.avi ";

            return args;
        }

        public void Decode(byte[] buffer)
        {
            buffers.Add(buffer);
        }

        /*
        public bool Read()
        {
            BitmapData data;
            byte[] buffer;

            buffer = decodedBuffers.Remove();

            if (buffer == null)
            {
                return false;
            }


            lock (decodeBuffer)
            {
                //Console.WriteLine("--- {0} ----", buffer.Length);
                data = decodeBuffer.LockBits(lockBounds, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
                decodeBuffer.UnlockBits(data);
            }



            data = null;
            return true;
        }
        */

        public Bitmap Buffer
        {
            get
            {
                return decodeBuffer;
            }

        }

        public void StopDecoding()
        {
            if (!started)
            {
                return;
            }

            started = false;
            buffers.Pulse();

            process.StandardError.Close();
            process.StandardInput.Close();
            process.StandardOutput.Close();
            process.Close();
            process = null;
        }

        public void readThreadMain()
        {
            int frameSize = width * height * 3;
            byte[] readBuffer = new byte[frameSize];
            int pos = 0;
            int bytesRead;
           // int remaining;

            while (started)
            {
                bytesRead = bufferedInputStream.Read(readBuffer, pos, readBuffer.Length - pos);

                if (bytesRead <= 0)
                {
                    continue;
                }

                pos += bytesRead;

                if (pos >= frameSize)
                {
                   // decodedBuffers.Add(readBuffer);


                    //Console.WriteLine("--- {0} ----", buffer.Length);
                    lock (decodeBuffer)
                    {
                        BitmapData data = decodeBuffer.LockBits(lockBounds, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                        Marshal.Copy(readBuffer, 0, data.Scan0, readBuffer.Length);
                        decodeBuffer.UnlockBits(data);
                    }

                    readBuffer = new byte[frameSize];
                    pos = 0;
                }
            }
        }

        public void writeThreadMain()
        {
            byte[] nextBuffer = null;

            while (started)
            {
                buffers.Wait();
                nextBuffer = buffers.Remove();

                if (nextBuffer == null)
                {
                    continue;
                }

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

            Console.WriteLine("[DECODE] {0}", msg);
        }
    }
}
