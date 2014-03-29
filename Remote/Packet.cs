using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public PacketType Type
        {
            get
            {
                return (PacketType)data[0];
            }
        }

        public byte[] Buffer
        {
            get
            {
                return data;
            }
        }



        public static void WriteInt16(short value, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)value;
            buffer[offset + 1] = (byte)(value >> 8);
        }

        public static short ReadInt16(byte[] buffer, int offset)
        {
            return (short)((buffer[offset + 0]) | (buffer[offset + 1] << 8));
        }

        public static void WriteInt32(int value, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte)value;
            buffer[offset + 1] = (byte)(value >> 8);
            buffer[offset + 2] = (byte)(value >> 0x10);
            buffer[offset + 3] = (byte)(value >> 0x18);
        }

        public static int ReadInt32(byte[] buffer, int offset)
        {
            return (buffer[offset + 0]) | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 0x10) | (buffer[offset + 3] << 0x18);
        }
    }

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

    public class VideoStartPacket : Packet
    {
        public VideoStartPacket()
            : base(PacketType.VIDEO_START, 1)
        {

        }
    }

    public class VideoUpdatePacket : Packet
    {
        public VideoUpdatePacket(int size)
            : base(PacketType.VIDEO_UPDATE, 5)
        {
            WriteInt32(size, data, 1);
        }
    }
}
