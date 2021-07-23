using Application.Common.Interfaces;
using Common.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

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
