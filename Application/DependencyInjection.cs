using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.Common.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, bool defaultServices = false)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


            if (defaultServices)
                services.AddDefaultServices();

            return services;
        }


        private static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, DefaultCurrentUserService>();
            services.AddScoped<IApplicationVersionService, DefaultApplicationVersionService>();

            return services;
        }
    }
}
