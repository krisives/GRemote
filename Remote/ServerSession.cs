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
        FFMpeg ffmpeg;

        // Listening socket
        Socket socket;

        // True if server is running
        bool running;

        // Address to listen on
        int port;
        String address;

        // Threads used to accept and process network data
        Thread listenThread;
        Thread writeThread;
        Thread readThread;

        // List of clients that are connected
        List<ConnectedClient> clients = new List<ConnectedClient>();

        // When data is broadcast to connected peers it goes
        // into this pool for the threads to read
        BufferPool outputBuffers = new BufferPool();

        // Capture, encoder and decoder for processing video data
        VideoCapture videoCapture;
        VideoEncoder videoEncoder;
        VideoDecoder videoDecoder;

        // 1K buffer for constructing header packets
        byte[] headerBuffer;
        MemoryStream headerStream;
        BinaryWriter headerBinary;

        VideoPreview videoPreview;

        public ServerSession(FFMpeg ffmpeg, VideoCapture videoCapture, String address, int port)
        {
            this.ffmpeg = ffmpeg;
            this.videoCapture = videoCapture;
            this.address = address;
            this.port = port;

            headerBuffer = new byte[1024];
            headerStream = new MemoryStream(headerBuffer, 0, 1024);
            headerBinary = new BinaryWriter(headerStream);
        }

        public VideoPreview Preview
        {
            set
            {
                videoPreview = value;
            }
            get
            {
                return videoPreview;
            }
        }

        public void StartServer()
        {
            if (IsRunning())
            {
                return;
            }

            lock (this)
            {
                running = true;
            }

            // Start the listening socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Begin the capture and encoding process
            videoCapture.Listener = Snapshot;

            videoEncoder = new VideoEncoder(ffmpeg, videoCapture.Width, videoCapture.Height);
            videoEncoder.StartEncoding();

            videoDecoder = new VideoDecoder(ffmpeg, videoCapture.Width, videoCapture.Height);
            videoDecoder.StartDecoding();

            // Create networking threads
            listenThread = new Thread(listenThreadMain);
            readThread = new Thread(readThreadMain);
            writeThread = new Thread(writeThreadMain);

            // Start networking threads
            listenThread.Start();
            readThread.Start();
            writeThread.Start();

            videoCapture.StartCapturing();
        }

        protected void RestartStream()
        {
            // TODO add "restart stream" packet
            outputBuffers.Clear();

            videoEncoder.StopEncoding();
            videoDecoder.StopDecoding();

            videoEncoder.StartEncoding();
            videoDecoder.StartDecoding();
        }

        protected void Snapshot()
        {
            byte[] encodedVideoBuffer;

            if (!IsRunning())
            {
                return;
            }

            videoEncoder.Encode(videoCapture.Buffer);

            while ((encodedVideoBuffer = videoEncoder.Read()) != null)
            {
                if (videoDecoder != null)
                {
                    videoDecoder.Decode(encodedVideoBuffer);
                }

                AddVideoBuffer(encodedVideoBuffer);
            }

            if (videoPreview != null)
            {
                videoPreview.RenderDirect(videoCapture.Buffer);
            }
        }

        public bool IsRunning()
        {
            lock (this)
            {
                return running;
            }
        }

        public void StopServer()
        {
            if (!IsRunning())
            {
                return;
            }

            lock (this)
            {
                running = false;
            }
            
            if (videoEncoder != null)
            {
                videoEncoder.StopEncoding();
                videoEncoder = null;
            }

            if (videoDecoder != null)
            {
                videoDecoder.StopDecoding();
                videoDecoder = null;
            }

            if (videoCapture != null)
            {
                videoCapture.StopCapturing();
                videoCapture = null;
            }

            socket.Close();
            outputBuffers.Clear();

            readThread = null;
            writeThread = null;
            listenThread = null;
            socket = null;
            outputBuffers = new BufferPool();
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

                RestartStream();

                client.socket = clientSocket;
                client.stream = new NetworkStream(clientSocket);
                client.writer = new BinaryWriter(client.stream);

                clients.Add(client);
                
            }
        }

        protected void AddVideoBuffer(byte[] buffer)
        {
            if (!IsRunning() || clients.Count <= 0) {
                return;
            }

            if (buffer.Length <= 0)
            {
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

        List<ConnectedClient> killList = new List<ConnectedClient>();

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
                    try
                    {
                        client.writer.Write(nextBuffer);
                    }
                    catch (Exception e)
                    {
                        KillClient(client);
                    }
                }

                foreach (ConnectedClient client in killList)
                {
                    clients.Remove(client);
                }

                killList.Clear();
            }
        }

        public void KillClient(ConnectedClient client)
        {
            client.writer.Close();
            client.socket.Close();

            lock (killList)
            {
                killList.Add(client);
            }
        }

        protected void readThreadMain()
        {

        }

        public class ConnectedClient
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
