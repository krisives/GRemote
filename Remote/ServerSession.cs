using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace GRemote
{
    class ServerSession
    {
        BufferPool outputBuffers = new BufferPool();
        Socket socket;
        bool running;
        int port;
        String address;
        Thread listenThread;
        Thread writeThread;
        Thread readThread;
        List<ConnectedClient> clients = new List<ConnectedClient>();

        byte[] headerBuffer;
        MemoryStream headerStream;
        BinaryWriter headerBinary;

        GRemoteDialog gRemote;

        public ServerSession(GRemoteDialog gRemote, String address, int port)
        {
            this.gRemote = gRemote;
            this.address = address;
            this.port = port;

            headerBuffer = new byte[1024];
            headerStream = new MemoryStream(headerBuffer, 0, 1024);
            headerBinary = new BinaryWriter(headerStream);
        }

        public void StartServer()
        {
            if (running)
            {
                return;
            }

            running = true;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);



            listenThread = new Thread(listenThreadMain);
            listenThread.Start();

            readThread = new Thread(readThreadMain);
            readThread.Start();

            writeThread = new Thread(writeThreadMain);
            writeThread.Start();
        }

        protected void listenThreadMain()
        {
            Socket clientSocket;

            IPAddress hostIP = (Dns.Resolve(address)).AddressList[0];
            IPEndPoint ep = new IPEndPoint(hostIP, port);

            socket.Bind(ep);
            socket.Listen(20);

            while (running)
            {
                try
                {
                    clientSocket = socket.Accept();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    continue;
                }

                if (clientSocket == null)
                {
                    continue;
                }

                ConnectedClient client = new ConnectedClient();
                client.socket = socket;
                client.stream = new NetworkStream(socket);
                client.writer = new BinaryWriter(client.stream);

                clients.Add(client);
                gRemote.RestartStream();
            }
        }

        public void StopServer()
        {
            if (!running)
            {
                return;
            }

            running = false;
            socket.Close();
            outputBuffers.Pulse();
        }

        public void AddVideoBuffer(byte[] buffer)
        {
            if (!running || clients.Count <= 0) {
                return;
            }

            byte[] packetBuffer = new byte[1 + 4];

            headerBinary.Seek(0, SeekOrigin.Begin);
            headerBinary.Write((byte)PacketType.VIDEO_UPDATE);
            headerBinary.Write((int)buffer.Length);
            headerBinary.Flush();

            Array.Copy(headerBuffer, packetBuffer, packetBuffer.Length);

            outputBuffers.Add(packetBuffer);
            outputBuffers.Add(buffer);
        }

        protected void writeThreadMain()
        {
            byte[] nextBuffer;

            while (running)
            {
                outputBuffers.Wait();
                nextBuffer = outputBuffers.Remove();

                if (nextBuffer == null)
                {
                    continue;
                }

                foreach (ConnectedClient client in clients) {
                    client.writer.Write(nextBuffer);
                }
            }
        }

        protected void readThreadMain()
        {

        }

        class ConnectedClient
        {
            public Socket socket;
            public NetworkStream stream;
            public BinaryWriter writer;
        }
    }

    public enum PacketType : byte
    {
        CONNECT_REQUEST = 0x01,
        CONNECT_RESPONSE = 0x02,

        VIDEO_REQUEST = 0x10,
        VIDEO_START = 0x11,
        VIDEO_UPDATE = 0x12,
        VIDEO_END = 0x13,

        KEYBOARD = 0x20
    }


}
