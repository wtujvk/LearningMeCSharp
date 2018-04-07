using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Demo.LindAgile.Standard.SerializingObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wtujvk.LearningMeCSharp.CoreMvc.Codes;
using wtujvk.LearningMeCSharp.CoreMvc.Models;
using wtujvk.LearningMeCSharp.Entities;
using wtujvk.LearningMeCSharp.IRepository;
using wtujvk.LearningMeCSharp.ToolStandard;
using wtujvk.LearningMeCSharp.ToolStandard.factorys;
using wtujvk.LearningMeCSharp.ToolStandard.Messages;
using wtujvk.LearningMeCSharp.ToolStandard.Objs;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.CoreMvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        
        private IHostingEnvironment hostingEnv;

        static ILogger logger = LoggerFactory.Instance;
    IExtensionRepository<Admin_Menu> Admin_Menurepository = ModuleManager.Resolve<IExtensionRepository<Admin_Menu>>();
        IExtensionRepository<BearerEntity> BearerEntityrepository = ModuleManager.Resolve<IExtensionRepository<BearerEntity>>();
        public HomeController(IHostingEnvironment env)
        {
            hostingEnv = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //发送短信测试
        public IActionResult SendEmail()
        {
          

            return View();
        }

        public IActionResult SendToEamil(MessageContext messageContext,string Addresses)
        {
            var ajaxRes= AjaxResponseData.GetAjaxResponseData<object>();
            try
            {
                if (ModelState.IsValid)
                {
                    //判断邮件是否合法
                    if (messageContext != null)
                    {
                        if (messageContext.Addresses.AnyElement())
                        {
                           // var list = messageContext.Addresses.ToList();
                           var list = SendOutEmailTool.Current.EmailAddressCheck(messageContext.Addresses);
                            if (list.AnyElement())
                            {
                               bool res= SendOutEmailTool.Current.SendEmail(messageContext);
                                ajaxRes.Res = res;
                                if (res)
                                {
                                    ajaxRes.OK = res;
                                    ajaxRes.Msg = "OK";

                                }
                            }
                        }
                    }
                }
                else
                {
                    // ajaxRes.Msg = "验证未通过";
                    //ajaxRes = AjaxResponseData.GetAjaxResponseData<MessageContext>(false, "验证未通过", messageContext ?? new MessageContext());
                    ajaxRes.Res = messageContext;
                    ajaxRes.Msg = "验证未通过";
                }
               
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
                ajaxRes.Msg = ex.Message;
            }
            return Json(ajaxRes);
        }

        const string UEcontentKey = "";
        /// <summary>
        /// 百度编辑器
        /// </summary>
        /// <returns></returns>
        public IActionResult UeditorDemo()
        {
           var html= HttpContext.Session.GetString(UEcontentKey)??string.Empty;
            
            ViewBag.HtmlStr = html;
            return View();
        }
        
        public IActionResult SendUEToServer(string html)
        {
            var ajaxRes = AjaxResponseData.GetAjaxResponseData<string>();

            try
            {
                if (html.IsVisable())
                {
                    HttpContext.Session.SetString(UEcontentKey, html);
                    ajaxRes.Res = html;
                    ajaxRes.OK = true;
                    ajaxRes.Msg = "OK";
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
                ajaxRes.Msg = ex.Message;
            }
            return Json(ajaxRes);
        }

        public IActionResult Menu()
        {
            var menulist = Admin_Menurepository.GetQuery(c=>c.Id>0).ToList();
            //return View(menulist);
            ViewData["menuLst"] = menulist;
            return PartialView("_Menus");
        }


        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult VerifyCode()
        {
           var rancode= RandCode.Str(4);
            HttpContext.Session.SetString(AppDataInit.LoginverifyCodeKey, rancode);
           var yzmHelper= new YZMHelper(rancode);
            yzmHelper.CreateImage();
            var bytes= yzmHelper.OutPutBytes(yzmHelper.Image);
            return File(bytes, "image/jpeg");
        }

        public IActionResult LoginCheck(string loginName, string pwd,string verifyCode,string online)
        {
            AjaxResponseData<object> ajaxResponse = AjaxResponseData.GetAjaxResponseData<object>();
            try
            {
                if (!string.IsNullOrWhiteSpace(loginName) && !string.IsNullOrWhiteSpace(pwd) && !string.IsNullOrWhiteSpace(verifyCode))
                {
                    var yzm = HttpContext.Session.GetString(AppDataInit.LoginverifyCodeKey);
                    if (string.Equals(verifyCode, yzm, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var bearerModel = BearerEntityrepository.GetQuery(c => c.UserId == pwd && c.UserName == loginName).Select(c => new BearerModel() { UserId = c.UserId, UserName = c.UserName, Refresh_token = c.Refresh_token }).FirstOrDefault();
                        if (bearerModel != null)
                        {
                            //存入session cookie
                            string dataStr = SerializerManager.Instance.SerializeObj(bearerModel);
                            HttpContext.Session.SetString(AppDataInit.LoginSessionKey, dataStr);

                            HttpContext.Response.Cookies.Append(AppDataInit.LoginCookieKey, dataStr, new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                            ajaxResponse.SetOk(t: bearerModel);
                        }
                    }
                    else
                    {
                        ajaxResponse.Msg = $"验证码错误: ok={yzm},post={verifyCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                ajaxResponse.Msg = ex.Message;
                LoggerFactory.Instance.Logger_Error(ex);
            }
            return Json(ajaxResponse);
        }
    }
}
