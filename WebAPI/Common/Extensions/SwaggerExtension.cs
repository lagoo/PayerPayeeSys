using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebAPI.Common.Filters;

namespace WebAPI.Common.Extensions
{
    public static class SwaggerExtension
    {
        public static void SwaggerAuthentication(this SwaggerGenOptions options, IConfiguration configuration)
        {
            switch (configuration["AuthenticationType"].ToLower())
            {
                case "jwt":
                    options.SwaggerInternalJwt();
                    break;
            };
        }

        private static void SwaggerInternalJwt(this SwaggerGenOptions options)
        {
            options.OperationFilter<AddAuthHeaderOperationFilter>();
            options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Description = "`Token only!!!` - without `Bearer_` prefix",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer"
            });
        }
    }
}
