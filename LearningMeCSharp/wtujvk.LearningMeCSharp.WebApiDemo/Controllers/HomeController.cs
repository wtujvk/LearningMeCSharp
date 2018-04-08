using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/home")]
    public class HomeController : Controller
    {
        [HttpGet,HttpPost]
        public IActionResult Index()
        {
            return Json(DateTime.Now);
        }
    }
}