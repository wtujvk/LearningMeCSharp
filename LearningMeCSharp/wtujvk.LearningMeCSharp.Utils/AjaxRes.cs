using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.Utils
{
    [Serializable]
    public class AjaxRes
    {
        public AjaxRes() { }

        /// <summary>
        /// 消息是否成功
        /// </summary>
        public bool OK { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ok"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public static AjaxRes GetAjaxRes(bool Ok = false, string Msg = "未处理的异常")
        {
            return new AjaxRes() { OK = Ok, Msg = Msg };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Ok"></param>
        /// <param name="Msg"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        public static AjaxRes<T> GetAjaxRes<T>(bool Ok = false, string Msg = "未处理的异常", T res = default(T))
        {
            return new AjaxRes<T>() { OK = Ok, Msg = Msg, Res = res };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="ajaxRes"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static AjaxRes<T> GetAjaxRes<S, T>(AjaxRes<S> ajaxRes, Func<S, T> fun)
        {
            if (ajaxRes == null) { return GetAjaxRes<T>(); }
            return new AjaxRes<T>() { OK = ajaxRes.OK, Msg = ajaxRes.Msg, Res = fun(ajaxRes.Res) };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        public void SetOk(bool success = true, string msg = "OK")
        {
            OK = success;
            Msg = msg;
        }
    }
    /// <summary>
    /// ajax数据封装 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class AjaxRes<T> : AjaxRes
    {
        public AjaxRes() { }
        /// <summary>
        /// 数据实体
        /// </summary>
        public T Res { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ok"></param>
        /// <param name="msg"></param>
        /// <param name="t"></param>
        public void SetOk(bool ok = true, string msg = "OK", T t = default(T))
        {
            OK = ok;
            Msg = msg;
            Res = t;
        }
    }
}
