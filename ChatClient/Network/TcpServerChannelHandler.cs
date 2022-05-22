using Common;
using DotNetty.Transport.Channels;
using System;
using System.Windows;

namespace ChatClient.Network
{
    class TcpServerChannelHandler : SimpleChannelInboundHandler<Packet>
    {
        private event Action<PacketReader>? OnReceive;

        public void Subscribe(Action<PacketReader> onReceive)
        {
            OnReceive += onReceive;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, Packet msg)
        {
            if (msg is not null)
            {
                PacketReader reader = new(msg);
                reader.ReadOpCode();
                
                if (reader.GetOpCode() == 0x3)
                {
                    OnReceive?.Invoke(new PacketReader(msg));
                }
            }
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            MessageBox.Show(exception.Message);
            base.ExceptionCaught(context, exception);
        }
    }
}
