using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;
        private ExceptionHandlerOptions _options;

        public ErrorHandlingMiddleware(RequestDelegate next, IOptions<ExceptionHandlerOptions> options)
        {
            _next = next;
            _options = options.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch
            {
                //虽然此处指定500，但是出现异常，再次执行异常页面则响应的是200
                context.Response.StatusCode = 500;
                context.Response.Clear();
                if (_options.ExceptionHandlingPath.HasValue)
                {
                    context.Request.Path = _options.ExceptionHandlingPath;
                }
                RequestDelegate handler = _options.ExceptionHandler ?? _next;
                await handler(context);
            }
        }
    }
}
