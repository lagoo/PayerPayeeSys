using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Extensions;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbConnectionConfig>(options => configuration.GetSection("DbConnection").Bind(options));
            
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseInMemoryDatabase("PayerPayeeSys"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>((provider, options) => { 
                    options.SetDbContextOptions(provider.GetRequiredService<IOptions<DbConnectionConfig>>().Value); });                
            }

            services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());            

            return services;
        }
    }
}
