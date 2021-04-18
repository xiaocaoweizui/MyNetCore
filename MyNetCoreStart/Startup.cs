using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyNetCoreStart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Console.WriteLine("StartUp:Startup");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ////�����������ע��
            Console.WriteLine("StartUp:ConfigureServices");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //����ע���Լ����м��
            Console.WriteLine("StartUp:Configure");

            //ʹ��·�����
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/StartDemo", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
           
        }

    }
}