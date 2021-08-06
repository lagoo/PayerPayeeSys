using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using WebAPI.Common.Models;
using WebAPI.Common.Implementations;
using WebAPI.Common.Interfaces;

namespace WebAPI.Common.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthenticationType(this IServiceCollection services, IConfiguration configuration)
        {
            switch (configuration["AuthenticationType"].ToLower())
            {
                case "jwt":
                    services.AddInternalJwt(configuration);
                    break;
                case "azuread":
                    services.AddAzureAd(configuration);
                    break;
            };
        }

        private static void AddInternalJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(options => configuration.GetSection("jwt").Bind(options));
            services.AddSingleton<IJwtHandler, JwtHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.RequireHttpsMetadata = false;
                        opt.SaveToken = true;
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidIssuer = configuration.GetSection("jwt")["Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("jwt")["SecretKey"]))
                        };
                    });
        }

        private static void AddAzureAd(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureAdOptions>(options => configuration.GetSection("AzureAd").Bind(options));            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.Audience = configuration.GetSection("AzureAd")["ResourceId"];
                        opt.Authority = $"{configuration.GetSection("AzureAd")["InstanceId"]}{configuration.GetSection("AzureAd")["TenantId"]}";                        
                    });
        }
    }
}
