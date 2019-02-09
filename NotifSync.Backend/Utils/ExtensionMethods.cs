using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using NotifSync.Backend.Model;
using NotifSync.Backend.Server;

namespace NotifSync.Backend.Utils
{
    public static class ExtensionMethods
    {
        public static T[] Concat<T>(this T[] x, T[] y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));
            var oldLen = x.Length;
            Array.Resize<T>(ref x, x.Length + y.Length);
            Array.Copy(y, 0, x, oldLen, y.Length);
            return x;
        }

        public static TV GetValueOrDefault<TK, TV>(this Dictionary<TK, TV> dict, TK key, TV defaultVal = default(TV))
        {
            return dict.ContainsKey(key) ? dict[key] : defaultVal;
        }
        
        public static TTv GetCastedValueOrDefault<TK, TV, TTv>(this Dictionary<TK, TV> dict, TK key, TTv defaultVal)
        {
            return dict.ContainsKey(key) ? (TTv)(object)dict[key] : defaultVal;
        }
        

        public static byte[] ToByteArray(this Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public static INotificationManager GetManager(this RemoteNotification notif)
        {
            return SharedObjects.Instance.GetManagerForIp(notif.SenderAddress);
        }

    }
}