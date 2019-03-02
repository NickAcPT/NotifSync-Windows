using System;
using System.Collections.Generic;
using Config.Net;
using Newtonsoft.Json;
using NotifSync.Backend.Model;

namespace NotifSync.Backend.Utils
{
    public class JsonConfigItemParser : ITypeParser
    {
        public bool TryParse(string value, Type t, out object result)
        {
            result = JsonConvert.DeserializeObject(value, t);
            return true;
        }

        public string ToRawString(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public IEnumerable<Type> SupportedTypes => new[] {typeof(RemoteDevice), typeof(List<RemoteDevice>)};
    }
}