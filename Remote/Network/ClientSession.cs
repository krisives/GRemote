using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;

namespace GRemote
{
    public class ClientSession
    {
        private GRemoteDialog gRemote;
        private ClientWriteThread writeThread;
        private ClientReadThread readThread;
        private VideoScreen screen;
        private InputCapture inputCapture;
        private String address;
        private int port;
        private bool running;

        public ClientSession(GRemoteDialog gRemote, String address, int port)
        {
            this.gRemote = gRemote;
            this.address = address;
            this.port = port;
            this.screen = gRemote.VideoPreview;
        }

        public InputCapture InputCapture
        {
            get
            {
                return inputCapture;
            }
        }

        public String Address
        {
            get
            {
                return address;
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
        }

        public FFMpeg FFMpeg
        {
            get
            {
                return gRemote.FFmpeg;
            }
        }

        public VideoScreen VideoScreen
        {
            get
            {
                return screen;
            }
        }

        public bool IsRunning
        {
            get
            {
                lock (this)
                {
                    return running;
                }
            }
        }

        public VideoDecoder Decoder
        {
            get
            {
                return (readThread != null) ? readThread.Decoder : null;
            }
        }

        public void StartClient()
        {
            if (IsRunning)
            {
                return;
            }

            lock (this)
            {
                running = true;
            }

            inputCapture = new InputCapture(this);

            readThread = new ClientReadThread(this);
            writeThread = new ClientWriteThread();

            readThread.Start();
            writeThread.Start();

            inputCapture.StartCapturing();
        }

        public void StopClient()
        {
            if (!running)
            {
                return;
            }

            running = false;

            if (writeThread != null)
            {
                writeThread.Stop();
                writeThread = null;
            }

            if (readThread != null)
            {
                readThread.Stop();
                readThread = null;
            }

            if (inputCapture != null)
            {
                inputCapture.StopCapturing();
                inputCapture = null;
            }
        }

        public int TotalEncodedBytes
        {
            get
            {
                return readThread.TotalEncodedBytes;
            }
        }

        public int FrameIndex
        {
            get
            {
                return (readThread == null) ? 0 : readThread.FrameIndex;
            }
        }
    }

    /// <summary>
    /// A thread that writes data to the network.
    /// </summary>
    public class ClientWriteThread : StoppableThread
    {
        public ClientWriteThread()
        {

        }

        protected override void ThreadRun()
        {
            Stop();
        }
    }

    /// <summary>
    /// A thread that reads data from the network.
    /// </summary>
    public class ClientReadThread : StoppableThread
    {
        private FFMpeg ffmpeg;
        private VideoScreen screen;
        private ClientSession client;
        private Socket socket;
        private NetworkStream stream;
        private VideoDecoder decoder;

        public ClientReadThread(ClientSession client)
        {
            this.client = client;
            this.ffmpeg = client.FFMpeg;
            this.screen = client.VideoScreen;
            this.decoder = null;
            this.socket = null;
            this.stream = null;
        }

        public int TotalEncodedBytes
        {
            get
            {
                return (decoder == null) ? 0 : decoder.TotalBytes;
            }
        }

        public int FrameIndex
        {
            get
            {
                return (decoder == null) ? 0 : decoder.FrameIndex;
            }
        }

        public VideoDecoder Decoder
        {
            get
            {
                return decoder;
            }
        }

        protected override void OnThreadStart()
        {
            IPAddress hostIP = (Dns.Resolve(client.Address)).AddressList[0];
            IPEndPoint ep = new IPEndPoint(hostIP, client.Port);

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Connect(ep);
            this.stream = new NetworkStream(socket);
        }

        public override void Stop()
        {
            base.Stop();

            if (decoder != null)
            {
                decoder.StopDecoding();
                decoder = null;
            }
        }

        protected override void ThreadRun()
        {
            Packet packet = Packet.Read(stream);

            if (packet == null)
            {
                return;
            }

            switch (packet.Type)
            {
                case PacketType.VIDEO_START:
                    readVideoStart(packet as VideoStartPacket);
                    break;
                case PacketType.VIDEO_UPDATE:
                    readVideoUpdate(packet as VideoUpdatePacket);
                    break;
                case PacketType.VIDEO_END:
                    readVideoEnd(packet as VideoEndPacket);
                    break;
                case PacketType.KEYBOARD:
                    readKeyboardPacket(packet as KeyboardPacket);
                    break;
            }
        }

        protected void readVideoStart(VideoStartPacket packet)
        {
            if (decoder != null)
            {
                decoder.StopDecoding();
                decoder = null;
            }

            decoder = new VideoDecoder(ffmpeg, packet.VideoWidth, packet.VideoHeight);
            screen.SetVideoSize(packet.VideoWidth, packet.VideoHeight);
            decoder.VideoPreview = screen;
            decoder.StartDecoding();
        }

        protected void readVideoUpdate(VideoUpdatePacket packet)
        {
            byte[] buffer = new byte[packet.VideoDataLength];

            readBuffer(buffer);

            if (decoder != null)
            {
                decoder.Decode(buffer);
            }
        }

        protected void readVideoEnd(VideoEndPacket packet)
        {

        }

        protected void readKeyboardPacket(KeyboardPacket packet)
        {

        }

        private void readBuffer(byte[] buffer)
        {
            int pos = 0;
            int readCount;

            while (pos < buffer.Length)
            {
                readCount = stream.Read(buffer, pos, buffer.Length - pos);

                if (readCount < 0)
                {
                    throw new Exception("End of stream");
                }

                pos += readCount;
            }
        }
    }
}
