using NotifSync.UI.Handlers;

namespace NotifSync.UI
{
    public class SharedObjects
    {
        private static SharedObjects _instance;
        public static SharedObjects Instance => _instance ?? (_instance = new SharedObjects());

        public NotificationRouter NotificationRouter { get; set; } = new NotificationRouter();

        /// <summary>
        /// This is just here so I can just call this class
        /// </summary>
        public void RandomMethod()
        {

        }

    }
}