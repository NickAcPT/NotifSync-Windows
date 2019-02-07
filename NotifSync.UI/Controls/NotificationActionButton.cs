using System.Windows;
using System.Windows.Controls;
using NotifSync.Backend.Model;

namespace NotifSync.UI.Controls
{
    public class NotificationActionButton : Button
    {
        public NotificationActionButton(RemoteAction action)
        {
            Style = (Style) FindResource("NotificationButton");
            Content = action.Title;
        }

        public RemoteInput Action { get; set; }
    }
}