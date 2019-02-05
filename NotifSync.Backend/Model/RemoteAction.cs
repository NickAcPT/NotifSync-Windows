using System.Drawing;

namespace NotifSync.Backend.Model
{
    public class RemoteAction
    {
        public string Title { get; set; }
        public Image Icon { get; set; }
        public RemoteInput[] Inputs { get; set; }
    }
}