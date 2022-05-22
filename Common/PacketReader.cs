using System.Text;

namespace Common
{
    public class PacketReader
    {
        public Packet Packet { get; private set; }

        public PacketReader(byte[] buffer)
        {
            Packet = new(buffer);
            Packet.OpCode = ReadInt();
        }

        public PacketReader(Packet packet)
        {
            Packet = packet;
        }

        public byte ReadByte()
        {
            byte value = Packet.Buffer[Packet.Position];
            Packet.Position++;
            return value;
        }

        public short ReadShort()
        {
            short value = BitConverter.ToInt16(Packet.Buffer, Packet.Position);
            Packet.Position += 2;
            return value;
        }

        public void ReadOpCode()
        {
            Packet.OpCode = ReadInt();
        }

        public int GetOpCode()
        {
            return Packet.OpCode;
        }

        public int ReadInt()
        {
            int value = BitConverter.ToInt32(Packet.Buffer, Packet.Position);
            Packet.Position += 4;
            return value;
        }

        public long ReadLong()
        {
            long value = BitConverter.ToInt64(Packet.Buffer, Packet.Position);
            Packet.Position += 8;
            return value;
        }

        public string ReadString()
        {
            short length = ReadShort();
            string value = Encoding.UTF8.GetString(Packet.Buffer, Packet.Position, length);
            Packet.Position += length;
            return value;
        }

        public string ReadableBuffer()
        {
            string value = "";
            foreach (byte b in Packet.Buffer)
            {
                value += b.ToString("X2") + " ";
            }
            return value;
        }
    }
}
