using DotNetty.Buffers;

namespace Common
{
    public class Packet
    {
        public byte[] Buffer { get; set; }
        public int Position { get; set; }
        public int OpCode { get; set; }

        public Packet(int size)
        {
            Buffer = new byte[size];
            Position = 0;
            OpCode = -0xFF;
        }

        public Packet(byte[] buffer)
        {
            Buffer = buffer;
            Position = 0;
            OpCode = -0xFF;
        }
    }
}
