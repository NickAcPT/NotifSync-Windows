using System.Collections.Generic;
using Config.Net;
using NotifSync.Backend.Server;
using NotifSync.Backend.Utils;
using Refit;

namespace NotifSync.Backend
{
    public class SharedObjects
    {
        private static IAppSettings _settings;
        private static SharedObjects _instance;
        public static SharedObjects Instance => _instance ?? (_instance = new SharedObjects());

        public INotificationRouter NotificationRouter { get; set; }

        private Dictionary<string, INotificationManager> NotificationManagers { get; } =
            new Dictionary<string, INotificationManager>();

        public static IAppSettings Settings => _settings ??
                                               (_settings = new ConfigurationBuilder<IAppSettings>()
                                                   .UseTypeParser(new JsonConfigItemParser()).UseJsonFile("config.json")
                                                   .Build());

        public INotificationManager GetManagerForIp(string ip)
        {
            if (!NotificationManagers.ContainsKey(ip))
                NotificationManagers[ip] = RestService.For<INotificationManager>($"http://{ip}:11786/");
            return NotificationManagers[ip];
        }
    }
}