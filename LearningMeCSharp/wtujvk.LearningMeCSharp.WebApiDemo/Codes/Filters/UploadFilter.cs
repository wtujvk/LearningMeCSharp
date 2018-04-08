using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes
{
    public class HttpHeaderOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            var actionAttrs = context.ApiDescription.ActionAttributes();

            var isAuthorized = actionAttrs.Any(a => a.GetType() == typeof(AuthorizeAttribute));

            if (isAuthorized == false) //提供action都没有权限特性标记，检查控制器有没有
            {
                var controllerAttrs = context.ApiDescription.ControllerAttributes();

                isAuthorized = controllerAttrs.Any(a => a.GetType() == typeof(AuthorizeAttribute));
            }

            var isAllowAnonymous = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            if (isAuthorized && isAllowAnonymous == false)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",  //添加Authorization头部参数
                    In = "header",
                    Type = "string",
                    Required = false
                });
            }
        }
    }

    /// <summary>
    /// 文件上传标记
    /// </summary>
    public class SwaggerFileUploadFilter : IOperationFilter
    {
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            var actionAttrs = context.ApiDescription.ActionAttributes();

            var iahavefile = actionAttrs.Any(a => a.GetType() == typeof(FileUploadAttribute));
            var fileattribute = actionAttrs.FirstOrDefault(a => a.GetType() == typeof(FileUploadAttribute)) as FileUploadAttribute;
            if (fileattribute!=null)
            {   
                if (operation?.Parameters?.Count > 0)
                {
                    operation.Parameters.Clear();//Clearing parameters
                }
                else
                {
                    operation.Parameters = new List<IParameter>();
                }
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "File",
                    In = "formData",
                    Description =fileattribute.DescriptionName,  //"Uplaod Image",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }

    /// <summary>
    /// 文件上传标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false,Inherited =false)]
    public class FileUploadAttribute : Attribute
    {
        public string DescriptionName { get; set; }
        public FileUploadAttribute() { }
        public FileUploadAttribute(string name)
        {
            DescriptionName = name;
        }
    }
}
