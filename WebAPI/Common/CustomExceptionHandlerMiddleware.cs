using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Common
{
    public class CustomExceptionHandlerMiddleware
    {
    }


    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            //return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
            return builder;
        }
    }
}
