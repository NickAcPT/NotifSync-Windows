using System;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using NotifSync.Backend.Utils;

namespace NotifSync.Backend.Model
{
    public class RemoteAction : IDisposable
    {
        public string Title { get; set; }
        public Image Icon { get; set; }
        public RemoteInput[] Inputs { get; set; }
        [JsonIgnore]
        public RemoteNotification Parent { get; set; }
        [JsonIgnore]
        public int Index { get; set; }

        public bool HasRemoteInputs => Inputs?.Any() ?? false;

        public void Invoke()
        {
            Parent?.GetManager()?.InvokeAction(Parent.Id ?? 0, Parent.AppPackage, Index);
        }
        public void Reply(string content)
        {
            Parent?.GetManager()?.InvokeReply(Parent.Id ?? 0, Parent.AppPackage, Index, content);
        }

        public void Dispose()
        {
            Icon?.Dispose();
        }
    }
}