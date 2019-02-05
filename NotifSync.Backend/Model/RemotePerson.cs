using System.Drawing;

namespace NotifSync.Backend.Model
{
    public class RemotePerson
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public Image Icon { get; set; }
        public bool Important { get; set; }
        public bool IsBot { get; set; }
    }
}