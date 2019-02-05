using System.Collections.Generic;

namespace NotifSync.Backend.Model
{
    public class RemoteMessageInfo
    {
        public List<RemoteMessage> Messages { get; set; }
        public bool GroupConversation { get; set; }
        public string ConversationTitle { get; set; }
        public RemotePerson User { get; set; }
    }
}