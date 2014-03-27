using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Cache;

// using DirectShowLib;

namespace GRemote
{
    public partial class GRemoteDialog : Form
    {
        public static String Version = "0.0.4";

        BoundsForm boundsForm = new BoundsForm();

        volatile bool recording = false;
        volatile bool playback = false;

        FFMpeg ffmpeg;

        VideoCapture videoCapture;
        VideoEncoder videoEncoder;
        VideoDecoder videoDecoder;

        VirtualInput virtualInput;
        IntPtr targetWindowPtr;

        SessionDialog sessionDialog = new SessionDialog();
        AboutDialog aboutDialog;
        PreferencesDialog prefsDialog = new PreferencesDialog();
        WebClient client;

        ServerSession serverSession;
        ClientSession clientSession;

        public GRemoteDialog()
        {
            ffmpeg = new FFMpeg();

            WebRequest.DefaultWebProxy = null;
            WebRequest.DefaultCachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            client = new WebClient();
            InitializeComponent();
        }

        // Happens X times per second (FPS)
        private void snapshotTimer_Tick(object sender, EventArgs e)
        {
            if (!recording)
            {
                return;
            }

            // Capture raw screen
            videoCapture.SetCapturePos(boundsForm.Left, boundsForm.Top);
            videoCapture.Capture();

            // Pass screen off to encoder (asynchronous)
            videoEncoder.Encode(videoCapture.Buffer);

            byte[] encodedBuffer;

            while ((encodedBuffer = videoEncoder.Read()) != null)
            {
                videoDecoder.Decode(encodedBuffer);

                if (serverSession != null)
                {
                    serverSession.AddVideoBuffer(encodedBuffer);
                }
            }

            if (WindowState != FormWindowState.Minimized)
            {
                //bg.Render();
                videoPreview.Render();
            }
        }

        private void playbackTimer_Tick(object sender, EventArgs e)
        {
            if (!playback)
            {
                return;
            }

            if (WindowState != FormWindowState.Minimized)
            {
                videoPreview.RenderDirect(videoDecoder.Buffer);
                videoPreview.Render();
            }
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            ToggleRecording();
        }

        public void ToggleRecording()
        {
            if (recording)
            {
                StopRecording();
            }
            else
            {
                StartRecording();
            }
        }

        public void StartRecording()
        {
            if (recording)
            {
                return;
            }

            recording = true;

            int w = boundsForm.Width, h = boundsForm.Height;

            recordButton.Text = "Stop Recording";
            statusLabel.Text = "Recording...";

            //bg = bufferContext.Allocate(videoPreview.CreateGraphics(), new Rectangle(0, 0, w, h));
            //g = bg.Graphics;

            //targetWindowPtr =  boundsForm.TargetWindow;
            //Process gameProcess = Process.GetProcessesByName("LANoire")[0];
            //targetWindowPtr = gameProcess.MainWindowHandle;

            videoCapture = new VideoCapture(w, h);
            videoCapture.SetCapturePos(boundsForm.Left, boundsForm.Top);

            videoEncoder = new VideoEncoder(ffmpeg, w, h);
            videoEncoder.FileRecordingEnabled = prefsDialog.enableSave.Checked;
            videoEncoder.FileRecordingPath = prefsDialog.saveFilename.Text;
            videoEncoder.StartEncoding();

            videoDecoder = new VideoDecoder(ffmpeg, w, h);
            videoDecoder.StartDecoding();

            virtualInput = new VirtualInput(targetWindowPtr);

            videoPreview.SetBuffers(videoCapture.Buffer, videoDecoder.Buffer);

            if (clientSession != null) {
                clientSession.SetDecoder(videoDecoder);
            }

            bandwidthTimer.Start();
            snapshotTimer.Start();
        }

        public void RestartStream()
        {
            /*
            videoEncoder.StopEncoding();
            videoDecoder.StopDecoding();

            videoEncoder.StartEncoding();
            videoDecoder.StopDecoding();
            */
            StopRecording();
            StartRecording();
        }

        public void StopRecording()
        {
            if (!recording)
            {
                return;
            }

            recording = false;
            snapshotTimer.Stop();
            bandwidthTimer.Stop();
            videoEncoder.StopEncoding();
            videoDecoder.StopDecoding();

            recordButton.Text = "Start";
            statusLabel.Text = "Not Recording";

            videoPreview.Refresh();
        }

        private void previewPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        int lastEncodedBytes = 0;
        int lastKBps = 0;

