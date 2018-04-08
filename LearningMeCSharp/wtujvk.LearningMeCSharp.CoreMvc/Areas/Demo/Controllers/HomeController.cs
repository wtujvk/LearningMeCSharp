using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wtujvk.LearningMeCSharp.CoreMvc.Codes;

namespace wtujvk.LearningMeCSharp.CoreMvc.Areas.Demo.Controllers
{
    /// <summary>
    /// Demo
    /// </summary>
    [Area("Demo")]
    public class HomeController : Controller
    {
        const string UEcontentKey = "abc123";
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult UeditorDemo()
        {
            var html = HttpContext.Session.GetString(UEcontentKey) ?? string.Empty;

            ViewBag.HtmlStr = html;
            return View();
        }

        [LoginFilter]
        public IActionResult MeiTuXx()
        {
            return View();
        }
    }
}