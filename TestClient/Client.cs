using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Common;
using System.Net;
using DotNetty.Buffers;

namespace TestClient
{
    class Client
    {
        public static async Task Start()
        {
            IEventLoopGroup group = new MultithreadEventLoopGroup();

            try
            {
                Bootstrap bootstrap = new Bootstrap()
                .Group(group)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;

                    pipeline.AddLast(new PacketDecoder());
                    pipeline.AddLast(new PacketEncoder());
                    pipeline.AddLast(new MessageHandler());
                }));

                IChannel bootstrapChannel = bootstrap.ConnectAsync(IPAddress.Parse("127.0.0.1"), 4949).Result;

                while (true)
                {
                    string line = Console.ReadLine()!;
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    try
                    {
                        PacketWriter writer = new(0x3);
                        writer.Write("yeehaw");
                        writer.Write(line);
                        await bootstrapChannel.WriteAndFlushAsync(writer.Packet);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                group.ShutdownGracefullyAsync().Wait(1000);
            }
        }
    }
}
