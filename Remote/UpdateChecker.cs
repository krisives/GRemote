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

        public delegate void CheckEvent(bool hasUpdate, String msg);

        public void Check(CheckEvent checkEvent)
        {
            Process p = CreateWgetProcess();
            
            p.Start();
            p.WaitForExit();

            String version = File.ReadAllText("UPDATE.txt");

            if (version == null)
            {
                version = "";
            }

            version = version.Trim();

            if (version == "")
            {
                checkEvent(false, "No response from Github");
                return;
            }

            if (IsNewerVersion(GRemoteDialog.Version, version))
            {
                checkEvent(true, String.Format("Version {0} is available on Github", version));
            }
            else
            {
                checkEvent(false, "Already up to date");
            }
        }

        public bool IsNewerVersion(String a, String b)
        {
            int[] partsA = new int[3];
            int[] partsB = new int[3];

            Console.WriteLine("{0} vs {1}", a, b);

            if (!ParseVersion(a, partsA))
            {
                return true;
            }

            if (!ParseVersion(b, partsB))
            {
                return false;
            }

            if (partsB[0] > partsA[0])
            {
                return true;
            }

            if (partsB[1] > partsA[1])
            {
                return true;
            }

            return partsB[2] > partsA[2];
        }

        protected bool ParseVersion(String str, int[] nums)
        {
            String[] parts;

            if (str == null || str.Length <= 0)
            {
                return false;
            }

            parts = str.Split('.');

            if (parts.Length != 3)
            {
                return false;
            }

            try
            {
                nums[0] = int.Parse(parts[0]);
                nums[1] = int.Parse(parts[1]);
                nums[2] = int.Parse(parts[2]);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        protected Process CreateWgetProcess()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "wget.exe";
            p.StartInfo.Arguments = "--no-check-certificates -O UPDATE.txt 'https://raw.githubusercontent.com/krisives/GRemote/master/VERSION.txt'";

            return p;
        }
    }
}
