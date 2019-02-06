using System;
using System.Drawing;
using Newtonsoft.Json;

namespace NotifSync.Backend.Utils
{
    public class AndroidColorAdapter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (!(reader.Value is long color))
                return Color.Transparent;

            var colorInt = (int) color;

            var bytes = BitConverter.GetBytes(colorInt);
            return Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
        }
    }
}