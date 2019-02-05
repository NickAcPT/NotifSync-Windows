using System.Collections.Generic;

namespace NotifSync.Backend.Model
{
    public class RemoteInput
    {
        public string Label { get; set; }
        public string[] Choices { get; set; }
        public List<string> AllowedDataTypes { get; set; }
        public Dictionary<string, object> Extras { get; set; }
        public bool AllowsFreeFormInput { get; set; }
        public bool IsDataOnly { get; set; }
        public string ResultKey { get; set; }
    }
}