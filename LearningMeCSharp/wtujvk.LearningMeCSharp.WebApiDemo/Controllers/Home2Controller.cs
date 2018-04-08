using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.LindAgile.IRepository;
using Microsoft.AspNetCore.Mvc;
using wtujvk.LearningMeCSharp.ToolStandard.Objs;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes.Ioc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes.Filters;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Controllers
{
    /// <summary>
    /// 测试home2
    /// </summary>
    [Route("api/home2")]
    public class Home2Controller : Controller
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IExtensionRepository<LoginInfo> logRepository;
        private IHostingEnvironment hostingEnv;
        public Home2Controller(IExtensionRepository<LoginInfo> repository, IHostingEnvironment env)
        {
             logRepository = repository;
             hostingEnv = env;
             // logRepository = IocHelper.Get<IExtensionRepository<LoginInfo>>();
        }
        [AccessKey]
        [HttpGet, Route("index")]
        public IActionResult Index()
        {
            return Json(DateTime.Now);
        }
        [HttpGet, Route("logcount")]
        public IActionResult LogCount()
        {
            int count = logRepository.GetQuery().Count();
            return Json(new { count });
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost, Route("add")]
        public IActionResult Add([FromBody]LoginInfo log)
        {
            AjaxResponseData<bool> ajaxres = AjaxResponseData.GetAjaxResponseData<bool>();
            try
            {
                if (log != null && ModelState.IsValid)
                {
                    logRepository.Insert(log);
                    logRepository.SaveChanges();
                    ajaxres.SetOk();
                }
                else
                {
                    var msgs = ModelState.GetErrorMsg();
                    if (msgs.AnyElement())
                    {
                        ajaxres.Msg = string.Join(",", msgs);
                    }
                    else
                    {
                        ajaxres.Msg = "验证未通过";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ajaxres.Msg = ex.Message;
            }
            return Json(ajaxres);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="logid"></param>
        /// <returns></returns>
        [HttpPut,HttpPost]
        public IActionResult Delete(long logid)
        {
            AjaxResponseData ajax = AjaxResponseData.GetAjaxResponseData();
            try
            {
                logRepository.DeleteFromQuery(c => c.Id == logid);
                ajax.SetOk();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ajax.Msg = ex.Message;
            }
            return Json(ajax);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [HttpPost, Route("update")]
        public IActionResult Update([FromBody]LoginInfo log)
        {
            AjaxResponseData ajaxres = AjaxResponseData.GetAjaxResponseData();
            try
            {
                if (log != null && ModelState.IsValid)
                {
                    logRepository.UpdateFromQuery(c => c.Id == log.Id, c => new LoginInfo()
                    {
                        ExSource = log.ExSource,
                        LogLevel = log.LogLevel,
                        Message = log.Message,
                        ExceptionName = log.ExceptionName,
                        ThreadId = log.ThreadId,
                        ExTargetSite = log.ExTargetSite
                    });
                    ajaxres.SetOk();
                }
                else
                {
                    var msgs = ModelState.GetErrorMsg();
                    if (msgs.AnyElement())
                    {
                        ajaxres.Msg = string.Join(",", msgs);
                    }
                    else
                    {
                        ajaxres.Msg = "验证未通过";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ajaxres.Msg = ex.Message;
            }
            return Json(ajaxres);
        }
        /// <summary>
        /// 文件上传 [upload]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json", "multipart/form-data")]//此处为新增
        [Route("upload")]
        [FileUpload("上传文件测试")]
        public async Task<IActionResult> UploadAsync(string type, IFormFile file)
        {
            AjaxResponseData<string> ajax = AjaxResponseData.GetAjaxResponseData<string>();
            try
            {
                if(file == null || file.Length == 0)
                {
                    ajax.Msg = "file not selected";
                }
                else
                {
                    var dir = Path.Combine(hostingEnv.WebRootPath, "upload");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var ext = file.FileName;
                    var virfile = $"/upload/{Guid.NewGuid().ToString("N")}{Path.GetExtension(file.FileName)}";
                    var path = Path.Combine(hostingEnv.WebRootPath,virfile.Substring(1));
                    using (var stream = new FileStream(path, FileMode.CreateNew))
                    {
                        await file.CopyToAsync(stream);
                    }
                    ajax.SetOk(true, "OK", virfile);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                ajax.Msg = ex.Message;
            }
            return Json(ajax);
        }
    }
}