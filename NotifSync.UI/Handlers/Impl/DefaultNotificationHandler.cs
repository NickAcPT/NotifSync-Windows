using System.Threading;
using System.Windows;
using NotifSync.Backend;
using NotifSync.Backend.Model;
using NotifSync.UI.Controls;
using NotifSync.UI.Controls.Impl;
using NotifSync.UI.Router;
using NotifSync.UI.Windows;

namespace NotifSync.UI.Handlers.Impl
{
    public class DefaultNotificationHandler : INotificationHandler
    {
        public BaseNotificationContentControl HandleNotification(RemoteNotification notification, ref NotificationWindow window)
        {
            return new BasicSimpleNotificationControl();
        }
    }
}