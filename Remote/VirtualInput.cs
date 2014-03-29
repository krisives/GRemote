using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Interceptor;

namespace GRemote
{
    class VirtualInput
    {
        IntPtr targetWindowPtr;
        Input input;

        public VirtualInput(IntPtr targetWindowPtr)
        {
            this.input = new Input();
            this.targetWindowPtr = targetWindowPtr;
        }

        public void TriggerKey()
        {

        }
    }
}