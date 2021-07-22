using Microsoft.AspNetCore.Builder;

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
