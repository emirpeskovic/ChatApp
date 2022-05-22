using ChatClient.Template;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient.Control
{
    /// <summary>
    /// Interaction logic for ScrollableChatUserControl.xaml
    /// </summary>
    public partial class ScrollableChatUserControl : UserControl
    {
        public ScrollableChatUserControl()
        {
            InitializeComponent();
        }

        public void AddMessage(Uri avatarSrc, string message)
        {
            Image image = new();
            image.Source = new BitmapImage(avatarSrc);
            image.Width = 48;
            image.Height = 48;
            chatWrapPanel.Children.Add(new ChatMessageUserControl(new ChatMessageTemplate(image, message)));

            Border separator = new();
            separator.Height = 1;
            separator.Background = new SolidColorBrush(Colors.Black);
            separator.HorizontalAlignment = HorizontalAlignment.Stretch;
            separator.SetBinding(WidthProperty, new Binding()
            {
                Path = new PropertyPath("ActualWidth"),
                Source = chatWrapPanel
            });
            chatWrapPanel.Children.Add(separator);
        }
    }
}