        private void bandwidthTimer_Tick(object sender, EventArgs e)
        {
            if (recording == false || videoEncoder == null)
            {
                return;
            }

            virtualInput.TriggerKey();

            int encodedBytes = videoEncoder.TotalBytes;
            int delta = (encodedBytes - lastEncodedBytes);
            int bytesPerSecond = delta * (1000 / bandwidthTimer.Interval);
            int KBps = (bytesPerSecond / 1024);
            int adjustedKBps = ((KBps + lastKBps) / 2);


            bandwidthLabel.Text = adjustedKBps + " KB/s";

            lastKBps = KBps;
            lastEncodedBytes = encodedBytes;
        }

        private void selectAreaButton_Click(object sender, EventArgs e)
        {
            boundsForm.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutDialog();
        }

        public void ShowAboutDialog()
        {
            if (aboutDialog == null)
            {
                aboutDialog = new AboutDialog();
            }

            aboutDialog.Show();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPreferences();
        }

        public void ShowPreferences()
        {
            prefsDialog.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://raw.githubusercontent.com:443/krisives/GRemote/master/VERSION.txt");

            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(uri);
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            String version = e.Result.Trim();
            Console.WriteLine(version);

            if (version != GRemoteDialog.Version)
            {
                MessageBox.Show("Version " + version + " is available!");
            }
        }

        private void hostMenuItem_Click(object sender, EventArgs e)
        {
            ToggleHosting();
        }

        public void ToggleHosting()
        {
            if (serverSession == null)
            {
                HostSession();
            }
            else
            {
                serverSession.StopServer();
                serverSession = null;
                hostMenuItem.Text = "Host";
            }
        }

        public void TogglePlayback()
        {
            if (clientSession == null)
            {
                JoinSession();
            }
            else
            {
                StopPlayback();
            }
        }

        public void StopPlayback()
        {
            playback = false;
            clientSession.StopClient();
            clientSession = null;
            joinMenuItem.Text = "Connect";
            playbackTimer.Stop();
        }

        public void JoinSession()
        {
            sessionDialog.addressBox.Text = "";
            sessionDialog.button1.Text = "Join Session";
            sessionDialog.Text = "Join Session";
            sessionDialog.ShowDialog(this);

            int w = boundsForm.Width;
            int h = boundsForm.Height;

            videoDecoder = new VideoDecoder(ffmpeg, w, h);
            videoDecoder.StartDecoding();

            joinMenuItem.Text = "Disconnect";
            clientSession = new ClientSession(this, sessionDialog.addressBox.Text, int.Parse(sessionDialog.portBox.Text));

            playback = true;
            clientSession.StartClient();
            playbackTimer.Start();
        }

        public void HostSession()
        {
            sessionDialog.addressBox.Text = "0.0.0.0";
            sessionDialog.button1.Text = "Begin Hosting";
            sessionDialog.Text = "Host Session";
            sessionDialog.ShowDialog(this);

            hostMenuItem.Text = "Stop Hosting";
            serverSession = new ServerSession(this, sessionDialog.addressBox.Text, int.Parse(sessionDialog.portBox.Text));
            serverSession.StartServer();
            
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TogglePlayback();
        }

        public void SetPreviewMode(PreviewMode mode)
        {
            if (videoPreview != null)
            {
                videoPreview.PreviewMode = mode;
            }

            switch (mode)
            {
                case PreviewMode.NONE:
                    noneToolStripMenuItem.Checked = true;
                    uncompressedToolStripMenuItem.Checked = false;
                    compressedToolStripMenuItem.Checked = false;
                    splitViewToolStripMenuItem.Checked = false;
                    break;
                case PreviewMode.UNCOMPRESSED:
                    noneToolStripMenuItem.Checked = false;
                    uncompressedToolStripMenuItem.Checked = true;
                    compressedToolStripMenuItem.Checked = false;
                    splitViewToolStripMenuItem.Checked = false;
                    break;
                case PreviewMode.COMPRESSED:
                    noneToolStripMenuItem.Checked = false;
                    uncompressedToolStripMenuItem.Checked = false;
                    compressedToolStripMenuItem.Checked = true;
                    splitViewToolStripMenuItem.Checked = false;
                    break;
                case PreviewMode.SPLIT:
                    noneToolStripMenuItem.Checked = false;
                    uncompressedToolStripMenuItem.Checked = false;
                    compressedToolStripMenuItem.Checked = false;
                    splitViewToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void uncompressedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPreviewMode(PreviewMode.UNCOMPRESSED);
        }

        private void compressedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPreviewMode(PreviewMode.COMPRESSED);
        }

        private void splitViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPreviewMode(PreviewMode.SPLIT);
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPreviewMode(PreviewMode.NONE);
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void GRemoteDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopRecording();

            if (serverSession != null)
            {
                serverSession.StopServer();
            }
        }
    }
}
