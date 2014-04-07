using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GRemote
{
    public abstract class Packet
    {
        protected byte[] data;

        public Packet(PacketType type, int size)
        {
            this.data = new byte[size];
            this.data[0] = (byte)type;
        }

        public Packet(byte[] data)
        {
            this.data = data;
        }

        /// <summary>
        /// Get a number that describes what kind of packet this is
        /// </summary>
        public PacketType Type
        {
            get
            {
                return (PacketType)data[0];
            }
        }

        /// <summary>
        /// Get the packet data as a byte array
        /// </summary>
        public byte[] Buffer
        {
            get
            {
                return data;
            }
        }

        /// <summary>
        /// Writes a 16-bit short integer from a byte buffer.
        /// </summary>
        /// <param name="value">Value to write</param>
        /// <param name="buffer">Byte buffer to write to</param>
        /// <param name="offset">Where in the buffer to write</param>
        public static void WriteInt16(short value, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)value;
            buffer[offset + 1] = (byte)(value >> 8);
        }

        /// <summary>
        /// Reads a 16-bit short integer from a byte buffer.
        /// </summary>
        /// <param name="buffer">Buffer to read from</param>
        /// <param name="offset">Offset to read at</param>
        /// <returns>16-bit short integer read</returns>
        public static short ReadInt16(byte[] buffer, int offset)
        {
            return (short)((buffer[offset + 0]) | (buffer[offset + 1] << 8));
        }

        /// <summary>
        /// Writes a 32-bit integer to a byte buffer
        /// </summary>
        /// <param name="value"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        public static void WriteInt32(int value, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)value;
            buffer[offset + 1] = (byte)(value >> 8);
            buffer[offset + 2] = (byte)(value >> 0x10);
            buffer[offset + 3] = (byte)(value >> 0x18);
        }

        /// <summary>
        /// Reads a 32-bit integer from a byte buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int ReadInt32(byte[] buffer, int offset)
        {
            return (buffer[offset + 0]) | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 0x10) | (buffer[offset + 3] << 0x18);
        }
    }

    /// <summary>
    /// Each packet has an 8-bit number used to identify it in
    /// the network protocol.
    /// </summary>
    public enum PacketType : byte
    {
        CONNECT_REQUEST = 0x01,
        CONNECT_RESPONSE = 0x02,

        VIDEO_REQUEST = 0x10,
        VIDEO_START = 0x11,
        VIDEO_UPDATE = 0x12,
        VIDEO_END = 0x13,

        KEYBOARD = 0x20
    }

    public class ConnectRequestPacket : Packet
    {
        public ConnectRequestPacket()
            : base(PacketType.CONNECT_REQUEST, 1)
        {

        }
    }

    public class ConnectResponsePacket : Packet
    {
        public ConnectResponsePacket()
            : base(PacketType.CONNECT_RESPONSE, 1)
        {

        }
    }

    public class VideoStartPacket : Packet
    {
        public VideoStartPacket(int width, int height)
            : base(PacketType.VIDEO_START, 9)
        {
            WriteInt32(width,  this.Buffer, 1);
            WriteInt32(height, this.Buffer, 5);
        }

        public VideoStartPacket(byte[] buffer)
            : base(buffer)
        {

        }

        public int VideoWidth
        {
            get
            {
                return ReadInt32(this.Buffer, 1);
            }
        }

        public int VideoHeight
        {
            get
            {
                return ReadInt32(this.Buffer, 5);
            }
        }
    }

    public class VideoUpdatePacket : Packet
    {
        public VideoUpdatePacket(int size)
            : base(PacketType.VIDEO_UPDATE, 5)
        {
            WriteInt32(size, data, 1);
        }

        public int VideoDataLength
        {
            get
            {
                return ReadInt32(data, 1);
            }
        }
    }

    public class KeyboardPacket : Packet
    {
        public KeyboardPacket(int scancode, bool set)
            : base(PacketType.KEYBOARD, 6)
        {
            WriteInt32(scancode, Buffer, 1);
            data[5] = set ? (byte)0x01 : (byte)0x00;
        }

        public KeyboardPacket(byte[] buffer)
            : base(buffer)
        {

        }

        public int Scancode
        {
            get
            {
                return ReadInt32(Buffer, 1);
            }
        }

        public bool KeyDown
        {
            get
            {
                return data[5] != 0;
            }
        }
    }
}
