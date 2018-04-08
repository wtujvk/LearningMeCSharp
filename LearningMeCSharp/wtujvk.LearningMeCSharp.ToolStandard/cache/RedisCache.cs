
using Demo.LindAgile.Standard.SerializingObject;
using System;
using System.Collections.Generic;
using System.Text;
using wtujvk.LearningMeCSharp.ToolStandard.Conf;

namespace wtujvk.LearningMeCSharp.ToolStandard
{
    /// <summary>
    /// 使用redis
    /// </summary>
    public class RedisCache : ICache
    {
        readonly static int _expireMinutes = ConfigManager.Config.Caching.ExpireMinutes;
        const string RedisCacheKey = "Redis_Cache:";
        #region ICache 成员
        string getKey(string key)
        {
            return RedisCacheKey + key;
        }
        public void Put(string key, object obj)
        {
            this.Put(key, obj, -1);
        }

        public void Put(string key, object obj, int expireMinutes)
        {
            RedisManager.Instance.GetDatabase().StringSet(getKey(key),SerializerManager.Instance.SerializeObj(obj));
            if (expireMinutes != -1)
                RedisManager.Instance.GetDatabase().KeyExpire(getKey(key), DateTime.Now.AddMinutes(expireMinutes));
        }
        /// <summary>
        /// 扩展的方法 不依赖于 Modules
        /// </summary>
        /// <param name="key"></param>
        /// <param name="str"></param>
        /// <param name="expireMinutes"></param>
        public void PutString(string key, string str, int expireMinutes)
        {
            RedisManager.Instance.GetDatabase().StringSet(getKey(key), str);
            if (expireMinutes != -1)
                RedisManager.Instance.GetDatabase().KeyExpire(getKey(key), DateTime.Now.AddMinutes(expireMinutes));
        }

        public object Get(string key)
        {

            if (RedisManager.Instance.GetDatabase().StringGet(getKey(key)).HasValue)
                return SerializerManager.Instance.DeserializeObj(RedisManager.Instance.GetDatabase().StringGet(getKey(key)).ToString());
               
            else
                return null;
        }

        public void Delete(string key)
        {
            RedisManager.Instance.GetDatabase().KeyDelete(getKey(key));
        }

        #endregion
    }
}
