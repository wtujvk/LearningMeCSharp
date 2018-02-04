//using LindAgile.SerializingObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils.Adapter
{
    /// <summary>
    /// Json.net实现序列化
    /// </summary>
    internal class NewtonsoftSerializer : SerializingObject.IObjectSerializer
    {
        static object lockObj = new object();
        public object DeserializeObj(string value, Type t)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            return JsonConvert.DeserializeObject(value, t);
        }

        public T DeserializeObj<T>(string value) where T : class, new()
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string SerializeObj(object obj)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            return JsonConvert.SerializeObject(obj);
        }

    }
}
