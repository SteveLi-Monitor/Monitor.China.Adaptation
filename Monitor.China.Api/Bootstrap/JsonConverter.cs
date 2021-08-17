using Monitor.Ioc;
using System;

namespace Monitor.China.Api.Bootstrap
{
    public class JsonConverter : IJsonConverter
    {
        public T Deserialize<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public object Deserialize(string json, Type type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json, type);
        }

        public string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
