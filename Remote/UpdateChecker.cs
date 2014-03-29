using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GRemote
{
    public class UpdateChecker
    {

        public UpdateChecker()
        {

        }

        public delegate void CheckEvent(bool hasUpdate);

        public void Check(CheckEvent checkEvent)
        {
            // Respones from GitHub are not working for
            // some reason right now so this is being
            // disabled
            MessageBox.Show("Not working currently :(");
        }
    }
}
