using ChatClient.Network;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private ChatViewModel chatViewModel;
        private TcpClient? client;
        
        public ChatWindow()
        {
            client = new();
            _ = client.TryConnect("127.0.0.1", 4949);
            client.Subscribe(OnMessageReceived);
            InitializeComponent();
            chatViewModel = new();
            DataContext = chatViewModel;
        }

        private void OnMessageReceived(PacketReader reader)
        {
            string username = reader.ReadString();
            string message = reader.ReadString();
            string combined = $"{username}: {message}";
            Dispatcher.Invoke(() =>
            {
                chatUserControl.AddMessage(new Uri(@"pack://application:,,,/Resources/black.png"), combined);
            });
        }
    }
}
