using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRemote
{
    public class InputPlayback
    {
        public InputPlayback()
        {

        }

        public void PressKey(int key)
        {
            PressKey((Interceptor.Keys)key);
        }

        public void PressKey(Interceptor.Keys key)
        {

        }

        public void ReleaseKey(int key)
        {
            ReleaseKey((Interceptor.Keys)key);
        }

        public void ReleaseKey(Interceptor.Keys key)
        {

        }

        public void StartPlayback()
        {

        }

        public void StopPlayback()
        {

        }
    }
}
