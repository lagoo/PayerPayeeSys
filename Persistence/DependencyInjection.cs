using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDatabase")));

            services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());
            services.AddDbContext<ApplicationContext>();

            return services;
        }
    }
}
