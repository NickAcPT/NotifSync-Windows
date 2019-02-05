using System;
using System.Collections.Generic;
using System.Linq;
using NotifSync.Backend.Model;

namespace NotifSync.UI.Handlers
{
    public class NotificationRouter
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
            
            handler?.HandleNotification(notif);
        }
    }
}