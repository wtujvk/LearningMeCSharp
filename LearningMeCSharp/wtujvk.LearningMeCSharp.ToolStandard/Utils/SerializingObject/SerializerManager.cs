
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.ToolStandard.Utils.Adapter;
using wtujvk.LearningMeCSharp.ToolStandard.Utils.SerializingObject;

namespace Demo.LindAgile.Standard.SerializingObject
{
    /// <summary>
    /// JS序列化管理者
    /// </summary>
    public class SerializerManager : IObjectSerializer
    {

        #region Constructs & Fields
        private SerializerManager()
        {
            //if (Modules.ModuleManager.IsRegistered(typeof(IObjectSerializer)))
            //    _iObjectSerializer = Modules.ModuleManager.Resolve<IObjectSerializer>();
            //else
            //    _iObjectSerializer = new NewtonsoftSerializer();
            _iObjectSerializer = new NewtonsoftSerializer();
        }
        private IObjectSerializer _iObjectSerializer;
        private static SerializerManager _instance;
        private static object lockObj = new object();

        /// <summary>
        /// 序列化单例对象
        /// </summary>
        public static SerializerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new SerializerManager();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T DeserializeObj<T>(string value) where T : class, new()
        {
            return _iObjectSerializer.DeserializeObj<T>(value);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public object DeserializeObj(string value, Type t)
        {
            return _iObjectSerializer.DeserializeObj(value, t);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public object DeserializeObj(string value)
        {
            return _iObjectSerializer.DeserializeObj(value);
        }
        /// <summary>
        /// 序列化成json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SerializeObj(object obj)
        {
            return _iObjectSerializer.SerializeObj(obj);
        }
    }
}
