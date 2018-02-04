using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;

namespace LindAgile.SerializingObject
{

    /// <summary>
    /// javascript实现序列化
    /// </summary>
    internal class JsSerializer : Demo.LindAgile.Standard.SerializingObject.IObjectSerializer
    {
        static object lockObj = new object();
        public object DeserializeObj(string value, Type t)
        {
            JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            return js.Deserialize(value, t);
        }


        public T DeserializeObj<T>(string value) where T : class, new()
        {
            JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            return js.Deserialize<T>(value); //这版在反序列化Tuple时会有问题，因为它没有空构造方法
        }



        public string SerializeObj(object obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };//解决大小的限制
            return js.Serialize(obj);
        }


    }
}
