using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GRemote
{
    public class FFMpeg
    {
        String path;

        public FFMpeg()
        {
            this.path = Directory.GetCurrentDirectory() + "\\ffmpeg.exe";

            if (!File.Exists(this.path))
            {
                throw new Exception("Cannot find ffmpeg.exe");
            }
        }

        public string Path
        {
            get {
                
                return path;
            }
        }
    }
}
