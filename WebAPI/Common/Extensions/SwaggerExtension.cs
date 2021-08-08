using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
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
                case "azuread":
                    options.SwaggerAzureAd(configuration);
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

        private static void SwaggerAzureAd(this SwaggerGenOptions options, IConfiguration configuration)
        {
            options.DocumentFilter<AuthenticationSwaggerFilter>();
            
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{configuration.GetSection("AzureAd")["InstanceId"]}{configuration.GetSection("AzureAd")["TenantId"]}/oauth2/v2.0/authorize"),
                        TokenUrl = new Uri($"{configuration.GetSection("AzureAd")["InstanceId"]}{configuration.GetSection("AzureAd")["TenantId"]}/oauth2/v2.0/token"),
                        Scopes = new Dictionary<string, string> {
                        {
                            "api://ad5c206a-78a5-4b84-a409-bff4cffba703/ReadAccess",
                            "Read and Write Access"
                        }
                    }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "oauth2",
                            In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        }


        public static void SwaggerCustomUI(this SwaggerUIOptions options, IConfiguration configuration)
        {
            switch (configuration["AuthenticationType"].ToLower())
            {
                case "azuread":
                    options.SwaggerAzureAdCustomUI(configuration);
                    break;
            };
        }

        private static void SwaggerAzureAdCustomUI(this SwaggerUIOptions options, IConfiguration configuration)
        {
            options.OAuthClientId(configuration.GetSection("AzureAd")["ClientId"]);
            options.OAuthClientSecret(configuration.GetSection("AzureAd")["ClientSecret"]);
            options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        }
    }
}
