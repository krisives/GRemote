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
        public PreferencesDialog()
        {
            InitializeComponent();
        }

        private void PreferencesDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

  
    }
}
