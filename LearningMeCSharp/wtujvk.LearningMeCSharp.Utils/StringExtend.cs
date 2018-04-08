
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;


namespace wtujvk.LearningMeCSharp.Utils
{
    /// <summary>
    /// 扩展方法，帮助类
    /// </summary>
    public static class StringExtend
    {
        static Random random = new Random();

        /// <summary>
        /// 字符串转int
        /// </summary>
        /// <param name="str"></param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public static int Toint(this string str, int def = 0)
        {
            int.TryParse(str, out def);
            return def;
        }
        /// <summary>
        /// 读取配置文件的内容
        /// </summary>
        /// <param name="key"></param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public static string GetConfigBykey(string key, string def = "")
        {
            var val = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(val))
            {
                val = def;
            }
            return val;
        }
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetRanString(Int16 len)
        {
            if (len <= 0) throw new ArgumentException();
            var strcons = "abcdefghihklmnopqrstuvwxyz123456789<>[]";
            Random ran = new Random();
            StringBuilder sb = new StringBuilder((int)len);
            for (int i = 0; i < len; i++)
            {
                sb.Append(strcons[ran.Next(strcons.Length)]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值，默认0</param>
        /// <returns></returns>
        public static int GetRanNum(int max, int min = 0)
        {
            return random.Next(min, max);
        }
        /// <summary>
        /// 获取随机数,正整数
        /// </summary>
        /// <param name="max">最大值</param>
        /// <param name="min">最小值，默认0</param>
        /// <returns></returns>
        public static ushort GetRanAge(ushort max, ushort min = 0)
        {
            var res = Math.Abs(random.Next(min, max));
            return Convert.ToUInt16(res);
        }
        /// <summary>
        /// 运行方法 捕获异常
        /// </summary>
        /// <param name="action"></param>
        /// <param name="exaction"></param>
        public static void DoMethod(Action action, Action<string> exaction)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
                string msg = $"{ex.Message},{ex.Source},{ex.TargetSite},{ex.StackTrace}";
                exaction(msg);
            }
        }

        #region MyRegion
        /// <summary>
        ///转换成json字符串 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJson(this object t)
        {
            try
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, DateFormatString = "yyyy-MM-dd HH:mm:ss" };
                return JsonConvert.SerializeObject(t, settings);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
            }
            return string.Empty;
        }
        /// <summary>
        /// json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJosn<T>(this string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(T);
            try
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, DateFormatString = "yyyy-MM-dd HH:mm:ss" };
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
            }
            return default(T);
        }
        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object FromJosn(this string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(object);
            try
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, DateFormatString = "yyyy-MM-dd HH:mm:ss" };
                return JsonConvert.DeserializeObject(json);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
            }
            return default(object);
        }
        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object FromJosn(this string json,Type type)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(object);
            try
            {
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, DateFormatString = "yyyy-MM-dd HH:mm:ss" };
                return JsonConvert.DeserializeObject(json,type,settings);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
            }
            return default(object);
        }
        #endregion
        /// <summary>
        /// 判断字符串是否在要比较的字符串中
        /// 空字符串直接返回false
        /// </summary>
        /// <param name="origin">源字符串</param>
        /// <param name="comparestring">比较的字符串</param>
        /// <param name="separators">分隔符</param>
        /// <example>
        /// <code>
        ///  string a; a.StrIsIn("1,3,4");
        ///  </code>
        /// </example>
        /// <returns></returns>
        public static bool StrIsIn(this string origin,string comparestring,params string[] separators)
        {
            //return Ext.IsIn(origin,comparestring,separators);
            if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(comparestring)) return false;
            if (separators == null) {
                separators = new string[] { "," };
            } else
            {
                separators = separators.Where(c=>c!=null).Distinct().ToArray();
            }
            var arry = comparestring.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return arry.Contains(origin);
        }
    }
}