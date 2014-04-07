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
    /// <summary>
    /// Checks the project Github page for new versions by downloading VERSION.txt
    /// and comparing it to GRemoteDialog.Version
    /// 
    /// Currently this only tells the user a new version is available.
    /// </summary>
    public class UpdateChecker
    {
        public UpdateChecker()
        {
            
        }

        /// <summary>
        /// A callback for checking if a new version is available.
        /// </summary>
        /// <param name="hasUpdate"></param>
        /// <param name="msg"></param>
        public delegate void CheckEvent(bool hasUpdate, String msg);

        /// <summary>
        /// Check if a new version is available. This calls the event callback
        /// upon completion.
        /// </summary>
        /// <param name="checkEvent"></param>
        public void Check(CheckEvent checkEvent)
        {
            // This uses wget because .NET is not working with Github for some reason
            // when I test. It gets mad about SSL and even with some extra workarounds
            // it still never does anything. Wget will also be used for the update
            // process, so this fits
            Process p = CreateWgetProcess();
            String version;

            p.Start();
            p.WaitForExit();

            try
            {
                version = File.ReadAllText("UPDATE.txt");
            }
            catch (Exception e)
            {
                checkEvent(false, String.Format("Failed to check for updates {0}", e.Message));
                return;
            }

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

        /// <summary>
        /// Checks if "x.y.z" is greater than "a.b.c" where both strings are
        /// semantic versions.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Parse a string like "x.y.z" into an existing array of numbers.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a process of wget.exe that downloads the most recent VERSION.txt locally
        /// as UPDATE.txt
        /// </summary>
        /// <returns></returns>
        protected Process CreateWgetProcess()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            
            p.StartInfo.FileName = "wget.exe";
            p.StartInfo.Arguments = "--no-check-certificate -O \"UPDATE.txt\" \"https://github.com/krisives/GRemote/raw/master/VERSION.txt\"";

            return p;
        }
    }
}
