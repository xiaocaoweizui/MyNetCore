using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreLog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //注册logging的服务
            // services.AddLogging();

     
            // 只使用当前扩展的日志方式
            services.AddMyLogger(Configuration.GetSection("ExtendLogging"));

            LoggerTest(services);
        }

        public void LoggerTest(IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            var logger = provider.GetRequiredService<ILogger<Startup>>();
            logger.LogInformation("LogInformation消息");
            logger.LogError("LogError消息");
            logger.LogDebug("LogDebug消息");
            logger.LogTrace("LogTrace消息");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

         

        }
    }
}
