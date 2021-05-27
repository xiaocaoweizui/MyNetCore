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
          
            //����һ���˴���Configuration �ܷ��ȡ�����ã�
            Console.WriteLine($"A2��{Configuration["A"]}");
        
            //�����еĲ���
            Console.WriteLine($"test3��{Configuration["test3"]}");

            //���������Ĳ���
            Console.WriteLine($"test4��{Configuration["test4"]}");
            Console.WriteLine($"ASPNETCORE_TEST4��{Configuration["ASPNETCORE_TEST4"]}"); //�˴��ܲ�ѯ����
            Console.WriteLine($"test5��{Configuration["test5"]}");//�˴��ܲ�ѯ����
            Console.WriteLine($"test6��{Configuration["test6"]}");//�˴��ܲ�ѯ����
            Console.WriteLine($"ENVIRONMENT��{Configuration["ENVIRONMENT"]}");//�˴��ܲ�ѯ����
       
            //��ȡ���������еķ���
            var section1 = Configuration.GetSection("Section1");
            Console.WriteLine($"MyKey2��{section1["MyKey2"]}");
            var section2 = Configuration.GetSection("Section1:Section2");
            Console.WriteLine($"MyKey3��{section2["MyKey3"]}");
            
             
            //myseetings.json�ļ��ж�ȡ
            Console.WriteLine($"MyKey1��{Configuration["MyKey1"]}"); 
            Console.ReadKey();
            Console.WriteLine($"MyKey1��{Configuration["MyKey1"]}");

            //appsetting.json�ļ��ж�ȡ
            Console.WriteLine($"MySection��{Configuration["MySection"]}");
            Console.WriteLine($"MySection2.MaxCount��{Configuration.GetSection("MySection2")["MaxCount"]}");
            //����ˣ�Ϊʲô��������ȡ����ֵ����
            Console.WriteLine($"OrderSerivce.MaxCount��{Configuration.GetSection("OrderSerivce")["MaxCount"]}");
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
