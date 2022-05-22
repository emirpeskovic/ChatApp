using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System.Diagnostics;

namespace Common
{
    public class PacketEncoder : MessageToMessageEncoder<Packet>
    {
        protected override void Encode(IChannelHandlerContext context, Packet message, List<object> output)
        {
            byte[] messageBuffer = message.Buffer;
            int finalLength = messageBuffer.Length == 4096 ? message.Position + 4 : messageBuffer.Length + 4;
            byte[] finalBuffer = new byte[finalLength];

            byte[] lengthInBytes = BitConverter.GetBytes(finalLength);
            Array.Copy(lengthInBytes, 0, finalBuffer, 0, 4);
            Array.Copy(messageBuffer, 0, finalBuffer, 4, finalLength - 4);

            IByteBuffer byteBuffer = Unpooled.WrappedBuffer(finalBuffer);
            output.Add(byteBuffer);
        }
    }
}
