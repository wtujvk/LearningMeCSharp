using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wtujvk.LearningMeCSharp.CoreMvc.Models;
using wtujvk.LearningMeCSharp.ToolStandard;
using wtujvk.LearningMeCSharp.ToolStandard.Messages;
using wtujvk.LearningMeCSharp.ToolStandard.Objs;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.CoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment hostingEnv;

        static ILogger logger = LoggerFactory.Instance;

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
    }
}
