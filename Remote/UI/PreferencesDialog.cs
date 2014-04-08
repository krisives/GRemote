using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GRemote
{
    public partial class PreferencesDialog : Form
    {
        GRemoteDialog gRemote;

        public PreferencesDialog(GRemoteDialog gRemote)
        {
            this.gRemote = gRemote;
            InitializeComponent();

            ffmpegBox.Text = gRemote.FFmpeg.Path;
            formatBox.SelectedIndex = 0;
        }

        public bool FileOutputEnabled
        {
            get
            {
                return enableSave.Checked;
            }
        }

        public String FileOutputPath
        {
            get
            {
                return saveFilename.Text;
            }
        }

        private void PreferencesDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void enableSave_CheckedChanged(object sender, EventArgs e)
        {
            saveFilename.Enabled = enableSave.Checked;
            browseSaveButton.Enabled = enableSave.Checked;
        }

        private void browseSaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.FileName = saveFilename.Text;

            switch (fileDialog.ShowDialog())
            {
                case DialogResult.OK:
                    saveFilename.Text = fileDialog.FileName;
                    break;
            }
        }

        private void browseFFMpeg_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "ffmpeg.exe|ffmpeg.exe";

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ffmpegBox.Text = fileDialog.FileName;
        }
    }
}
