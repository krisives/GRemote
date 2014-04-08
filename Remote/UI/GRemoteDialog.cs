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

namespace GRemote
{
    public partial class GRemoteDialog : Form
    {
        public static String Version
        {
            get
            {
                return "0.0.15";
            }
        }

        private VideoCapture videoCapture;
        private CaptureArea areaDialog;
        private FFMpeg ffmpeg;
        private SessionDialog sessionDialog;
        private PreferencesDialog prefsDialog;
        private AboutDialog aboutDialog;
        private ServerSettings serverSettings = new ServerSettings();
        private ServerSession serverSession;
        private ClientSession clientSession;
        private UpdateChecker updateChecker;
        private int lastEncodedBytes = 0;
        private int lastKBps = 0;
        private int lastFrameCount = 0;

        public GRemoteDialog()
        {
            ffmpeg = new FFMpeg();
            areaDialog = new CaptureArea(this);
            sessionDialog = new SessionDialog();
            prefsDialog = new PreferencesDialog(this);

            InitializeComponent();

            videoScreen.GRemote = this;
            Text = String.Format("GRemote ({0})", Version);
        }

        /// <summary>
        /// Checks if the server has been started and is running.
        /// </summary>
        public bool IsServerRunning
        {
            get
            {
                return serverSession != null && serverSession.IsRunning;
            }
        }

        /// <summary>
        /// Checks if the client has been started and is running.
        /// </summary>
        public bool IsClientRunning
        {
            get
            {
                return clientSession != null && clientSession.IsRunning;
            }
        }

        /// <summary>
        /// If the server is running, it will be stopped, otherwise StartServer()
        /// will be called.
        /// </summary>
        public void ToggleServer()
        {
            if (IsServerRunning)
            {
                StopServer();
                hostItem.Text = "Host";
                connectItem.Enabled = true;
                hostItem.Enabled = true;
            }
            else
            {
                if (StartServer())
                {
                    hostItem.Text = "Stop Server";
                    hostItem.Enabled = true;
                    connectItem.Enabled = false;
                }
            }
        }

        public void ToggleClient()
        {
            if (IsServerRunning)
            {
                MessageBox.Show("Server is running");
                return;
            }

            if (IsClientRunning)
            {
                StopClient();
                connectItem.Text = "Connect";
            }
            else
            {
                if (StartClient())
                {
                    connectItem.Text = "Disconnect";
                }
            }
        }

        /// <summary>
        /// Starts the recording and server.
        /// </summary>
        public bool StartServer()
        {
            sessionDialog.addressBox.Text = "0.0.0.0";
            sessionDialog.portBox.Text = "9999";
            sessionDialog.finishButton.Text = "Begin Recording";
            
            switch (sessionDialog.ShowDialog())
            {
                case DialogResult.OK:
                    break;
                default:
                    return false;
            }

            if (videoCapture == null)
            {
                videoCapture = new VideoCapture(areaDialog.Width, areaDialog.Height);
                videoCapture.SetPosition(areaDialog.Left, areaDialog.Top);
            }

            videoScreen.SetVideoSize(videoCapture.Width, videoCapture.Height);

            serverSettings.Address = sessionDialog.addressBox.Text;
            serverSettings.PortString = sessionDialog.portBox.Text;
            
            serverSession = new ServerSession(FFmpeg, videoCapture, serverSettings);
            //serverSession.TargetWindow = areaDialog.TargetWindow;
            serverSession.Preview = videoScreen;
            serverSession.StartServer();
            statusLabel.Text = "Recording...";

            return true;
        }

        /// <summary>
        /// Stops the ServerSession and any recording.
        /// </summary>
        public void StopServer()
        {
            if (serverSession != null)
            {
                serverSession.StopServer();
            }

            if (videoCapture != null)
            {
                videoCapture.StopCapturing();
                videoCapture = null;
            }

            statusLabel.Text = "Not Recording";
            videoScreen.Refresh();
        }

