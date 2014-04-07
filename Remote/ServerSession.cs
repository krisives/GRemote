using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using Interceptor;

namespace GRemote
{
    public class ServerSession
    {
        private FFMpeg ffmpeg;
        private Socket socket;
        private bool running;
        private StoppableThread listenThread;
        private StoppableThread writeThread;
        private StoppableThread readThread;
        private List<ConnectedClient> clients = new List<ConnectedClient>();
        private BufferPool outputBuffers = new BufferPool();
        private VideoCapture videoCapture;
        private VideoEncoder videoEncoder;
        private byte[] headerBuffer;
        private MemoryStream headerStream;
        private BinaryWriter headerBinary;
        private VideoPreview videoPreview;
        private ServerSettings settings;
        private List<ConnectedClient> killList = new List<ConnectedClient>();
        private IntPtr targetWindow = IntPtr.Zero;
        private InputPlayback inputPlayback;

        public ServerSession(FFMpeg ffmpeg, VideoCapture videoCapture, ServerSettings settings)
        {
            this.ffmpeg = ffmpeg;
            this.videoCapture = videoCapture;
            this.settings = settings;

            headerBuffer = new byte[1024];
            headerStream = new MemoryStream(headerBuffer, 0, 1024);
            headerBinary = new BinaryWriter(headerStream);
        }

        public ServerSettings Settings
        {
            get
            {
                return settings;
            }
        }

        public EncoderSettings EncoderSettings
        {
            get
            {
                return settings.EncoderSettings;
            }
        }

