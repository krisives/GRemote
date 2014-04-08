using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GRemote
{
    /// <summary>
    /// A wrapper around the System.Threading.Thread class to make it easy
    /// to safely stop a thread from outside or inside the thread. By default
    /// all stoppable threads are background threads.
    /// </summary>
    public abstract class StoppableThread
    {
        /// <summary>
        /// Handle to the actual system thread
        /// </summary>
        protected Thread handle;

        /// <summary>
        /// True if the Start() method has been called (thread may not yet be running)
        /// </summary>
        protected bool started = false;

        /// <summary>
        /// True if the thread was started and Run() was invoked
        /// </summary>
        protected bool hasStarted = false;

        /// <summary>
        /// True if Stop() has been called
        /// </summary>
        protected bool stopped = false;

        /// <summary>
        /// True if the Run() method has finished
        /// </summary>
        protected bool hasFinished = false;

        public StoppableThread()
        {
            handle = new Thread(new ThreadStart(Run));
            handle.IsBackground = true;
        }

        /// <summary>
        /// Starts the thread. If the thread is already started this does nothing.
        /// </summary>
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

        /// <summary>
        /// Stops the thread. If the thread was already stopped this does nothing. The thread will exit
        /// shortly after.
        /// </summary>
        public virtual void Stop()
        {
            lock (this)
            {
                stopped = true;
            }
        }

        /// <summary>
        /// Checks if the thread has been started. This may return true even if the
        /// thread is currently stopped. This being true means the thread has at one
        /// time been started.
        /// </summary>
        public bool HasStarted
        {
            get
            {
                lock (this)
                {
                    return started;
                }
            }
        }

        /// <summary>
        /// Checks if the thread has been stopped. This may return true even if the
        /// thread is currently running, but has been requested to stop. This being true
        /// means the thread has at one time been stopped.
        /// </summary>
        public bool HasStopped
        {
            get
            {
                lock (this)
                {
                    return stopped;
                }
            }
        }

        /// <summary>
        /// Checks if the thread is finished running. This will only return true if the
        /// thread has been started, ran, and finished running.
        /// </summary>
        public bool HasFinished
        {
            get
            {
                lock (this)
                {
                    return hasFinished;
                }
            }
        }

        /// <summary>
        /// Runs the thread. This is the main method of the thread and
        /// should not be overriden. Instead override the ThreadStart(),
        /// ThreadRun(), and ThreadEnd() methods.
        /// </summary>
        public void Run()
        {
            // Use one lock
            lock (this)
            {
                hasStarted = true;

                if (stopped || !started)
                {
                    // Skip running the thread entirely
                    started = true;
                    stopped = true;
                    hasStarted = true;
                    hasFinished = true;
                    
                    return;
                }
            }

            try
            {
                OnThreadStart();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Stop();
            }

            while (!HasStopped)
            {
                try
                {
                    ThreadRun();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Stop();
                    break;
                }
            }

            try
            {
                OnThreadFinish();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            lock (this)
            {
                hasFinished = true;
            }
        }

        /// <summary>
        /// A method that is invoked within a loop as long as it's started and
        /// not stopped.
        /// </summary>
        protected abstract void ThreadRun();

        /// <summary>
        /// Optional method to override that will be invoked when the thread
        /// begins running.
        /// </summary>
        protected virtual void OnThreadStart()
        {

        }

        /// <summary>
        /// Optional method to override that will be invoked when thread is
        /// finishing / before exit.
        /// </summary>
        protected virtual void OnThreadFinish()
        {

        }
    }
}
