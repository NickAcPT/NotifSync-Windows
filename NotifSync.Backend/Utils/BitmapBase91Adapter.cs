using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using NotifSync.Utils;

namespace NotifSync.Backend.Utils
{
    public class BitmapBase91Adapter : JsonConverter
    {
        public BitmapBase91Adapter(Dictionary<string, Image> bitmaps)
        {
            Bitmaps = bitmaps;
        }

        public Dictionary<string, Image> Bitmaps { get; set; }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            /*
            if (value is Image img)
            {
                var bytes = Base91.Encode(img.ToByteArray());
                writer.WriteValue(Encoding.UTF8.GetString(bytes));
                return;
            }
            */

            writer.WriteNull();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                var id = reader.Value as string ?? throw new InvalidOperationException();

                Image bitmap = null;
                Bitmaps?.TryGetValue(id, out bitmap);
                return bitmap;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override bool CanConvert(Type objectType) => typeof(Image).IsAssignableFrom(objectType);
    }
}