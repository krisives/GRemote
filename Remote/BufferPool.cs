using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GRemote
{
    class BufferPool
    {
        List<byte[]> buffers = new List<byte[]>();
        int totalSize;

        public BufferPool()
        {

        }

        public void Add(byte[] buffer)
        {
            lock (buffers)
            {
                totalSize += buffer.Length;
                buffers.Add(buffer);
                Monitor.PulseAll(buffers);
            }
        }

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

            return null;
        }

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

        public void Pulse()
        {
            lock (buffers)
            {
                Monitor.PulseAll(buffers);
            }
        }
    }
}
