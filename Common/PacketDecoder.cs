using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Common
{
    public class PacketDecoder : MessageToMessageDecoder<IByteBuffer>
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer message, List<object> output)
        {
            byte[] bytes = new byte[message.ReadableBytes];
            message.ReadBytes(bytes);

            byte[] packetBuffer = new byte[bytes.Length - 4];
            Array.Copy(bytes, 4, packetBuffer, 0, packetBuffer.Length);

            output.Add(new Packet(packetBuffer));
        }
    }
}
