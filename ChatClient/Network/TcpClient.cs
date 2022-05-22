using Common;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient.Network
{
    public class TcpClient
    {
        private TcpServerChannelHandler? handler;
        private IChannel? channel;

        public TcpClient()
        {
            channel = null;
            handler = null;
        }

        public void Subscribe(Action<PacketReader> onReceive)
        {
            handler?.Subscribe(onReceive);
        }

        public bool IsAlive()
        {
            return channel != null && channel.Active && channel.Open;
        }

        public async Task TryConnect(string ip, int port)
        {
            IEventLoopGroup group = new MultithreadEventLoopGroup();
            handler = new();
            try
            {
                Bootstrap bootstrap = new Bootstrap()
                    .Group(group)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(handler: new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new PacketDecoder());
                        pipeline.AddLast(new PacketEncoder());
                        pipeline.AddLast(handler);
                    }));

                channel = await bootstrap.ConnectAsync(IPAddress.Parse(ip), port);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }
    }
}
