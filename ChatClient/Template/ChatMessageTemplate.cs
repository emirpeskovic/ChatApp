using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChatClient.Template
{
    public class ChatMessageTemplate : INotifyPropertyChanged
    {
        private Image avatar;
        public Image Avatar
        {
            get { return avatar; }
            set
            {
                avatar = value;
                NotifyPropertyChanged(nameof(Avatar));
            }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                NotifyPropertyChanged(nameof(Message));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ChatMessageTemplate(Image avatar, string message)
        {
            this.avatar = avatar;
            this.message = message;
        }
    }
}
