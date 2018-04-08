
using Demo.LindAgile.Standard.SerializingObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.ToolStandard.Utils.SerializingObject;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;
using wtujvk.LearningMeCSharp.ToolStandard;
using Microsoft.AspNetCore.Mvc;
using wtujvk.LearningMeCSharp.ToolStandard.Objs;
using wtujvk.LearningMeCSharp.Entities;
using Microsoft.AspNetCore.Http;

namespace wtujvk.LearningMeCSharp.CoreMvc.Codes
{
    /// <summary>
    /// Bearer对象
    /// </summary>
    public class BearerModel
    {
        /// <summary>
        /// 请求token
        /// </summary>
        public string Access_token { get; set; }
        /// <summary>
        /// 刷新的token，access_token有问题再比较这个，然后同时生成新的access_token
        /// </summary>
        public string Refresh_token { get; set; }
        /// <summary>
        /// access_token过期时间
        /// </summary>
        public DateTime Access_token_Expires { get; set; }
        /// <summary>
        /// refresh_token过期时间
        /// </summary>
        public DateTime Refresh_Token_Expires { get; set; }
        /// <summary>
        /// 被授权的用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 被收取的用户编号
        /// </summary>
        public string UserId { get; set; }
    }
    /// <summary>
    /// Bearer授权方式改进
    /// </summary>
    public class BearerImpl2
    {
        /// <summary>
        ///更新数据到数据库的
        /// </summary>
        public static Func<BearerEntity, bool> upFunc = c => true;
        public BearerImpl2()
        {
            if (upFunc == null)
            {
                throw new ArgumentNullException();
            }
        }
        #region public Methods
        /// <summary>
        /// 用户登陆成功后，生成token并返回
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="func">从数据库查找Bearer</param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static AjaxResponseData<BearerModel> GeneratorBearer(string userId, string userName, Func<BearerModel, bool> func, int minutes = 5, int refMinutes = 60 * 24)
        {
            AjaxResponseData<BearerModel> ajaxResponse = AjaxResponseData.GetAjaxResponseData<BearerModel>();
            try
            {
                //生成token
                var model = new BearerModel
                {
                    Access_token = Guid.NewGuid().ToString("N"),
                    Refresh_token = Guid.NewGuid().ToString("N"),
                    UserName = userName,
                    Access_token_Expires = DateTime.Now.AddMinutes(minutes),
                    Refresh_Token_Expires = DateTime.Now.AddMinutes(refMinutes),
                    UserId = userId,
                };
                if (func != null)
                {
                    if (func(model))
                    {
                        //存储token
                        CacheManager.Current.Put(model.Access_token,SerializerManager.Instance.SerializeObj(model), minutes);
                        //返回token
                        ajaxResponse.OK = true;
                        ajaxResponse.Res = model;
                        ajaxResponse.Msg = "OK";
                    }
                    else { ajaxResponse.Msg = "数据存储校验失败"; }
                }
                else { ajaxResponse.Msg = "func参数错误"; }
            }
            catch (Exception ex)
            {
                ajaxResponse.Msg = ex.Message;
                LoggerFactory.Instance.Logger_Error(ex);
            }
            return ajaxResponse;
        }
        /// <summary>
        /// 從緩存中刪除token
        /// </summary>
        /// <param name="access_token"></param>
        public static void DelToken(string access_token)
        {
            CacheManager.Current.Delete(access_token);
        }
        #endregion

