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

// using DirectShowLib;

namespace GRemote
{
    public partial class GRemote : Form
    {
        public static String Version = "0.0.2";

        BoundsForm boundsForm = new BoundsForm();
        volatile bool recording = false;
        BufferedGraphicsContext bufferContext;
        BufferedGraphics bg;
        Graphics g;
        FFMpeg ffmpeg;
        VideoCapture videoCapture;
        VideoEncoder videoEncoder;
        VideoDecoder videoDecoder;
        VirtualInput virtualInput;
        IntPtr targetWindowPtr;

        AboutDialog aboutDialog;
        PreferencesDialog prefsDialog;

        public GRemote()
        {
            ffmpeg = new FFMpeg();

            bufferContext = BufferedGraphicsManager.Current;
            InitializeComponent();
             
            
        }

   


        private void snapshotTimer_Tick(object sender, EventArgs e)
        {
            if (!recording)
            {
                return;
            }

            videoCapture.SetCapturePos(boundsForm.Left, boundsForm.Top);
            videoCapture.Capture();
            videoEncoder.Encode(videoCapture.Buffer);

            byte[] encodedBuffer;

            while ((encodedBuffer = videoEncoder.Read()) != null)
            {
                videoDecoder.Decode(encodedBuffer);
            }

            g.DrawImage(videoCapture.Buffer, 0, 0);

           // while (videoDecoder.Read())
           // {
          //      
           // }

            lock (videoDecoder.Buffer)
            {
                g.DrawImage(videoDecoder.Buffer, 0, 0);

            }

            if (WindowState != FormWindowState.Minimized)
            {
                bg.Render();
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

            bg = bufferContext.Allocate(previewPanel.CreateGraphics(), new Rectangle(0, 0, w, h));
            g = bg.Graphics;

            //targetWindowPtr =  boundsForm.TargetWindow;
            //Process gameProcess = Process.GetProcessesByName("LANoire")[0];
           // targetWindowPtr = gameProcess.MainWindowHandle;

            videoCapture = new VideoCapture(w, h);
            videoCapture.SetCapturePos(boundsForm.Left, boundsForm.Top);

            videoEncoder = new VideoEncoder(ffmpeg, w, h);
            videoEncoder.StartEncoding();

            videoDecoder = new VideoDecoder(ffmpeg, w, h);
            videoDecoder.StartDecoding();

            virtualInput = new VirtualInput(targetWindowPtr);

            bandwidthTimer.Start();
            snapshotTimer.Start();
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

            previewPanel.Refresh();
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
            if (prefsDialog == null)
            {
                prefsDialog = new PreferencesDialog();
            }

            prefsDialog.Show();
        }

    }
}
