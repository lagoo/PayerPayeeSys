using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Common.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddHttpClient<IAuthorizationService, AuthorizationService>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("AuthorizationServiceUrl").Value);
            });

            services.AddHttpClient<IMessageService, EmailMessageService>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("MessageServiceUrl").Value);
            });

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, MachineDateTime>();

            return services;
        }

    }
}
