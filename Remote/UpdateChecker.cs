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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

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

            Thread thread = new Thread(new ThreadStart(delegate()
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate(
                    object sender,
                    X509Certificate cert,
                    X509Chain chain,
                    SslPolicyErrors errors
                )
                {
                    return true;
                };

                WebRequest.DefaultWebProxy = null;
                WebClient client = new WebClient();
                client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                client.Proxy = null;
                client.Headers.Add(HttpRequestHeader.UserAgent, "GRemote");
                Console.WriteLine("Checking...");
                Console.WriteLine(client.DownloadString("https://raw.githubusercontent.com/krisives/GRemote/master/VERSION.txt"));
            }));

            thread.Start();
        }
    }
}
