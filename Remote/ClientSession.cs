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
    class ClientSession
    {
        GRemoteDialog gRemote;
        Socket socket;
        NetworkStream networkStream;
        BinaryReader binaryReader;
        Thread readThread;
        String address;
        int port;
        bool running;
        VideoDecoder videoDecoder;

        public ClientSession(GRemoteDialog gRemote, String address, int port)
        {
            this.gRemote = gRemote;
            this.address = address;
            this.port = port;
        }

        public void StartClient()
        {
            if (running)
            {
                return;
            }

            running = true;
            readThread = new Thread(new ThreadStart(readThreadMain));
        }

        protected void readThreadMain()
        {
            
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress hostIP = (Dns.Resolve(address)).AddressList[0];
            IPEndPoint ep = new IPEndPoint(hostIP, port);

            socket.Connect(ep);
            networkStream = new NetworkStream(socket);
            binaryReader = new BinaryReader(networkStream);

            byte packetType;

            while (running)
            {
                packetType = binaryReader.ReadByte();

                switch ((PacketType)packetType)
                {
                    case PacketType.VIDEO_UPDATE:
                        readVideoPacket();
                        break;
                }
            }
        }

        protected void readVideoPacket()
        {
            int packetLength = binaryReader.ReadInt32();
            byte[] buffer = binaryReader.ReadBytes(packetLength);
            videoDecoder.Decode(buffer);
        }

        public void StopClient()
        {
            if (!running)
            {
                return;
            }

            running = false;
        }

        public void SetDecoder(VideoDecoder videoDecoder)
        {
            this.videoDecoder = videoDecoder;
        }
    }
}
