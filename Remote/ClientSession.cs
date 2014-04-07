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
        private VideoPreview videoPreview;
        private InputCapture inputCapture;
        private String address;
        private int port;
        private bool running;

        public ClientSession(GRemoteDialog gRemote, String address, int port)
        {
            this.gRemote = gRemote;
            this.address = address;
            this.port = port;
            this.videoPreview = gRemote.VideoPreview;
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

        public VideoPreview Preview
        {
            get
            {
                return videoPreview;
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
    }

    public class ClientWriteThread : StoppableThread
    {
        public ClientWriteThread()
        {

        }

        protected override void RunThread()
        {
            Stop();
        }
    }

    public class ClientReadThread : StoppableThread
    {
        private FFMpeg ffmpeg;
        private VideoPreview preview;
        private ClientSession client;
        private Socket socket;
        private NetworkStream networkStream;
        private BinaryReader binaryReader;
        private VideoDecoder decoder;

        public ClientReadThread(ClientSession client)
        {
            this.client = client;
            this.ffmpeg = client.FFMpeg;
            this.preview = client.Preview;
            this.decoder = null;// client.Decoder;
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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

            socket.Connect(ep);

            this.networkStream = new NetworkStream(socket);
            this.binaryReader = new BinaryReader(networkStream);
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

        protected override void RunThread()
        {
            byte packetType;

            packetType = binaryReader.ReadByte();

            switch ((PacketType)packetType)
            {
                case PacketType.VIDEO_START:
                    readVideoStart();
                    break;
                case PacketType.VIDEO_UPDATE:
                    readVideoPacket();
                    break;
                case PacketType.KEYBOARD:
                    readKeyboardPacket();
                    break;
            }
        }

        protected void readVideoStart()
        {
            if (decoder != null)
            {
                decoder.StopDecoding();
                decoder = null;
            }

            byte[] buffer = new byte[9];
            buffer[0] = (byte)PacketType.VIDEO_START;
            binaryReader.Read(buffer, 1, 8);
            VideoStartPacket packet = new VideoStartPacket(buffer);

            decoder = new VideoDecoder(ffmpeg, packet.VideoWidth, packet.VideoHeight);
            preview.SetSize(packet.VideoWidth, packet.VideoHeight);
            decoder.VideoPreview = preview;
            decoder.StartDecoding();
        }

        protected void readVideoPacket()
        {
            int packetLength = binaryReader.ReadInt32();
            byte[] buffer = binaryReader.ReadBytes(packetLength);

            if (decoder != null)
            {
                decoder.Decode(buffer);
            }

            //videoPreview.RenderDirect(videoDecoder.Buffer);
        }

        protected void readKeyboardPacket()
        {
            byte[] buffer = new byte[6];
            buffer[0] = (byte)PacketType.KEYBOARD;
            binaryReader.Read(buffer, 1, 5);
            KeyboardPacket packet = new KeyboardPacket(buffer);
        }
    }
}
