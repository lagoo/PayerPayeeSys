using Application.Common.Interfaces;
using Common.Interface;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services.AddScoped<IMessageService, EmailMessageService>();            
            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, MachineDateTime>();
                        
            return services;
        }

    }
}
