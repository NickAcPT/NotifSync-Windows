using System;
using System.Collections.Generic;

namespace NotifSync.Backend.Model
{
    public class RemoteMessage : IDisposable
    {
        public RemotePerson Person { get; set; }
        public string Text { get; set; }
        public long Timestamp { get; set; }
        public string DataUri { get; set; }
        public string DataMimeType { get; set; }
        public Dictionary<string, object> Extras { get; set; }

        public void Dispose()
        {
            Person?.Dispose();
        }
    }
}