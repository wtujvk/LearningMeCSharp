using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UEditor.Core;
using wtujvk.LearningMeCSharp.CoreMvc.Codes;


namespace wtujvk.LearningMeCSharp.CoreMvc.Controllers
{

    [BearerIdentityFilter]
   // [Route("api/[controller]")] //配置路由
    public class UEditorController : Controller
    {
        private readonly UEditorService _ueditorService;
        public UEditorController(UEditorService ueditorService)
        {
            this._ueditorService = ueditorService;
        }
        /// <summary>
        /// 百度编辑器主要入口
        /// </summary>
        /// <returns></returns>
        [AllowAnonymousAttribute]
        public IActionResult Index()
        {
            var response = _ueditorService.UploadAndGetResponse(HttpContext);
            return Content(response.Result, response.ContentType);
        }

        /// <summary>
        /// 截图上传
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public IActionResult UploadCropImg()
        {
            return View();
        }
    }
}