        /// <summary>
        /// Shows a dialog and connects a ClientSession to begin
        /// playback.
        /// </summary>
        public bool StartClient()
        {
            if (clientSession != null)
            {
                clientSession.StopClient();
                clientSession = null;
            }

            sessionDialog.addressBox.Text = "localhost";
            sessionDialog.portBox.Text = "9999";
            sessionDialog.finishButton.Text = "Connect";

            switch (sessionDialog.ShowDialog())
            {
                case DialogResult.OK:
                    break;
                default:
                    return false;
            }

            int portNumber;

            try
            {
                portNumber = int.Parse(sessionDialog.portBox.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                portNumber = 9999;
            }

            videoScreen.SetVideoSize(800, 600);
            clientSession = new ClientSession(this, sessionDialog.addressBox.Text, portNumber);
            clientSession.StartClient();
            statusLabel.Text = "Connecting...";
            recordItem.Enabled = false;
            connectItem.Text = "Disconnect";
            Refresh();

            return true;
        }

        /// <summary>
        /// Stops the current ClientSession and it's playback. If there is
        /// no connected session this does nothing.
        /// </summary>
        public void StopClient()
        {
            if (clientSession != null)
            {
                clientSession.StopClient();
            }

            recordItem.Enabled = true;
            connectItem.Text = "Connect";
            Refresh();
        }

        /// <summary>
        /// Stops any recording, playback, server, client, etc.
        /// </summary>
        public void Stop()
        {
            if (videoCapture != null)
            {
                videoCapture.StopCapturing();
            }

            StopServer();
            StopClient();

            if (aboutDialog != null)
            {
                aboutDialog.Close();
                aboutDialog.Dispose();
                aboutDialog = null;
            }

            if (areaDialog != null)
            {
                areaDialog.Close();
                areaDialog.Dispose();
                areaDialog = null;
            }
        }

        /// <summary>
        /// Information about where FFMpeg is
        /// </summary>
        public FFMpeg FFmpeg
        {
            get
            {
                return ffmpeg;
            }
        }

        /// <summary>
        /// Shows the about dialog
        /// </summary>
        public void ShowAboutDialog()
        {
            if (aboutDialog == null)
            {
                aboutDialog = new AboutDialog();
            }

            aboutDialog.ShowDialog(this);
        }

        /// <summary>
        /// Shows the preferences dialog.
        /// </summary>
        public void ShowPreferences()
        {
            if (prefsDialog.ShowDialog(this) != DialogResult.OK)
            {
                //return;
            }

            Console.WriteLine("Settings saved");

            if (prefsDialog.FileOutputEnabled)
            {
                serverSettings.SetFileOutput(prefsDialog.FileOutputPath);
            }
            else
            {
                serverSettings.DisableFileOutput();
            }

            if (prefsDialog.ffmpegBox.Text.Length > 0)
            {
                ffmpeg.Path = prefsDialog.ffmpegBox.Text;
            }
        }

        /// <summary>
        /// Gets the VideoPreview control used to display playback
        /// or recording.
        /// </summary>
        public VideoScreen VideoPreview
        {
            get
            {
                return videoScreen;
            }
        }

        /// <summary>
        /// Updates the capture position if recording is enabled.
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetCapturePos(int x, int y)
        {
            if (videoCapture != null)
            {
                videoCapture.SetPosition(x, y);
            }
        }

        /// <summary>
        /// Changes the video codec used when recording. If the server is already running
        /// the codec is changed using SetVideoCodec(), otherwise the EncoderSettings are
        /// modified so that the next time recording begins it will use this codec.
        /// 
        /// This method also updates menu items.
        /// </summary>
        /// <param name="codec">Codec being used (like EncoderSettings.codec)</param>
        public void SetVideoCodec(String codec)
        {

            if (serverSession != null)
            {
                serverSession.SetVideoCodec(codec);
            }
            else
            {
                serverSettings.EncoderSettings.codec = codec;
            }

            UI.UpdateMenuCheckGroup(codecItem, codec);
        }

        public void SetBitrate(int kbps)
        {
            ToolStripMenuItem item;

            if (IsServerRunning)
            {
                serverSession.SetBitrate(kbps);
            }
            else
            {
                serverSettings.EncoderSettings.videoRate = kbps;
            }

            item = UI.UpdateMenuCheckGroup(bitrateItem, kbps.ToString());
            bitrateItemCustom.Checked = (item == null);

            if (bitrateItemCustom.Checked)
            {
                bitrateItemCustom.Text = String.Format("Custom ({0} KB/s)", (kbps / 8));
            }
        }

        public void SetBitrate(String str)
        {
            SetBitrate(int.Parse(str));
        }

        public void SetWindow(IntPtr hwnd)
        {
            if (serverSession != null)
            {
                //serverSession.TargetWindow = hwnd;
            }
        }

        public void SetScaleMode(ScaleMode scaleMode)
        {
            centerItem.Checked = (scaleMode == ScaleMode.CENTER);
            stretchItem.Checked = (scaleMode == ScaleMode.STRETCHED);

            videoScreen.ScaleMode = scaleMode;
        }

        protected void UpdateStats()
        {
            int encodedBytes = 0;
            int frameCount = 0;

            if (IsServerRunning)
            {
                encodedBytes = serverSession.Encoder.TotalBytes;
                frameCount = serverSession.FrameIndex;
            }
            else if (IsClientRunning)
            {
                encodedBytes = clientSession.TotalEncodedBytes;
                frameCount = clientSession.FrameIndex;
            }
            else
            {
                frameCount = 0;
                encodedBytes = 0;
                lastFrameCount = 0;
                lastEncodedBytes = 0;
            }
            
            int delta = (encodedBytes - lastEncodedBytes);
            int bytesPerSecond = delta * (1000 / statsTimer.Interval);
            int KBps = (bytesPerSecond / 1024);
            int adjustedKBps = ((KBps + lastKBps) / 2);

            bandwidthLabel.Text = String.Format("{0} KB/s", adjustedKBps);

            lastKBps = KBps;
            lastEncodedBytes = encodedBytes;

            int fps = (frameCount - lastFrameCount);

            fpsLabel.Text = String.Format("{0} fps", fps);
            lastFrameCount = frameCount;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bandwidthTimer_Tick(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPreferences();
        }

        private void hostMenuItem_Click(object sender, EventArgs e)
        {
            ToggleServer();
        }

        private void joinMenuItem_Click(object sender, EventArgs e)
        {
            ToggleClient();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void GRemoteDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }

        private void checkUpdatesItem_Click(object sender, EventArgs e)
        {
            if (updateChecker == null)
            {
                updateChecker = new UpdateChecker();
            }

            updateChecker.Check(delegate(bool hasUpdate, String message)
            {
                if (hasUpdate)
                {
                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show(message);
                }
            });
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

        private void selectAreaButton_Click(object sender, EventArgs e)
        {
            areaDialog.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutDialog();
        }

        private void bitrateItemCustom_Click(object sender, EventArgs e)
        {
            CustomBitrateDialog bitrateForm = new CustomBitrateDialog();
            
            if (bitrateForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            SetBitrate(bitrateForm.KilobitsPerSecond);
        }

        private void bitrateItem_Click(object sender, EventArgs e)
        {
            SetBitrate((sender as ToolStripMenuItem).Tag as String);
        }

        private void centerItem_Click(object sender, EventArgs e)
        {
            SetScaleMode(ScaleMode.CENTER);
        }

        private void stretchItem_Click(object sender, EventArgs e)
        {
            SetScaleMode(ScaleMode.STRETCHED);
        }

    }
}
