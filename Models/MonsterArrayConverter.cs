using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonsterAdvancer.Models
{
    public class MonsterArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Dictionary<string, JObject>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JArray ja = JArray.Load(reader);
            var key = string.Empty;
            var dict = new Dictionary<string, JObject>();
            foreach (JObject item in ja)
            {
                key = item.Property("Name").Value.ToString();
                if (dict.ContainsKey(key))
                {
                    key = $"{key} ({item.Property("Source").Value})";
                    dict.Add(key, item);
                }
                else
                {
                    dict.Add(key, item);
                }
                
            }
            return dict;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
