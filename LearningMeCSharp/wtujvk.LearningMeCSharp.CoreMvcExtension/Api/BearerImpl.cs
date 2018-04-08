
using Demo.LindAgile.Standard.SerializingObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.ToolStandard.Utils.SerializingObject;

namespace wtujvk.LearningMeCSharp.CoreMvcExtension
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
    /// Bearer授权方式
    /// </summary>
    public class BearerImpl
    {
        static IObjectSerializer objectSerializer = SerializerManager.Instance;
       // static CacheManager cacheManager = CacheManager.Instance;

        #region public Methods
        /// <summary>
        /// 用户登陆成功后，生成token并返回
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static BearerModel GeneratorBearer(string userId, string userName, int minutes = 5, int refMinutes = 60 * 24)
        {
            //生成token
            var model = new BearerModel
            {
                Access_token = Guid.NewGuid().ToString(),
                Refresh_token = Guid.NewGuid().ToString(),
                UserName = userName,
                Access_token_Expires = DateTime.Now.AddMinutes(minutes),
                Refresh_Token_Expires = DateTime.Now.AddMinutes(refMinutes),
                UserId = userId,
            };
            //存储token
            cacheManager.Put(model.Access_token, objectSerializer.SerializeObj(model));
            //返回token
            return model;
        }
        /// <summary>
        /// 用户登陆成功后，生成token并返回
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static BearerModel GeneratorBearer2(string userId, string userName, int minutes = 5, int refMinutes = 60 * 24,Action<BearerModel> After=null)
        {
            //生成token
            var model = new BearerModel
            {
                Access_token = Guid.NewGuid().ToString(),
                Refresh_token = Guid.NewGuid().ToString(),
                UserName = userName,
                Access_token_Expires = DateTime.Now.AddMinutes(minutes),
                Refresh_Token_Expires = DateTime.Now.AddMinutes(refMinutes),
                UserId = userId,
            };
            //存储token
            cacheManager.Put(model.Access_token, objectSerializer.SerializeObj(model),minutes);
            After?.Invoke(model);
            //返回token
            return model;
        }

        /// <summary>
        /// 從緩存中刪除token
        /// </summary>
        /// <param name="access_token"></param>
        public static void DelToken(string access_token)
        {
            if(access_token.IsVisable())
            cacheManager.Delete(access_token);
        }
        #endregion

        #region private & internal Methods
        /// <summary>
        /// 校验access_token的有效性
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="refresh_token"></param>
        /// <returns></returns>
        public static string ValidateToken(string access_token, string refresh_token = null)
        {
            if (string.IsNullOrWhiteSpace(access_token) || cacheManager.Get(access_token) == null)
                return string.Empty;

            var obj = objectSerializer.DeserializeObj<BearerModel>(cacheManager.Get(access_token).ToString());

            //校验access-token
            if (obj.Access_token.Equals(access_token)
                && obj.Access_token_Expires >= DateTime.Now)
                return access_token;

            //检验refresh-token
            if (!string.IsNullOrWhiteSpace(refresh_token)
                && obj.Refresh_token.Equals(refresh_token)
                && obj.Refresh_Token_Expires >= DateTime.Now)
            {
                //更新token
                cacheManager.Delete(obj.Access_token);
                obj = GeneratorBearer(obj.UserId, obj.UserName);
                cacheManager.Put(obj.Access_token, objectSerializer.SerializeObj(obj));
                return obj.Access_token;
            }

            return string.Empty;
        }

        /// <summary>
        /// 登陆后获取用户信息--测试用
        /// </summary>
        public static BearerModel GetBearerImplAfterLogin(string access_token, string refresh_token = null)
        {
            //1.校验
            var newtoken = ValidateToken(access_token);
            if (newtoken.IsVisable())
            {
                var obj = objectSerializer.DeserializeObj<BearerModel>(cacheManager.Get(access_token).ToString());
                return obj;
            }
            else
            {
                return new BearerModel ();
            }
        }
        #endregion
    }

    /// <summary>
    /// api-bearer教研拦截器
    /// </summary>
    public class BearerIdentityFilter : ActionFilterAttribute
    {

        string validateToken = string.Empty;
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //不需要验证的特性
            bool allowAnonymous =
                actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0
                ||
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0;

            //如果不是匿名特性，就进行校验
            if (!allowAnonymous)
            {
                if (!actionContext.Request.Headers.Contains("Authorize"))
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent("请求非法，没有Authorize头信息！")
                    };
                else
                {
                    var list = actionContext.Request.Headers.GetValues("Authorize");

                    if (list == null)
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                        {
                            Content = new StringContent("请求非法！")
                        };

                    var tokenArr = list.FirstOrDefault().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    var access_token = tokenArr[0];
                    var refresh_token = tokenArr.Length > 1 ? tokenArr[1] : null;
                    validateToken = BearerImpl.ValidateToken(access_token, refresh_token);
                    if (string.IsNullOrWhiteSpace(validateToken))
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                        {
                            Content = new StringContent("校验失败！")
                        };
                    }
                }
            }

            base.OnActionExecuting(actionContext);
        }

        //public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        //{

        //    if (!string.IsNullOrWhiteSpace(validateToken) && CacheManager.Instance.Get(validateToken) != null)
        //        actionExecutedContext.Response.Headers.Add("Authorize", CacheManager.Instance.Get(validateToken).ToString());
        //    base.OnActionExecuted(actionExecutedContext);
        //}
    }
}
