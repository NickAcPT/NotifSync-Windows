using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NotifSync.Backend;
using NotifSync.Backend.Model;
using NotifSync.UI.Windows;

namespace NotifSync.UI.Router
{
    public class NotificationRouter : INotificationRouter
    {
        public List<(INotificationHandler Handler, List<string> Styles)> Handlers { get; set; } =
            new List<(INotificationHandler Handler, List<string> Styles)>();

        public NotificationRouter()
        {
            var type = typeof(INotificationHandler);
            Handlers.AddRange(AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p))
                .Where(p => !p.IsInterface)
                .Select(t => ((INotificationHandler) Activator.CreateInstance(t), GetStylesForType(t))));
        }

        private List<string> GetStylesForType(Type type)
        {
            var list = new List<string>();

            list.AddRange(type.GetCustomAttributes(typeof(TargetedStyleAttribute), true)
                ?.OfType<TargetedStyleAttribute>()?.Select(c => c.Style));

            return list;
        }

        public Dictionary<(int, string), NotificationWindow> NotificationWindows { get; set; } = new Dictionary<(int, string), NotificationWindow>();

        public void Route(RemoteNotification notif)
        {
            object stl = "";
            notif.Extras.TryGetValue("android.template", out stl);
            INotificationHandler handler = null;

            var style = stl as string ?? "";

            if (style != "")
            {
                //Try to find the first handler for our current style
                handler = Handlers.FirstOrDefault(c => c.Styles.Contains(style)).Handler;
            }

            if (handler == null)
                handler = Handlers.FirstOrDefault(c => !c.Styles.Any()).Handler;


            Application.Current.Dispatcher.Invoke(() =>
            {
                var keyTuple = (notif.Id ?? 0, notif.AppPackage);
                var alreadyExists = NotificationWindows.ContainsKey(keyTuple);
                var win = alreadyExists ? NotificationWindows[keyTuple] : new NotificationWindow
                {
                    WindowStartupLocation = WindowStartupLocation.Manual
                };
                win.Notification = notif;
                notif.ReplyHandler = win;

                if (!alreadyExists)
                {
                    NotificationWindows.Add(keyTuple, win);
                }

                var control = handler?.HandleNotification(notif, ref win);
                if (control == null) return;

                if (alreadyExists) control.Notification?.Dispose();
                control.Notification = notif;
                win.SetNotificationContent(control, alreadyExists);
                if (!alreadyExists)
                {
                    win.Closing += (sender, args) => NotificationWindows.Remove(keyTuple);
                }
                else
                {
                    win.AdjustWindowToContentControl(control);
                }

                win.Show();
            });
        }

        public void Remove(int id, string appPackage)
        {
            var valueTuple = (id, appPackage);
            if (!NotificationWindows.ContainsKey(valueTuple))
                return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var notificationWindow = NotificationWindows[valueTuple];
                notificationWindow.Close();

                NotificationWindows.Remove(valueTuple);
            });
        }
    }
}