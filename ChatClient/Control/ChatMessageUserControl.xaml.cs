using ChatClient.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ChatMessageUserControl.xaml
    /// </summary>
    public partial class ChatMessageUserControl : UserControl
    {
        public readonly ChatMessageTemplate ChatMessageTemplate;
        
        public ChatMessageUserControl(ChatMessageTemplate template)
        {
            ChatMessageTemplate = template;
            this.DataContext = ChatMessageTemplate;
            InitializeComponent();
        }
    }
}
