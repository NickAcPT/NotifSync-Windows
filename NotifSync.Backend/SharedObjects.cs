namespace NotifSync.Backend
{
    public class SharedObjects
    {
        private static SharedObjects _instance;
        public static SharedObjects Instance => _instance ?? (_instance = new SharedObjects());

        public INotificationRouter NotificationRouter { get; set; }
    }
}