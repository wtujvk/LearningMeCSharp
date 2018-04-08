using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.ToolStandard;

namespace wtujvk.LearningMeCSharp.RedisSession.Codes
{
    /// <summary>
    /// 应用程序辅助
    /// </summary>
    public class AppDataInit
    {
        /// <summary>
        /// session键值
        /// </summary>
        public const string SessionLoginKey = "TEST-SESSION-KEY";
    }
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extend
    {
        /// <summary>
        /// 转换成json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson2(this object obj)
        {
            if (obj == null) return string.Empty;
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, DateFormatString = "yyyy-MM-dd HH:mm:ss" };
           return JsonConvert.SerializeObject(obj,settings);
        }
        /// <summary>
        /// 从json字符串中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(T);
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
               LoggerFactory.Instance.Logger_Error(ex);
            }
            return default(T);
         }
        /// <summary>
        /// 从json字符串中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object FromJson(this string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(object);
            try
            {
                return JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
            }
            return default(object);
        }
    }
}
