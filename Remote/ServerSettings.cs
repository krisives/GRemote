using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GRemote
{
    public class ServerSettings
    {
        string address;
        int port;
        EncoderSettings encoderSettings = new EncoderSettings();
        bool enableFileOutput;
        string fileOutput;

        public bool FileOutputEnabled
        {
            get
            {
                return enableFileOutput;
            }
        }

        public String FileOutputPath
        {
            get
            {
                return fileOutput;
            }
        }

        public void SetFileOutput(String file)
        {
            enableFileOutput = true;
            fileOutput = file;
        }

        public void DisableFileOutput()
        {
            enableFileOutput = false;
            fileOutput = null;
        }

        public String Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public IPAddress IPAddress
        {
            get
            {
                if (address == "" || address == "0.0.0.0" || address == "localhost")
                {
                    return IPAddress.Any;
                }

                return Dns.GetHostEntry(address).AddressList[0];
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                if (value == 0)
                {
                    port = 9999;
                    return;
                }

                port = value;
            }
        }

        public String PortString
        {
            get
            {
                return port.ToString();
            }
            set
            {
                try
                {
                    Port = int.Parse(value);
                }
                catch (Exception e)
                {
                    Port = 9999;
                }
            }
        }

        public EncoderSettings EncoderSettings
        {
            get
            {
                return encoderSettings;
            }
        }
    }
}
