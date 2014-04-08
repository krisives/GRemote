using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GRemote
{
    /// <summary>
    /// Describes FFMpeg and settings for using it.
    /// </summary>
    public class FFMpeg
    {
        private String path;

        /// <summary>
        /// Constructs an FFMpeg object looking for ffmpeg.exe in the working
        /// </summary>
        public FFMpeg()
        {
            this.path = Directory.GetCurrentDirectory() + "\\ffmpeg.exe";

            if (!File.Exists(this.path))
            {
                throw new Exception("Cannot find ffmpeg.exe");
            }
        }

        /// <summary>
        /// Gets the full path to ffmpeg.exe
        /// </summary>
        public string Path
        {
            get {
                
                return path;
            }
            set
            {
                path = value;
            }
        }
    }
}
