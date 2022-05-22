using System.Text;

namespace Common
{
    public class PacketWriter
    {
        public Packet Packet { get; private set; }

        public PacketWriter(int opCode)
        {
            Packet = new(4096);
            WriteOpCode(opCode);
        }

        public PacketWriter(Packet packet)
        {
            Packet = packet;
        }

        public void Write(byte value)
        {
            Packet.Buffer[Packet.Position] = value;
            Packet.Position++;
        }

        public void Write(byte[] value)
        {
            Array.Copy(value, 0, Packet.Buffer, Packet.Position, value.Length);
            Packet.Position += value.Length;
        }

        public void Write(short value)
        {
            Write(BitConverter.GetBytes(value));
        }

        public void Write(int value)
        {
            Write(BitConverter.GetBytes(value));
        }

        public void Write(long value)
        {
            Write(BitConverter.GetBytes(value));
        }

        public void Write(string value)
        {
            short length = (short)value.Length;
            Write(length);
            Write(Encoding.UTF8.GetBytes(value));
        }

        private void WriteOpCode(int opCode)
        {
            Write(opCode);
            Packet.OpCode = opCode;
        }
    }
}