        public VideoEncoder Encoder
        {
            get
            {
                return videoEncoder;
            }
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

        public IntPtr TargetWindow
        {
            get
            {
                return targetWindow;
            }
            set
            {
                targetWindow = value;
            }
        }

        public void StartServer()
        {
            if (IsRunning)
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
            inputPlayback = new InputPlayback();

            videoEncoder = new VideoEncoder(ffmpeg, videoCapture.Width, videoCapture.Height);
            
            if (settings.FileOutputEnabled)
            {
                videoEncoder.EnableFileOutput(settings.FileOutputPath);
            }

            videoEncoder.StartEncoding(settings.EncoderSettings);

            // Create networking threads
            listenThread = new ServerListenThread(this, clients, socket);
            readThread = new ServerReadThread(this);
            writeThread = new ServerWriteThread(this, outputBuffers, clients, killList);

            // Start networking threads
            listenThread.Start();
            readThread.Start();
            writeThread.Start();

            videoCapture.StartCapturing();
            inputPlayback.StartPlayback();
        }

        public delegate void Callback();

        public void RestartStream(Callback f)
        {
            VideoStartPacket packet = new VideoStartPacket(videoCapture.Width, videoCapture.Height);
            videoEncoder.StopEncoding();
            outputBuffers.Clear();

            if (f != null)
            {
                f();
            }

            AddPacket(packet);

            videoEncoder.StartEncoding(settings.EncoderSettings);
        }

        public void RestartStream()
        {
            RestartStream(null);
        }

        protected void Snapshot()
        {
            try
            {
                SnapshotReal();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected void SnapshotReal()
        {
            byte[] encodedVideoBuffer;

            if (!IsRunning)
            {
                return;
            }

            videoEncoder.Encode(videoCapture.Buffer);

            while ((encodedVideoBuffer = videoEncoder.Read()) != null)
            {
                AddVideoBuffer(encodedVideoBuffer);
            }

            if (videoPreview != null)
            {
                videoPreview.RenderDirect(videoCapture.Buffer);
            }
        }

        public bool IsRunning
        {
            get {
                lock (this)
                {
                    return running;
                }
            }
        }

        public void StopServer()
        {
            if (!IsRunning)
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

            if (socket != null)
            {
                socket.Close();
                socket = null;
            }

            if (outputBuffers != null)
            {
                outputBuffers.Clear();
            }

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

            if (listenThread != null)
            {
                listenThread.Stop();
                listenThread = null;
            }

            if (inputPlayback != null)
            {
                inputPlayback.StopPlayback();
                inputPlayback = null;
            }

            outputBuffers = new BufferPool();
        }

        public void AddPacket(Packet packet)
        {
            outputBuffers.Add(packet.Buffer);
        }

        public void AddVideoBuffer(byte[] buffer)
        {
            if (!IsRunning || clients.Count <= 0) {
                return;
            }

            if (buffer.Length <= 0)
            {
                return;
            }

            // Create the 5 byte header
            byte[] packetBuffer = new byte[1 + 4];
            headerBinary.Seek(0, SeekOrigin.Begin);
            headerBinary.Write((byte)PacketType.VIDEO_UPDATE);
            headerBinary.Write((int)buffer.Length);
            headerBinary.Flush();

            // Copy and add the header/video data to the output buffer pool
            Array.Copy(headerBuffer, packetBuffer, packetBuffer.Length);
            outputBuffers.Add(packetBuffer);
            outputBuffers.Add(buffer);
        }

        public void KillClient(ConnectedClient client)
        {
            client.writer.Close();
            client.socket.Close();

            lock (killList)
            {
                killList.Add(client);
                outputBuffers.Pulse();
            }
        }

        public void SetVideoCodec(string codec)
        {
            settings.EncoderSettings.codec = codec;
            RestartStream();

        }

        public void SetBitrate(int kbps)
        {
            settings.EncoderSettings.videoRate = kbps;
            RestartStream();
        }
    }

    public class ConnectedClient
    {
        public Socket socket;
        public NetworkStream stream;
        public BinaryWriter writer;
    }


    public class ServerListenThread : StoppableThread
    {
        ServerSession server;
        Socket socket;
        ServerSettings settings;
        List<ConnectedClient> clients;

        public ServerListenThread(ServerSession server, List<ConnectedClient> clients, Socket socket)
        {
            this.server = server;
            this.settings = server.Settings;
            this.clients = clients;
            this.socket = socket;

            socket.Bind(new IPEndPoint(settings.IPAddress, settings.Port));
            socket.Listen(20);
        }

        protected override void RunThread()
        {
            Socket clientSocket;
            IPEndPoint ep = new IPEndPoint(settings.IPAddress, settings.Port);

            try
            {
                clientSocket = socket.Accept();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            if (clientSocket == null)
            {
                return;
            }

            ConnectedClient client = new ConnectedClient();

            client.socket = clientSocket;
            client.stream = new NetworkStream(clientSocket);
            client.writer = new BinaryWriter(client.stream);

            server.RestartStream(delegate()
            {
                clients.Add(client);
            });
        }
    }

    public class ServerReadThread : StoppableThread
    {
        ServerSession server;
        Input input;

        public ServerReadThread(ServerSession server)
        {
            this.server = server;
            
            

            
        }

        protected override void OnThreadStart()
        {
            this.input = new Input();

            // Be sure to set your keyboard filter to be able to capture key presses and simulate key presses
            // KeyboardFilterMode.All captures all events; 'Down' only captures presses for non-special keys; 'Up' only captures releases for non-special keys; 'E0' and 'E1' capture presses/releases for special keys
            input.KeyboardFilterMode = KeyboardFilterMode.All;
            // You can set a MouseFilterMode as well, but you don't need to set a MouseFilterMode to simulate mouse clicks

            // Finally, load the driver
            input.Load();
        }

        protected override void RunThread()
        {
            Thread.Sleep(1000);
        }

        [DllImport("user32.dll", SetLastError=true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
    }

    public class ServerWriteThread : StoppableThread
    {
        ServerSession server;
        BufferPool outputBuffers;
        List<ConnectedClient> killList;
        List<ConnectedClient> clients;

        public ServerWriteThread(ServerSession server, BufferPool outputBuffers, List<ConnectedClient> clients, List<ConnectedClient> killList)
        {
            this.server = server;
            this.outputBuffers = outputBuffers;
            this.clients = clients;
            this.killList = killList;
        }

        protected override void RunThread()
        {
             byte[] nextBuffer;
             
             nextBuffer = outputBuffers.Remove();
             
             if (nextBuffer == null)
             {
                 KillClients();
                 return;
             }
             
             foreach (ConnectedClient client in clients)
             {
                 try
                 {
                     client.writer.Write(nextBuffer);
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e);
                     server.KillClient(client);
                 }
             }
             
             KillClients();
        }

        protected void KillClients()
        {
            lock (killList)
            {
                foreach (ConnectedClient client in killList)
                {
                    clients.Remove(client);
                }

                killList.Clear();
            }
        }
    }
}
