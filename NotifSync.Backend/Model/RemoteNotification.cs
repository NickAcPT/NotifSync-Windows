using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NotifSync.Backend.Utils;

namespace NotifSync.Backend.Model
{
    public class RemoteNotification : IDisposable
    {
        public int? Id { get; set; }
        public string AppName { get; set; }
        public string AppPackage { get; set; }
        public Color Color { get; set; }
        public long PostTime { get; set; }
        public long When { get; set; }
        public RemoteAction[] Actions { get; set; }
        public Image SmallIcon { get; set; }
        public Image LargeIcon { get; set; }
        public Dictionary<string, object> Extras { get; set; }
        public RemoteMessageInfo MessageInfo { get; set; }
        public bool Ongoing { get; set; }
        public bool Clearable { get; set; }

        /* Helper Values */
        public string Title => Extras?.GetCastedValueOrDefault("android.title", "") ?? "";
        public string Text => Extras?.GetCastedValueOrDefault("android.text", "") ?? "";
        public string SubText => Extras?.GetCastedValueOrDefault("android.subText", "") ?? "";
        public bool HasSubText => Extras?.ContainsKey("android.subText") ?? false;

        public bool HasActions => Actions?.Length > 0;
        /* Helper Values */

        public void Dispose()
        {
            SmallIcon?.Dispose();
            LargeIcon?.Dispose();
            MessageInfo?.Dispose();
            Actions?.ToList().ForEach(c => c.Dispose());
        }
    }
}