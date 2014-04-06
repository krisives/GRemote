using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GRemote
{
    public abstract class StoppableThread
    {
        protected Thread handle;
        protected bool started = false;
        protected bool stopped = false;

        public StoppableThread()
        {
            handle = new Thread(new ThreadStart(Run));
        }

        public virtual void Start()
        {
            lock (this)
            {
                if (started)
                {
                    return;
                }

                if (stopped)
                {
                    return;
                }

                started = true;
            }

            handle.Start();
        }

        public virtual void Stop()
        {
            lock (this)
            {
                stopped = true;
            }
        }

        public bool IsRunning
        {
            get
            {
                lock (this)
                {
                    return started && !stopped;
                }
            }
        }

        public void Run()
        {
            while (IsRunning)
            {
                try
                {
                    RunThread();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        protected abstract void RunThread();
    }
}
