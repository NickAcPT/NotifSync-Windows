using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using NotifSync.Backend.Model;

namespace NotifSync.UI.Controls
{
    public class NotificationActionButton : Button
    {
        public RemoteAction NotificationAction { get; }

        public NotificationActionButton(RemoteAction action)
        {
            NotificationAction = action;
            Style = (Style) FindResource("NotificationButton");
            Content = action.Title;
        }

        protected override void OnClick()
        {
            base.OnClick();
            if (!NotificationAction.HasRemoteInputs)
            {
                NotificationAction.Invoke();
            }
            else
            {
                NotificationAction?.Parent?.ReplyHandler?.GetReplyFromUser().ContinueWith(task =>
                {
                    if (!task.IsCompleted || string.IsNullOrEmpty(task.Result))
                        return;

                    NotificationAction.Reply(task.Result);
                });
            }
        }

        public RemoteInput Action { get; set; }
    }
}