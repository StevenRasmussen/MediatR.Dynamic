using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test
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
            services.AddMediatR(typeof(Startup));
            services.AddControllers();
            services.AddMediatRDynamic();

            services.AddDynamicNotificationHandler<WeatherForecastRequest>();
            services.AddDynamicNotificationHandler<WeatherForecastRequest2>(); 

            services.AddSingleton<WeatherForcastNotHandler>();
            services.AddSingleton<WeatherForcast2NotHandler>();
            services.AddSingleton<WeatherForcastNotHandler2>();
            services.AddSingleton<WeatherForcast2NotHandler2>();
             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.ApplicationServices.GetService<WeatherForcastNotHandler>();
            _ = app.ApplicationServices.GetService<WeatherForcast2NotHandler>();
            _ = app.ApplicationServices.GetService<WeatherForcastNotHandler2>();
            _ = app.ApplicationServices.GetService<WeatherForcast2NotHandler2>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    /// <summary>
    /// used just to load Singletons at startup
    /// </summary>
    public class PreLoadSingletons
    {
        public PreLoadSingletons(IServiceProvider serviceProvider, List<Type> typeList)
        {
            typeList.ForEach((t) => {
                // inilize TickerAlerterNotificationHandler service
                _ = serviceProvider.GetService(t); 
            });
        } 
    }
}
