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
using System.Drawing;

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
        private VideoPreview videoPreview;
        private ServerSettings settings;
        private List<ConnectedClient> killList = new List<ConnectedClient>();
        private InputPlayback inputPlayback;

        public ServerSession(FFMpeg ffmpeg, VideoCapture videoCapture, ServerSettings settings)
        {
            this.ffmpeg = ffmpeg;
            this.videoCapture = videoCapture;
            this.settings = settings;
        }

        /// <summary>
        /// Gets the current settings for this server
        /// </summary>
        public ServerSettings Settings
        {
            get
            {
                return settings;
            }
        }

        /// <summary>
        /// Gets the current encoding settings for this server
        /// </summary>
        public EncoderSettings EncoderSettings
        {
            get
            {
                return settings.EncoderSettings;
            }
        }

        /// <summary>
        /// Gets the video capture being used currently or null if no video capturing
        /// is happening.
        /// </summary>
        public VideoCapture VideoCapture
        {
            get
            {
                return videoCapture;
            }
        }

        /// <summary>
        /// Gets a video encoder being used currently or null if no encoder is
        /// being used currently.
        /// </summary>
        public VideoEncoder Encoder
        {
            get
            {
                return videoEncoder;
            }
        }

        /// <summary>
        /// Gets the video preview control used to preview the video capture.
        /// </summary>
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

        /// <summary>
        /// Starts the server. This begins the video capture, encoding, and network
        /// processes. It's safe to start a server more than once.
        /// </summary>
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

        /// <summary>
        /// A simple callback type accepts no types and returns nothing
        /// </summary>
        public delegate void Callback();

        /// <summary>
        /// Restarts the video encoding stream. This clears the output buffers
        /// to stop encoded video data from going out. If the callback is provided
        /// it will be called before the next packet is added the the queue.
        /// </summary>
        /// <param name="f"></param>
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

        /// <summary>
        /// Restart the video stream. This clears the output buffers to stop
        /// encoded video data from going out.
        /// </summary>
        public void RestartStream()
        {
            RestartStream(null);
        }

        /// <summary>
        /// Invoked when the video capture frame is ready. This passes data off to
        /// the video encoder
        /// </summary>
        /// <param name="buffer"></param>
        protected void Snapshot(Bitmap buffer)
        {
            byte[] encodedVideoBuffer;

            try
            {
                videoEncoder.Encode(buffer);

                while ((encodedVideoBuffer = videoEncoder.Read()) != null)
                {
                    AddVideoBuffer(encodedVideoBuffer);
                }

                if (videoPreview != null)
                {
                    videoPreview.RenderDirect(buffer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Checks if this server has been started
        /// </summary>
        public bool IsRunning
        {
            get {
                lock (this)
                {
                    return running;
                }
            }
        }

        /// <summary>
        /// Stops the server from capturing, encoding video, and all network activity. It's
        /// safe to stop a server more than once.
        /// </summary>
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

            if (videoCapture != null)
            {
                videoCapture.StopCapturing();
                videoCapture = null;
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

        /// <summary>
        /// Adds the bytes of a packet to the output pool, essentially sending it to all
        /// connected clients.
        /// </summary>
        /// <param name="packet"></param>
        public void AddPacket(Packet packet)
        {
            outputBuffers.Add(packet.Buffer);
        }

        /// <summary>
        /// Adds a buffer of encrypted video data to the output pool, essentially sending
        /// it to all connected clients.
        /// </summary>
        /// <param name="buffer">A buffer of encrypted video data</param>
        public void AddVideoBuffer(byte[] buffer)
        {
            if (buffer.Length <= 0)
            {
                return;
            }

            VideoUpdatePacket packet = new VideoUpdatePacket(buffer.Length);
            outputBuffers.Add(packet.Buffer, buffer);
        }

        /// <summary>
        /// Mark a client as killed. The socket and other resources will be
        /// destroyed soon after. It's okay to kill a client more than once.
        /// </summary>
        /// <param name="client"></param>
        public void KillClient(ConnectedClient client)
        {
            lock (killList)
            {
                if (client.killed)
                {
                    return;
                }

                killList.Add(client);
                client.killed = true;
                outputBuffers.Pulse();
            }
        }

        /// <summary>
        /// Closes any sockets or streams associated with the client. It's safe
        /// to kill a client more than once.
        /// </summary>
        /// <param name="client"></param>
        public void KillClientFinish(ConnectedClient client)
        {
            if (client.socket == null)
            {
                return;
            }

            try
            {
                client.socket.Close();
                client.socket.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            client.socket = null;
            client.stream = null;
        }

        /// <summary>
        /// Change the current video codec. This will cause the stream to restart.
        /// </summary>
        /// <param name="codec"></param>
        public void SetVideoCodec(string codec)
        {
            settings.EncoderSettings.codec = codec;
            RestartStream();
        }

        /// <summary>
        /// Changes the streaming bitrate. This will cause the stream to restart
        /// for connected clients.
        /// </summary>
        /// <param name="kbps"></param>
        public void SetBitrate(int kbps)
        {
            settings.EncoderSettings.videoRate = kbps;
            RestartStream();
        }
    }

    /// <summary>
    /// A connected client
    /// </summary>
    public class ConnectedClient
    {
        public Socket socket;
        public NetworkStream stream;
        public bool killed;
    }

    /// <summary>
    /// A thread that listens on a socket and handles incoming connections.
    /// </summary>
    public class ServerListenThread : StoppableThread
    {
        private ServerSession server;
        private Socket socket;
        private ServerSettings settings;
        private List<ConnectedClient> clients;

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

            server.RestartStream(delegate()
            {
                clients.Add(client);
            });
        }
    }

    /// <summary>
    /// A thread that reads data from the clients
    /// </summary>
    public class ServerReadThread : StoppableThread
    {
        private ServerSession server;
        private Input input;

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

    /// <summary>
    /// A thread that writes data to connected clients. It waits on buffers from
    /// the server session.
    /// </summary>
    public class ServerWriteThread : StoppableThread
    {
        private ServerSession server;
        private BufferPool outputBuffers;
        private List<ConnectedClient> killList;
        private List<ConnectedClient> clients;

        public ServerWriteThread(ServerSession server, BufferPool outputBuffers, List<ConnectedClient> clients, List<ConnectedClient> killList)
        {
            this.server = server;
            this.outputBuffers = outputBuffers;
            this.clients = clients;
            this.killList = killList;
        }

        protected override void RunThread()
        {
            byte[] nextBuffer = null;

            outputBuffers.Wait();

            while ((nextBuffer = outputBuffers.Remove()) != null)
            {
                HandleBuffer(nextBuffer);
            }

            RemoveKilledClients();
        }

        /// <summary>
        /// Sends a buffer to all the connected clients that are not killed
        /// </summary>
        /// <param name="nextBuffer"></param>
        protected void HandleBuffer(byte[] nextBuffer)
        {
            if (nextBuffer.Length <= 0)
            {
                return;
            }

            foreach (ConnectedClient client in clients)
            {
                if (client.killed)
                {
                    continue;
                }

                try
                {
                    client.stream.Write(nextBuffer, 0, nextBuffer.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    server.KillClient(client);
                }
            }
        }

        /// <summary>
        /// Removes clients that are marked as killed.
        /// </summary>
        protected void RemoveKilledClients()
        {
            lock (killList)
            {
                foreach (ConnectedClient client in killList)
                {
                    clients.Remove(client);

                    try
                    {
                        server.KillClientFinish(client);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                killList.Clear();
            }
        }
    }
}
