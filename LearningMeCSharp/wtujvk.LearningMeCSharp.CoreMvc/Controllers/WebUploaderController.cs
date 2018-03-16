using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Demo.LindAgile.Standard.SerializingObject;
using Microsoft.AspNetCore.Mvc;
using wtujvk.LearningMeCSharp.CoreMvc.Codes;
using wtujvk.LearningMeCSharp.ToolStandard;
using wtujvk.LearningMeCSharp.ToolStandard.Objs;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.CoreMvc.Controllers
{
    public class WebUploaderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult ReceiveFileOne()
        //{
        //    AjaxResponseData<IEnumerable<string>> ajaxResponse=AjaxResponseData.GetAjaxResponseData<IEnumerable<string>>();
        //    try
        //    {

        //    }
        //    catch (Exception e)
        //    {
        //     LoggerFactory.Instance.Logger_Error(e);
        //     ajaxResponse.Msg = e.Message;
        //    }
        //    return Content(SerializerManager.Instance.SerializeObj(ajaxResponse));
        //}

        public string ReceiveFileOne()
        {
            AjaxResponseData<string> ajaxResponse =AjaxResponseData.GetAjaxResponseData<string>();
            string zoom =Request.Form["zoom"];//图片是否需要压缩，N：不需要
       
            string savePath = string.Empty;
            try
            {
               string Extension = ".png";
                string saveDir = string.Format("/{0}/{1:yyyy.MM.dd}/", AppDataInit.UploadTempDir, DateTime.Now);
                if (!System.IO.Directory.Exists(AppDataInit.WebRoot + saveDir))
                {
                    System.IO.Directory.CreateDirectory(AppDataInit.WebRoot + saveDir);
                }
                Random r = new Random();
                string typeStr = Request.Form["base64"].FirstOrDefault()??string.Empty;
                if (typeStr == "base64")
                {
                    //上传表单域名称
                    var UploadFieldName = Request.Form["UploadFieldName"].FirstOrDefault() ?? "";
                    var uploadFileBytes = Convert.FromBase64String(Request.Form[UploadFieldName]);

                }
                else
                {
                    var file = Request.Form.Files[0];
                    Extension = System.IO.Path.GetExtension(file.FileName);
                   var savefile = string.Format("{0}{1}{2}", saveDir,Guid.NewGuid().ToString("N"),Extension);
                    var physicpath = (AppDataInit.WebRoot+savefile).Replace("/","\\");
                    using (var stream = new FileStream(physicpath, FileMode.CreateNew))
                    {
                       // user.IdCardImg.CopyTo(stream);
                        file.CopyToAsync(stream);
                    }
                    ajaxResponse.SetOk(t:savefile);
                }
                //if (zoom == "N" || zoom == "n" || !ImageHelper.RewriteImage(uploadFileBytes, localPath, true))
                //{
                //    System.IO.File.WriteAllBytes(localPath, uploadFileBytes);
                //}
            }
            catch (Exception ex)
            {
              LoggerFactory.Instance.Logger_Error(ex);
                ajaxResponse.Msg = ex.Message;
            }
            var str = SerializerManager.Instance.SerializeObj(ajaxResponse);
            return str;
            // return Json(ajaxResponse);
        }
    }
}