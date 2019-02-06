using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using NotifSync.Backend.Model;
using NotifSync.UI.Annotations;

namespace NotifSync.UI.Windows
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window, INotifyPropertyChanged
    {
        private RemoteNotification _notification;

        public NotificationWindow(RemoteNotification notification)
        {
            Notification = notification;
        }

        public RemoteNotification Notification
        {
            get => _notification;
            set
            {
                if (Equals(value, _notification)) return;
                _notification = value;
                OnPropertyChanged();
            }
        }

        public NotificationWindow()
        {
            InitializeComponent();
        }

        private void MoveRect_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}