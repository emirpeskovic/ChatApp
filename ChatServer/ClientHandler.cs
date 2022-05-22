using Common;
using DotNetty.Transport.Channels;

namespace ChatServer
{
    class ClientHandler : SimpleChannelInboundHandler<Packet>
    {
        private readonly TcpServer server;

        public ClientHandler(TcpServer server)
        {
            this.server = server;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, Packet msg)
        {
            if (msg != null)
            {
                server.Broadcast(msg);
            }
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            Console.WriteLine("Client connected: " + context.Channel.RemoteAddress);
            server.AddClient(context.Channel);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Console.WriteLine("Client disconnected: " + context.Channel.RemoteAddress);
            server.RemoveClient(context.Channel);
        }
    }
}
