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
    public partial class CustomBitrateForm : Form
    {
        public CustomBitrateForm()
        {
            InitializeComponent();
            UpdateLabels();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bitsBox_ValueChanged(object sender, EventArgs e)
        {
            bytesBox.Value = bitsBox.Value / 8;
            UpdateLabels();
        }

        private void bytesBox_ValueChanged(object sender, EventArgs e)
        {
            bitsBox.Value = bytesBox.Value * 8;
            UpdateLabels();
        }

        protected void UpdateLabels()
        {
            megaBitsLabel.Text = String.Format("{0:0.00} Mbps", (double)bitsBox.Value / 1000.0);
            megaBytesLabel.Text = String.Format("{0:0.00} MB/s", (double)bytesBox.Value / 1000.0);
        }

        public int KilobitsPerSecond
        {
            get
            {
                return (int)bitsBox.Value;
            }
        }
    }
}
