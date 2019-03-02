using System.Collections.Generic;
using Config.Net;
using NotifSync.Backend.Model;

namespace NotifSync.Backend
{
    public interface IAppSettings
    {
        [Option(DefaultValue = "Unnamed Device")]
        string DeviceName { get; set; }

        [Option]
        List<RemoteDevice> Devices { get; set; }
    }
}