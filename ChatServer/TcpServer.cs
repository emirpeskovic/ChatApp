using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using Common;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Handlers.Logging;
using System.Net;

namespace ChatServer
{
    class TcpServer
    {
        public bool Running { get; private set; }

        private readonly List<IChannel> clients;

        public TcpServer()
        {
            Running = false;
            clients = new();
        }

        public void AddClient(IChannel channel)
        {
            lock(clients)
            {
                if (!clients.Any(ch => ch == channel))
                {
                    clients.Add(channel);
                }
            }
        }

        public void RemoveClient(IChannel channel)
        {
            lock (clients)
            {
                if (clients.Contains(channel))
                {
                    channel.CloseAsync();
                    clients.Remove(channel);
                }
            }
        }

        public void Broadcast(Packet packet)
        {
            lock(clients)
            {
                clients.ForEach(async client => await client.WriteAndFlushAsync(packet));
            }
        }

        public async void Run()
        {
            IEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
            IEventLoopGroup workerGroup = new MultithreadEventLoopGroup();

            IChannel channel;

            try
            {
                ServerBootstrap bootstrap = new ServerBootstrap()
                    .Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoKeepalive, true)
                    .Handler(new LoggingHandler(LogLevel.INFO))
                    .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;

                        pipeline.AddLast(new PacketDecoder());
                        pipeline.AddLast(new PacketEncoder());
                        pipeline.AddLast(new ClientHandler(this));
                    }));

                channel = bootstrap.BindAsync(IPAddress.Parse("127.0.0.1"), 4949).Result;

                Console.WriteLine("Server successfully running on port 4949.");

                Running = true;

                while (Running)
                {
                    Console.ReadLine();
                    Console.WriteLine("No input supported.");
                }

                await channel.CloseAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
