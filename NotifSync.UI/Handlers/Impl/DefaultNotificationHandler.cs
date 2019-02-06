using System.Threading;
using System.Windows;
using NotifSync.Backend.Model;
using NotifSync.UI.Windows;

namespace NotifSync.UI.Handlers.Impl
{
    public class DefaultNotificationHandler : INotificationHandler
    {
        public void HandleNotification(RemoteNotification notification)
        {
            Application.Current.Dispatcher.Invoke(() => StartWindow(notification));
        }

        private void StartWindow(RemoteNotification notification)
        {
            var window = new NotificationWindow
            {
                Notification = notification,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            void Window_Loaded(object sender, RoutedEventArgs e)
            {
                var desktopWorkingArea = SystemParameters.WorkArea;
                window.Left = desktopWorkingArea.Right - window.Width;
                window.Top = desktopWorkingArea.Bottom - window.Height;
            }

            window.Loaded += Window_Loaded;

            window.Show();
        }
    }
}