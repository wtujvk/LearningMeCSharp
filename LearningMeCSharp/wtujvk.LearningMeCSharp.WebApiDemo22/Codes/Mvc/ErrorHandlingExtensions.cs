using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder, ExceptionHandlerOptions options)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>(Options.Create(options));
        }
    }
}
