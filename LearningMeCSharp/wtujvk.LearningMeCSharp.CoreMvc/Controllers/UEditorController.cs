using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UEditorNetCore;

namespace wtujvk.LearningMeCSharp.CoreMvc.Controllers
{

    [Route("api/[controller]")] //配置路由
    public class UEditorController : Controller
    {
        private UEditorService ue;
        public UEditorController(UEditorService ue)
        {
            this.ue = ue;
        }
        public void Index()
        {
            var action = HttpContext.Request.Query["action"];
            ue.DoAction(HttpContext);

           // return View();
        }
    }
}