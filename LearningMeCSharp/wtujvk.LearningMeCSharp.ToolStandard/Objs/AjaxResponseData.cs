using System;

namespace wtujvk.LearningMeCSharp.ToolStandard.Objs
{
    /// <summary>
    /// ajax数据封装
    /// </summary>
    public class AjaxResponseData
    {
        public AjaxResponseData() { }
        
        /// <summary>
        /// 消息是否成功
        /// </summary>
        public bool OK { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string  Msg { get; set; }

        public static AjaxResponseData GetAjaxResponseData(bool Ok=false,string Msg= "未处理的异常")
        {
            return new AjaxResponseData() { OK = Ok, Msg =Msg };
        }
        public static AjaxResponseData<T> GetAjaxResponseData<T>(bool Ok=false, string Msg = "未处理的异常",T res=default(T))
        {
            return new AjaxResponseData<T>() { OK = Ok, Msg = Msg,Res=res };
        }

        public static AjaxResponseData<T> GetAjaxResponseData<S,T>(AjaxResponseData<S>ajaxRes,Func<S,T> fun)
        {
            if (ajaxRes == null) { return GetAjaxResponseData<T>(); }
            return new AjaxResponseData<T>() { OK = ajaxRes.OK, Msg = ajaxRes.Msg, Res = fun(ajaxRes.Res) };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        public void SetOk(bool success = true, string msg = "OK")
        {
            this.OK = success;
            this.Msg = msg;

        }
    }
    /// <summary>
    /// ajax数据封装 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AjaxResponseData<T> : AjaxResponseData,IFinder<T>
    {
        public AjaxResponseData(){}
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
            this.OK = ok;
            this.Msg = msg;
            this.Res = t;
        }
    }
    public interface IFinder<out T> { }
}
