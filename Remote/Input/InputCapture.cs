using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Interceptor;

namespace GRemote
{
    public class InputCapture
    {
        private Input input;
        private ClientSession client;
        private bool capturing = false;

        public InputCapture(ClientSession client)
        {
            this.client = client;
            this.input = new Input();

            input.KeyboardFilterMode = KeyboardFilterMode.All;

            input.Load();

            input.OnKeyPressed += new EventHandler<KeyPressedEventArgs>(OnKeyPressed);
        }

        public bool IsCapturing
        {
            get
            {
                lock (this)
                {
                    return capturing;
                }
            }
        }

        private void OnKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (!IsCapturing)
            {
                return;
            }

            switch (e.State)
            {
                case KeyState.Down:
                    //server.AddPacket(new KeyboardPacket((int)e.Key, true));
                    break;
                case KeyState.Up:
                   //server.AddPacket(new KeyboardPacket((int)e.Key, false));
                    break;
            }
        }

        public void StartCapturing()
        {
            lock (this)
            {
                capturing = true;
            }
        }

        public void StopCapturing()
        {
            lock (this)
            {
                capturing = false;
            }
        }
    }
}