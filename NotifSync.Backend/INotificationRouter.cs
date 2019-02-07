using NotifSync.Backend.Model;

namespace NotifSync.Backend
{
    public interface INotificationRouter
    {
        void Route(RemoteNotification notif);

        void Remove(int id, string appPackage);
    }
}