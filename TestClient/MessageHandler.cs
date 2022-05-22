using Common;
using DotNetty.Transport.Channels;

namespace TestClient
{
    class MessageHandler : SimpleChannelInboundHandler<Packet>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, Packet msg)
        {
            PacketReader reader = new(msg);
            string message = reader.ReadString();
            Console.WriteLine(message);
        }
    }
}
