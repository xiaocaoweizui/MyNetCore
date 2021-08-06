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
            //ע��logging�ķ���
            // services.AddLogging();

     
            // ֻʹ�õ�ǰ��չ����־��ʽ
            services.AddMyLogger(Configuration.GetSection("ExtendLogging"));

            LoggerTest(services);
        }

        public void LoggerTest(IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            var logger = provider.GetRequiredService<ILogger<Startup>>();
            logger.LogInformation("LogInformation��Ϣ");
            logger.LogError("LogError��Ϣ");
            logger.LogDebug("LogDebug��Ϣ");
            logger.LogTrace("LogTrace��Ϣ");
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
