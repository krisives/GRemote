using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace GRemote
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String d = Directory.GetCurrentDirectory();
            String p = Path.GetFileName(d);

            if (p == "Debug")
            {
                Directory.SetCurrentDirectory(d + "\\..\\..\\..\\");
            }

            Console.WriteLine(Directory.GetCurrentDirectory());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GRemoteDialog());
        }
    }
}
