using System;
using System.Drawing;

namespace NotifSync.Backend.Model
{
    public class RemoteAction : IDisposable
    {
        public string Title { get; set; }
        public Image Icon { get; set; }
        public RemoteInput[] Inputs { get; set; }

        public void Dispose()
        {
            Icon?.Dispose();
        }
    }
}