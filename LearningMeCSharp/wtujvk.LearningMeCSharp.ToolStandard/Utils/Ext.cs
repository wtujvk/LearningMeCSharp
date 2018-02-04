using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Ext
    {
        /// <summary>
        /// 输出字符串，将null值设置为默认字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="def">默认字符串</param>
        /// <returns></returns>
        public static string ToNotNullString(this object obj,string def = "")
        {
            try
            {
                if (obj != null) { def = obj.ToString(); }
            }
            catch (Exception)
            {
                throw;
            }
            return def;
        }
        
        /// <summary>
        /// 判断字符串是不是null或者空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        /// <summary>
        /// 判断字符串是不是有 null或者空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAnyNullOrWhiteSpace(this IEnumerable<string> strs)
        {
            return strs == null || strs.Any(c => c.IsNullOrWhiteSpace());
        }
        /// <summary>
        /// 判断字符串是否可见，是不是非空白字符串和null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsVisable(this string str)
        {
            return !str.IsNullOrWhiteSpace();
        }
        /// <summary>
        /// 判断多个字符串都可见
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static bool IsAllVisable(this IEnumerable<string> strs)
        {
            return strs.AnyElement() && strs.All(c => c.IsVisable());
        }
        /// <summary>
        /// 判断是否包含元素（包含对null值的判断）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static bool AnyElement<T>(this IEnumerable<T> lst)
        {
            return lst != null && lst.Any();
        }
        /// <summary>
        /// 根据字符串获取枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <param name="str"></param>
        /// <param name="en2"></param>
        /// <returns></returns>
        public static T GetEnumFromStr<T>(this Enum en,string str,T en2=default(T)) where T:struct
        {
            try
            {
               en2=  (T)Enum.Parse(en.GetType(), str);
            }
            catch (Exception)
            {
             
            }
            return en2;
        }
    }
}
