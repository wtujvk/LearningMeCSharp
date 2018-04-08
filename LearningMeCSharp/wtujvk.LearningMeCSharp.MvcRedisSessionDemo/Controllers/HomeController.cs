using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wtujvk.LearningMeCSharp.MvcRedisSessionDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Content(DateTime.Now.ToString());
            return View();
        }
    }
}