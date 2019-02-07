using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using NotifSync.Backend.Model;
using NotifSync.UI.Annotations;

namespace NotifSync.UI.Controls
{
    public class BaseNotificationContentControl : ContentControl, INotifyPropertyChanged
    {
        private RemoteNotification _notification;

        public BaseNotificationContentControl()
        {
            
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}