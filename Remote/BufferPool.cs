using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GRemote
{
    /// <summary>
    /// A collection of buffers that multiple threads read and write to. Threads add
    /// buffers or wait for buffers to be added. Every operation on this object is
    /// thread safe.
    /// </summary>
    public class BufferPool
    {
        /// <summary>
        /// A list of buffers that are waiting to be consumed. This member is never
        /// accessed without a lock.
        /// </summary>
        private List<byte[]> buffers = new List<byte[]>();

        /// <summary>
        /// A running total of the bytes waiting to be consumed
        /// </summary>
        private int totalSize = 0;

        /// <summary>
        /// Construct an empty buffer pool
        /// </summary>
        public BufferPool()
        {

        }

        /// <summary>
        /// Add a single buffer to the pool. This will trigger threads
        /// waiting to consume buffers.
        /// </summary>
        /// <param name="buffer"></param>
        public void Add(byte[] buffer)
        {
            lock (buffers)
            {
                totalSize += buffer.Length;
                buffers.Add(buffer);
                Monitor.PulseAll(buffers);
            }
        }

        /// <summary>
        /// Adds a list of buffers to the pool. This will trigger threads
        /// waiting to consume buffers after all have been added.
        /// </summary>
        /// <param name="list"></param>
        public void Add(params byte[][] list)
        {
            lock (buffers)
            {
                foreach (byte[] buffer in list)
                {
                    totalSize += buffer.Length;
                    buffers.Add(buffer);
                }

                Monitor.PulseAll(buffers);
            }
        }

        /// <summary>
        /// Tries to remove the next buffer from the pool. If there are no buffers
        /// this will return null. This operation does not block.
        /// </summary>
        /// <returns></returns>
        public byte[] Remove()
        {
            byte[] b;

            lock (buffers)
            {
                if (buffers.Count <= 0)
                {
                    return null;
                }

                b = buffers[0];
                totalSize -= b.Length;
                buffers.RemoveAt(0);
                return b;
            }
        }

        /// <summary>
        /// Waits for the buffer pool to no longer be empty. If there are
        /// already buffers in the pool this skips any waiting.
        /// </summary>
        public void Wait()
        {
            lock (buffers)
            {
                if (buffers.Count > 0)
                {
                    return;
                }

                Monitor.Wait(buffers);
            }
        }

        /// <summary>
        /// Counts how many buffers are in the pool.
        /// </summary>
        public int Count
        {
            get
            {
                lock (buffers)
                {
                    return buffers.Count;
                }
            }
        }

        /// <summary>
        /// Gets the number of bytes waiting in the pool.
        /// </summary>
        public int TotalBytes
        {
            get
            {
                lock (buffers)
                {
                    return TotalBytes;
                }
            }
        }

        /// <summary>
        /// Triggers any waiting threads without adding or removing any buffers. This
        /// can be used to help threads finish up work before exiting.
        /// </summary>
        public void Pulse()
        {
            lock (buffers)
            {
                Monitor.PulseAll(buffers);
            }
        }

        /// <summary>
        /// Remove all buffers and trigger any waiting threads. This can be useful to
        /// help threads finish up work before exiting.
        /// </summary>
        public void Clear()
        {
            lock (buffers)
            {
                buffers.Clear();
                Monitor.PulseAll(buffers);
            }
        }
    }
}
