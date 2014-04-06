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
        GRemoteDialog gRemote;
        Socket socket;
        NetworkStream networkStream;
        BinaryReader binaryReader;
        StoppableThread writeThread;
        StoppableThread readThread;
        String address;
        int port;
        bool running;
        //VideoDecoder videoDecoder;
        VideoPreview videoPreview;

        public ClientSession(GRemoteDialog gRemote, String address, int port)
        {
            this.gRemote = gRemote;
            this.address = address;
            this.port = port;
            this.videoPreview = gRemote.VideoPreview;
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

       // public VideoDecoder Decoder
       // {
       //     get
        //    {
        //        return videoDecoder;
         //   }
        //}

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

            //videoDecoder = new VideoDecoder(gRemote.FFmpeg, 800, 600);
            //videoDecoder.VideoPreview = gRemote.VideoPreview;
            //videoDecoder.StartDecoding();

            readThread = new ClientReadThread(this);
            writeThread = new ClientWriteThread();

            readThread.Start();
            writeThread.Start();
        }

        public void StopClient()
        {
            if (!running)
            {
                return;
            }

            running = false;

            //if (videoDecoder != null)
            //{
           //     videoDecoder.StopDecoding();
            //    videoDecoder = null;
            //}

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
        }

        //public void SetDecoder(VideoDecoder videoDecoder)
        //{
        //    this.videoDecoder = videoDecoder;
        //}
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
        FFMpeg ffmpeg;
        VideoPreview preview;
        ClientSession client;
        Socket socket;
        NetworkStream networkStream;
        BinaryReader binaryReader;
        VideoDecoder decoder;

        public ClientReadThread(ClientSession client)
        {
            this.client = client;
            this.ffmpeg = client.FFMpeg;
            this.preview = client.Preview;
            this.decoder = null;// client.Decoder;
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
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
    }
}
