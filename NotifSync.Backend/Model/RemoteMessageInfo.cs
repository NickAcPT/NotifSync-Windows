using System;
using System.Collections.Generic;
using System.Linq;

namespace NotifSync.Backend.Model
{
    public class RemoteMessageInfo : IDisposable
    {
        public List<RemoteMessage> Messages { get; set; }
        public bool GroupConversation { get; set; }
        public string ConversationTitle { get; set; }
        public RemotePerson User { get; set; }

        public void Dispose()
        {
            User?.Dispose();
            Messages?.ToList().ForEach(c => c.Dispose());
        }
    }
}