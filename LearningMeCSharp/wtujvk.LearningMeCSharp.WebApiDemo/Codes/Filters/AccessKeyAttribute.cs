using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes.Filters
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class AccessKeyAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="require"></param>
        public AccessKeyAttribute(string name="",bool require = false)
        {
            Name = name;
            IsRequired = require;
        }

        public string Name { get; set; } = "用户访问Key";
        public bool IsRequired { get; set; } = true;

        const string accessvalue = "123456789"; //这里这样写 ，正式项目不要这种做
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                var headers = context.HttpContext.Request.Headers;
                var accesskey = "access-key";
                if (headers.Any(c => c.Key == accesskey && c.Value == accessvalue))
                {
                    return;
                }
                else
                {
                    //验证没通过,处理未授权的请求
                    var friendlyMsg = $"需要填写{accesskey} - {accessvalue}";
                    var content = JsonConvert.SerializeObject(new { State = HttpStatusCode.Unauthorized, friendlyMsg });
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Result = new ContentResult() { Content = content, StatusCode = (int)HttpStatusCode.Forbidden, ContentType = "application/json" };
                }
            }
        }
    }

    /// <summary>
    /// swagger
    /// </summary>

    public class HttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }
            var actionAttrs = context.ApiDescription.ActionAttributes();
            var Authorized = actionAttrs.FirstOrDefault(a => a is AccessKeyAttribute) as AccessKeyAttribute;
            if (Authorized == null) //提供action都没有权限特性标记，检查控制器有没有
            {
                var controllerAttrs = context.ApiDescription.ControllerAttributes();
                Authorized = controllerAttrs.FirstOrDefault(a => a.GetType() == typeof(AccessKeyAttribute)) as AccessKeyAttribute;
            }
            var isAllowAnonymous = actionAttrs.Any(a => a is IAllowAnonymous);
            if (Authorized!=null && isAllowAnonymous == false)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "access-key",
                    @In = "header",
                    Description =Authorized.Name,   //"用户访问Key",
                    Required = Authorized.IsRequired,
                    Type = "string"
                });
            }
        }
    }
}
