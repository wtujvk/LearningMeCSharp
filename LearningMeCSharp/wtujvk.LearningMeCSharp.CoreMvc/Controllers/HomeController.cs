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
        const string recipient = "1170971516@qq.com ";

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
        public IActionResult SendEmail(string _recipient=recipient)
        {
            //try
            //{
            //    if (!_recipient.IsVisable())
            //    {
            //        _recipient = recipient;
            //    }

            //    var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    var body = string.Format("这是收到的一条消息。<img src='{0}' alt='花间提壶方大厨' />","http://img1.imgtn.bdimg.com/it/u=2162087863,686892888&fm=27&gp=0.jpg");
            //    //MessageFactory.GetService(MessageType.Email).Send(recipient, "wtujvk.LearningMeCSharp.Console发送email消息", body, () => { logger.Logger_Debug("发送成功"); }, () => { logger.Logger_Info("发送失败"); });
            //    MessageContext messageContext = new MessageContext() { Addresses = new string[] { _recipient}, Type = MessageType.Email, Subject = "wtujvk.LearningMeCSharp.Console发送email消息", Body = body };

            //    SendOutEmailTool.Current.SendEmail(messageContext, () => { });
            //}
            //catch (Exception ex)
            //{
            //    logger.Logger_Error(ex);
            //    return BadRequest();
            //}

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
