using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Application;
using Infrastructure;
using Persistence;
using WebAPI.Common;
using FluentValidation.AspNetCore;
using Application.Common.Interfaces;
using WebAPI.Worker;
using WebAPI.Common.Extensions;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddApplication(true)
                    .AddInfrastructure(Configuration)
                    .AddPersistence(Configuration);
            
            services.AddAuthenticationType(Configuration);

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationContext>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers()
                    .AddNewtonsoftJson()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IApplicationContext>());            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayerPayeeSys", Version = "v1" });

                c.SwaggerAuthentication(Configuration);
            });

            services.AddHostedService<SendNotificationWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayerPayeeSys v1");
                    c.SwaggerCustomUI(Configuration);
                });
            }

            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
