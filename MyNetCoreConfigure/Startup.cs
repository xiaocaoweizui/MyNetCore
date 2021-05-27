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
using Microsoft.Extensions.Primitives;

namespace MyNetCoreConfigure
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
          
            //问题一：此处用Configuration 能否获取到配置？
            Console.WriteLine($"A2：{Configuration["A"]}");
        
            //命令行的参数
            Console.WriteLine($"test3：{Configuration["test3"]}");

            //环境变量的参数
            Console.WriteLine($"test4：{Configuration["test4"]}");
            Console.WriteLine($"ASPNETCORE_TEST4：{Configuration["ASPNETCORE_TEST4"]}"); //此处能查询到吗？
            Console.WriteLine($"test5：{Configuration["test5"]}");//此处能查询到吗？
            Console.WriteLine($"test6：{Configuration["test6"]}");//此处能查询到吗？
            Console.WriteLine($"ENVIRONMENT：{Configuration["ENVIRONMENT"]}");//此处能查询到吗？
       
            //获取环境变量中的分区
            var section1 = Configuration.GetSection("Section1");
            Console.WriteLine($"MyKey2：{section1["MyKey2"]}");
            var section2 = Configuration.GetSection("Section1:Section2");
            Console.WriteLine($"MyKey3：{section2["MyKey3"]}");
            
             
            //myseetings.json文件中读取
            Console.WriteLine($"MyKey1：{Configuration["MyKey1"]}"); 
            Console.ReadKey();
            Console.WriteLine($"MyKey1：{Configuration["MyKey1"]}");

            //appsetting.json文件中读取
            Console.WriteLine($"MySection：{Configuration["MySection"]}");
            Console.WriteLine($"MySection2.MaxCount：{Configuration.GetSection("MySection2")["MaxCount"]}");
            //奇怪了，为什么下面这句获取不到值？？
            Console.WriteLine($"OrderSerivce.MaxCount：{Configuration.GetSection("OrderSerivce")["MaxCount"]}");
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
