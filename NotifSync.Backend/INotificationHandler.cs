using NotifSync.Backend.Model;

namespace NotifSync.UI.Handlers
{
    public interface INotificationHandler
    {
        void HandleNotification(RemoteNotification notification);
    }
}