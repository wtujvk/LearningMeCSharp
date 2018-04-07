//using LindAgile.SerializingObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.ToolStandard.Utils.SerializingObject;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils.Adapter
{
    /// <summary>
    /// Json.net实现序列化
    /// </summary>
    internal class NewtonsoftSerializer : IObjectSerializer
    {
        static object lockObj = new object();
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public object DeserializeObj(string value, Type t)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            return JsonConvert.DeserializeObject(value, t,settings);
        }

        public T DeserializeObj<T>(string value) where T : class, new()
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            return JsonConvert.DeserializeObject<T>(value);
        }

        public object DeserializeObj(string value)
        {
            return JsonConvert.DeserializeObject(value, settings);
        }

        public string SerializeObj(object obj)
        {
            return JsonConvert.SerializeObject(obj,settings);
        }

    }
}
