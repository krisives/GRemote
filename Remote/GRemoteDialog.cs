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
        public static String Version
        {
            get
            {
                return "0.0.5";
            }
        }

        VideoCapture videoCapture;
        CaptureArea boundsForm;
        FFMpeg ffmpeg;
        VirtualInput virtualInput;
        IntPtr targetWindowPtr;
        SessionDialog sessionDialog;
        PreferencesDialog prefsDialog;
        AboutDialog aboutDialog;
        ServerSession serverSession;
        ClientSession clientSession;
        UpdateChecker updateChecker;
        EncoderSettings encoderSettings = new EncoderSettings();

        int lastEncodedBytes = 0;
        int lastKBps = 0;

        public GRemoteDialog()
        {
            ffmpeg = new FFMpeg();
            boundsForm = new CaptureArea(this);
            sessionDialog = new SessionDialog();
            prefsDialog = new PreferencesDialog();

            InitializeComponent();
        }

        public bool IsServerRunning
        {
            get
            {
                return serverSession != null && serverSession.IsRunning();
            }
        }

        public void ToggleServer()
        {
            if (IsServerRunning)
            {
                StopServer();
                hostMenuItem.Text = "Host";
                joinMenuItem.Enabled = true;
                hostMenuItem.Enabled = true;
            }
            else
            {
                StartServer();
                hostMenuItem.Text = "Stop Server";
                hostMenuItem.Enabled = true;
                joinMenuItem.Enabled = false;
            }
        }

        public void StartServer()
        {
            sessionDialog.addressBox.Text = "0.0.0.0";
            sessionDialog.portBox.Text = "9999";
            sessionDialog.finishButton.Text = "Begin Recording";
            
            switch (sessionDialog.ShowDialog())
            {
                case DialogResult.OK:
                    break;
                default:
                    return;
            }

            int portNumber;

            try
            {
                portNumber = int.Parse(sessionDialog.portBox.Text);
            }
            catch (Exception e)
            {
                portNumber = 9999;
            }

            if (videoCapture == null)
            {
                videoCapture = new VideoCapture(boundsForm.Width, boundsForm.Height);
                videoCapture.SetCapturePos(boundsForm.Left, boundsForm.Top);
            }

            videoPreview.SetSize(videoCapture.Width, videoCapture.Height);
            serverSession = new ServerSession(FFmpeg, videoCapture, sessionDialog.addressBox.Text, portNumber, encoderSettings);
            serverSession.Preview = videoPreview;
            serverSession.StartServer();
            statusLabel.Text = "Recording...";
        }

        public void StopServer()
        {
            if (serverSession != null)
            {
                serverSession.StopServer();
            }

            statusLabel.Text = "Not Recording";
            videoPreview.Refresh();
        }

        public void StartClient()
        {
            if (clientSession != null)
            {
                clientSession.StopClient();
                clientSession = null;
            }

            sessionDialog.addressBox.Text = "";
            sessionDialog.portBox.Text = "9999";
            sessionDialog.finishButton.Text = "Connect";

            switch (sessionDialog.ShowDialog())
            {
                case DialogResult.OK:
                    break;
                default:
                    return;
            }

            int portNumber;

            try
            {
                portNumber = int.Parse(sessionDialog.portBox.Text);
            }
            catch (Exception e)
            {
                portNumber = 9999;
            }

            videoPreview.SetSize(800, 600);
            clientSession = new ClientSession(this, sessionDialog.addressBox.Text, portNumber);
            clientSession.StartClient();
        }

        public void StopClient()
        {
            if (clientSession != null)
            {
                clientSession.StopClient();
            }
        }

        public void Stop()
        {
            if (videoCapture != null)
            {
                videoCapture.StopCapturing();
            }

            StopServer();
            StopClient();
        }

        public FFMpeg FFmpeg
        {
            get
            {
                return ffmpeg;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void bandwidthTimer_Tick(object sender, EventArgs e)
        {
            /*
            if (IsServerRunning)
            {
                
            }

            int encodedBytes = videoEncoder.TotalBytes;
            int delta = (encodedBytes - lastEncodedBytes);
            int bytesPerSecond = delta * (1000 / bandwidthTimer.Interval);
            int KBps = (bytesPerSecond / 1024);
            int adjustedKBps = ((KBps + lastKBps) / 2);


            bandwidthLabel.Text = adjustedKBps + " KB/s";

            lastKBps = KBps;
            lastEncodedBytes = encodedBytes;
            */
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

            aboutDialog.ShowDialog(this);
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
            prefsDialog.ShowDialog(this);
        }

        private void hostMenuItem_Click(object sender, EventArgs e)
        {
            ToggleServer();
        }

        private void joinMenuItem_Click(object sender, EventArgs e)
        {
            StartClient();
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
            Stop();
        }

        public VideoPreview VideoPreview
        {
            get
            {
                return videoPreview;
            }
        }

        public void SetCapturePos(int x, int y)
        {
            if (videoCapture != null)
            {
                videoCapture.SetCapturePos(x, y);
            }
        }

        private void checkUpdatesItem_Click(object sender, EventArgs e)
        {
            if (updateChecker == null)
            {
                updateChecker = new UpdateChecker();
            }

            updateChecker.Check(delegate(bool hasUpdate) {
                if (hasUpdate)
                {
                    MessageBox.Show("New version is available");
                }
                else
                {
                    MessageBox.Show("Already up to date");
                }
            });
        }

        public void SetVideoCodec(String codec)
        {

            Console.WriteLine("Codec changed: {0}", codec);

            if (serverSession != null)
            {
                serverSession.SetVideoCodec(codec);
            }
            else
            {
                encoderSettings.codec = codec;
            }

            foreach (ToolStripMenuItem item in codecItem.DropDownItems)
            {
                item.Checked = encoderSettings.codec.Equals(item.Tag);
            }
        }

        private void codecItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item;

            if (!(sender is ToolStripMenuItem))
            {
                return;
            }

            item = (ToolStripMenuItem)sender;
            SetVideoCodec(item.Tag as string);
        }

        private void xvidItem_Click(object sender, EventArgs e)
        {
            SetVideoCodec("libxvid");
        }

        private void wmv1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVideoCodec("wmv1");
        }

        private void wmv2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVideoCodec("wmv2");
        }

        private void theoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVideoCodec("theora");
        }


    }
}
