using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oculus.Scheduler;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForestInteractiveTestApp.Common;
using ForestInteractiveTestApp.Core;
using ForestInteractiveTestApp.Scheduler;

namespace ForestInteractiveTestApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            APIConfig.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContextCore>(options =>
            {
                options.UseSqlServer(APIConfig.Configuration?.GetSection("ConnectionStrings")["DefaultConnection"]);
            });

            services.AddMvc();
            services.AddRepository();
            services.AddHostedService<SchedulerHostedService>();

            // Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Scheduler services
            services.AddSingleton<SMSScheduler>();
            services.AddSingleton(new JobSchedule(
               jobType: typeof(SMSScheduler),
               cronExpression: "0 0/1 * 1/1 * ? *")); // Fires after 1 minute
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Application Starting!");
                });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
