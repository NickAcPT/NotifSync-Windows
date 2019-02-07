using NotifSync.Backend.Model;
using NotifSync.UI.Controls;
using NotifSync.UI.Windows;

namespace NotifSync.UI.Router
{
    public interface INotificationHandler
    {
        BaseNotificationContentControl HandleNotification(RemoteNotification notification, ref NotificationWindow window);
    }
}