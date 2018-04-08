
using System;
using System.Collections.Generic;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard
{
    public class CacheManager : ThreadSafeLazyBaseSingleton<CacheManager>, ICache
    {
        ICache cache;
         public CacheManager()
        {
            if (ModuleManager.IsRegistered<ICache>())
            {
                cache = ModuleManager.Resolve<ICache>();
            }
            else
            {
                throw new ArgumentNullException("ICache未被实例化");
            }
        }
        public void Delete(string key)
        {
            cache.Delete(key);
        }

        public object Get(string key)
        {
           return cache.Get(key);
        }

        public void Put(string key, object obj)
        {
            cache.Put(key, obj);
        }

        public void Put(string key, object obj, int expireMinutes)
        {
            cache.Put(key, obj, expireMinutes);
        }
    }
}
