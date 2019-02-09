using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NotifSync.Backend;
using NotifSync.Backend.Model;
using NotifSync.UI.Controls;
using NotifSync.UI.Properties;

namespace NotifSync.UI.Windows
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window, INotifyPropertyChanged, IReplyHandler
    {
        private RemoteNotification _notification;

        public NotificationWindow(RemoteNotification notification)
        {
            Notification = notification;
        }

        public void SetNotificationContent(BaseNotificationContentControl control, bool alreadyLoaded = false)
        {
            ContentControl.Content = control;

            var buttons = Notification?.Actions?.Select(c => new NotificationActionButton(c)) ??
                          Enumerable.Empty<NotificationActionButton>();
            var actionButtons = buttons as NotificationActionButton[] ?? buttons.ToArray();
            ActionPanel.Children.Clear();
            foreach (var button in actionButtons)
            {
                ActionPanel.Children.Add(button);
            }

            if (!alreadyLoaded)
            {
                var onLoaded = PrepareWindow(control, actionButtons);
                Loaded -= onLoaded;
                Loaded += onLoaded;
            }
            else
            {
                AdjustWindowToContentControl(control);
            }
        }

        [Annotations.CanBeNull]
        public Task<string> GetReplyFromUser()
        {
            var replyMessageBox = new ReplyWindowMessageBox();
            replyMessageBox.Deactivated += (sender, args) =>
            {
                if (replyMessageBox.IsClosing)
                    return;
                //Canceled
                replyMessageBox.MessageContent = string.Empty;
                replyMessageBox.Close();
            };

            replyMessageBox.Loaded += (sender, args) => replyMessageBox.AdjustWindowToNotificationWindow(this);
            replyMessageBox.Owner = this;
            replyMessageBox.Show();
            return Task.Run(() => replyMessageBox.InputEnteredEvent.WaitOne())
                .ContinueWith(task => replyMessageBox.MessageContent);
        }

        public RoutedEventHandler PrepareWindow(BaseNotificationContentControl control,
            NotificationActionButton[] actionButtons)
        {
            return (sender, args) =>
            {
                foreach (var button in actionButtons)
                {
                    button.Width = button.ActualWidth + 10;
                }

                AdjustWindowToContentControl(control);

                var desktopWorkingArea = SystemParameters.WorkArea;
                Left = desktopWorkingArea.Right - Width /* + OuterBorder.Margin.Right*/;
                Top = desktopWorkingArea.Bottom - Height /* + OuterBorder.Margin.Bottom*/;
            };
        }

        public void AdjustWindowToContentControl([Annotations.NotNull] FrameworkElement control)
        {
            var size = new Size(control.Width, control.Height);
            var oldContentWindowSize = new Size(ContentGrid.ActualWidth + 0, ContentGrid.ActualHeight + ActionPanel.ActualHeight);

            var (width, height) = (Width:
                size.Width - oldContentWindowSize.Width,
                Height: size.Height - oldContentWindowSize.Height);

            Width += width;
            Height += height;
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
            Notification?.Close();
            Close();
        }
    }
}