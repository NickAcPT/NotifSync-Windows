using System;
using System.Collections.Generic;
using System.Drawing;

namespace NotifSync.Backend.Model
{
    public class RemoteNotification
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
    }
}