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
    public class VideoEncoder
    {
        private FFMpeg ffmpeg;
        private int width;
        private int height;
        private Rectangle lockBounds;
        private volatile bool started = false;
        private BufferPool frameBuffers = new BufferPool();
        private BufferPool encodedBuffers = new BufferPool();
        private Process process;
        private StoppableThread errorThread;
        private StoppableThread writeThread;
        private StoppableThread readThread;
        private List<Stream> listenerStreams = new List<Stream>();
        private volatile int totalBytes = 0;
        private Stream fout;
        private String fileOutputPath;
        private bool enableFileRecording = false;

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

            Console.WriteLine("Encoding buffer {0}x{1}", width, height);

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
            frameBuffers.Clear();

            if (enableFileRecording)
            {
                fout = new BufferedStream(File.Open(fileOutputPath, FileMode.Create));
            }

            // Create new buffer pools in case threads are still doing things
            frameBuffers = new BufferPool();
            encodedBuffers = new BufferPool();

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

            readThread = new VideoEncoderReadThread(this, process, encodedBuffers);//new Thread(new ThreadStart(readThreadMain));
            writeThread = new VideoEncoderWriteThread(this, process, frameBuffers);//new Thread(new ThreadStart(writeThreadMain));
            errorThread = new VideoEncoderErrorThread(this, process);//new Thread(new ThreadStart(errorThreadMain));

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
            frameBuffers.Add(buffer);
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

            frameBuffers.Pulse();
            encodedBuffers.Pulse();

            try
            {
                process.StandardInput.Close();
                process.StandardError.Close();
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

        public byte[] Read()
        {
            return encodedBuffers.Remove();
        }
    }

    public class EncoderSettings
    {
        public String codec = "libx264";
        public int videoRate = 900;
        public int framerate = 30;
    }

    public class VideoEncoderWriteThread : StoppableThread
    {
        private VideoEncoder encoder;
        private Stream stream;
        private BufferPool frameBuffers;

        public VideoEncoderWriteThread(VideoEncoder encoder, Process process, BufferPool frameBuffers)
        {
            this.encoder = encoder;
            this.frameBuffers = frameBuffers;
            this.stream = process.StandardInput.BaseStream;
        }

        protected override void RunThread()
        {
            byte[] nextBuffer = frameBuffers.Remove();

            if (nextBuffer == null)
            {
                return;
            }

            stream.Write(nextBuffer, 0, nextBuffer.Length);
        }
    }

    public class VideoEncoderErrorThread : StoppableThread
    {
        private StreamReader reader;

        public VideoEncoderErrorThread(VideoEncoder encoder, Process process)
        {
            this.reader = process.StandardError;
        }

        protected override void RunThread()
        {
            String str = reader.ReadLine();
        }
    }

    public class VideoEncoderReadThread : StoppableThread
    {
        // Encoded data is read from FFMPeg in 16K chunks
        private byte[] readBuffer;
        private Stream stream;
        private Process process;
        private VideoEncoder encoder;
        private BufferPool encodedBuffers;

        public VideoEncoderReadThread(VideoEncoder encoder, Process process, BufferPool encodedBuffers)
        {
            this.encoder = encoder;
            this.process = process;
            this.encodedBuffers = encodedBuffers;
            this.readBuffer = new byte[1024 * 16];
            this.stream = process.StandardOutput.BaseStream;
        }

        protected override void RunThread()
        {
            int readCount;

            // Read encoded output of FFMpeg
            readCount = stream.Read(readBuffer, 0, readBuffer.Length);
               
            if (readCount <= 0)
            {
                return;
            }

            byte[] resized = new byte[readCount];
            Array.Copy(readBuffer, resized, readCount);

            encodedBuffers.Add(resized);
        }
    }
}
