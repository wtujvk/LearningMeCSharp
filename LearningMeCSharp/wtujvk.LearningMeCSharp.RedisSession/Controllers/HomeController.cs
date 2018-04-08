using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using wtujvk.LearningMeCSharp.RedisSession.Codes;
using wtujvk.LearningMeCSharp.RedisSession.Models;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.RedisSession.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
            //  HttpContext.Session.Set(AppDataInit.SessionLoginKey,)
            if (!HttpContext.Session.Keys.Contains(AppDataInit.SessionLoginKey))
            {
                var student = new Student() { Name = RandomChinese.GetRandChineseName(), Score = StringExtend.GetRanAge(9999) };
                HttpContext.Session.SetString(AppDataInit.SessionLoginKey, student.ToJson());
                ToolStandard.LoggerFactory.Instance.Logger_Debug("创建session");
            }
            else
            {
                ToolStandard.LoggerFactory.Instance.Logger_Debug("session已经存在！");
            }
            
            ViewData["RequestInfo"] = GetNameValues();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["Session"] = HttpContext.Session.GetString(AppDataInit.SessionLoginKey).FromJosn<Student>();
            ViewData["RequestInfo"] = GetNameValues();
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            HttpContext.Session.Remove(AppDataInit.SessionLoginKey);
            ViewData["RequestInfo"] = GetNameValues();
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        public StringBuilder GetString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Host:{HttpContext.Request.Host}");
            sb.AppendLine($"Port:{HttpContext.Request.Host.Port}");
            sb.AppendLine($"RuntimeFramework:{PlatformServices.Default.Application.RuntimeFramework}");
            sb.AppendLine($"ApplicationName:{PlatformServices.Default.Application.ApplicationName}");
            sb.AppendLine($"ApplicationBasePath:{PlatformServices.Default.Application.ApplicationBasePath}");
            sb.AppendLine($"OSVersion:{System.Environment.OSVersion}");
            sb.AppendLine($"{HttpContext.Connection.LocalIpAddress}");
          
          //  sb.AppendLine($"{HttpContext.Request.Headers.}");
            return sb;
        }
        public NameValueCollection GetNameValues()
        {
            NameValueCollection nameValues = new NameValueCollection
            {
                { "请求开始时间:", DateTime.Now.ToString("HH:mm:ss") },
                { "服务器IP地址:", Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? "" },
                { "HTTP访问端口:", Request.Headers["SERVER_PORT"].FirstOrDefault() ?? "" },
                { "域名主机:", Request.Headers["HTTP_HOST"].FirstOrDefault() ?? "" },
                { "用户信息:", Request.Headers["HTTP_USER_AGENT"].FirstOrDefault() ?? "" },
                { "请求来源地址", Request.Headers["X-Real-IP"] },
                { "Host", HttpContext.Request.Host.Host },
                { "Port", HttpContext.Request.Host.Port.ToNotNullString() },
                { "RuntimeFramework", PlatformServices.Default.Application.RuntimeFramework.FullName },
                { "ApplicationName", PlatformServices.Default.Application.ApplicationName },
                { "ApplicationBasePath", PlatformServices.Default.Application.ApplicationBasePath },
                { "OSVersion", System.Environment.OSVersion.VersionString },
                { "LocalIpAddress", HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() },
                { "Referer", Request.Headers.ContainsKey("Referer") ? string.Join(",", Request.Headers["Referer"].ToList()) : "" }
            };
            return nameValues;
        }
    }
}