        #region private & internal Methods
        /// <summary>
        /// 校验access_token的有效性
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="refresh_token"></param>
        ///<param name="func">验证</param>
        /// <returns></returns>
        public static AjaxResponseData<string> ValidateToken(string access_token, Func<BearerModel, bool> func, string refresh_token = null)
        {
            AjaxResponseData<string> ajaxResponse = AjaxResponseData.GetAjaxResponseData<string>();
            try
            {
                if (string.IsNullOrWhiteSpace(access_token) || CacheManager.Current.Get(access_token) == null)
                {
                    ajaxResponse.Msg = "参数不合法";
                }
                else
                {
                    var obj = SerializerManager.Instance.DeserializeObj<BearerModel>(CacheManager.Current.Get(access_token).ToString());
                    if (obj != null)
                    {
                        //校验access-token
                        if (obj.Access_token.Equals(access_token)
                            && obj.Access_token_Expires >= DateTime.Now)
                        {
                           ajaxResponse.Msg = "Access_token过期";
                        }
                        else
                        {
                            //检验refresh-token
                            if (!string.IsNullOrWhiteSpace(refresh_token)
                                && obj.Refresh_token.Equals(refresh_token)
                                && obj.Refresh_Token_Expires >= DateTime.Now)
                            {
                                //更新token
                                CacheManager.Current.Delete(obj.Access_token);
                                var res = GeneratorBearer(obj.UserId, obj.UserName, func);
                                if (res.OK)
                                {
                                    ajaxResponse.Res = obj.Access_token;
                                    ajaxResponse.OK = true;
                                    ajaxResponse.Msg = "OK";
                                }
                                else
                                {
                                    ajaxResponse.Msg = res.Msg;
                                }
                            }
                            else
                            {
                               ajaxResponse.Msg = "refresh_token不合法";
                            }
                        }
                    }
                    else
                    {
                        var res = GeneratorBearer(obj.UserId, obj.UserName, func);
                        if (res.OK)
                        {
                            ajaxResponse.Res = obj.Access_token;
                            ajaxResponse.OK = true;
                            ajaxResponse.Msg = "OK";
                        }
                        else
                        {
                            ajaxResponse.Msg = res.Msg;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ajaxResponse.Msg = ex.Message;
            }
            return ajaxResponse;
        }
        #endregion
    }

   
}
namespace wtujvk.LearningMeCSharp.CoreMvc.Codes
{
    /// <summary>
    /// api-bearer教研拦截器
    /// </summary>
    public class BearerIdentityFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 校验userid和username是否合法
        /// </summary>
        public static Func<BearerModel, bool> func = c => true;

        string validateToken = string.Empty;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //不需要验证的特性
            var allowAnonymous = context.ActionDescriptor.FilterDescriptors.Any(c => c.Filter.GetType() == typeof(AllowAnonymousAttribute));
            //如果不是匿名特性，就进行校验
            if (!allowAnonymous)
            {
                //context.Result = new HttpResponseMessage(HttpStatusCode.Forbidden)
                //{
                //    Content = new StringContent("请求非法，没有Authorize头信息！")
                //};
                if (!context.HttpContext.Request.Headers.ContainsKey("Authorize"))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new JsonResult("请求非法，没有Authorize头信息！");
                }
                else
                {
                    //最后一个Authorize
                    var list = context.HttpContext.Request.Headers["Authorize"].Where(c => c.IsValuable() && c.IsVisable()).LastOrDefault();
                    //actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    //{
                    //    Content = new StringContent("请求非法！")
                    //};
                    if (list == null)
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Result = new JsonResult("请求非法，没有获取到Authorize内容！");
                    }
                    var tokenArr = list.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    var access_token = tokenArr[0];
                    var refresh_token = tokenArr.Length > 1 ? tokenArr[1] : null;
                    var res = BearerImpl2.ValidateToken(access_token, func, refresh_token);
                    if (!res.OK || string.IsNullOrWhiteSpace(res.Res))
                    {
                        //actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                        //{
                        //    Content = new StringContent("校验失败！")
                        //};
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Result = new JsonResult("校验失败！");
                    }
                }
            }
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!string.IsNullOrWhiteSpace(validateToken) && CacheManager.Current.Get(validateToken) != null)
                context.HttpContext.Response.Headers.Add("Authorize", CacheManager.Current.Get(validateToken).ToString());
            base.OnActionExecuted(context);
        }
    }
}

namespace wtujvk.LearningMeCSharp.CoreMvc.Codes
{

    /// <summary>
    /// 登陆验证
    /// </summary>
    public class LoginFilter : ActionFilterAttribute
    {
        //IHttpContextAccessor accessor;
        //public LoginFilter(IHttpContextAccessor _accessor)
        //{
        //    accessor = _accessor;
        //}
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //不需要验证的特性
            var allowAnonymous = context.ActionDescriptor.FilterDescriptors.Any(c => c.Filter.GetType() == typeof(AllowAnonymousAttribute));
            //如果不是匿名特性，就进行校验
            if (!allowAnonymous)
            {
                //1.判断session有没有失效
                if (!context.HttpContext.Session.Keys.Contains(AppDataInit.LoginSessionKey))
                {
                    if (!context.HttpContext.Request.Cookies.ContainsKey(AppDataInit.LoginCookieKey))
                    {
                        context.Result = new RedirectResult("/Home/Login");
                    }
                    else
                    {
                        var cookie = context.HttpContext.Request.Cookies[AppDataInit.LoginCookieKey];
                    }
                }
                else
                {

                    var session = context.HttpContext.Session.GetString(AppDataInit.LoginSessionKey);
                }
                //2.判断cookie有没有失效
            }
            // base.OnActionExecuting(context);
        }
    }
}