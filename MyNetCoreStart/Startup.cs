using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
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

        public void ConfigureServices(IServiceCollection services)
        {
            ////�����������ע��
            Console.WriteLine("StartUp:ConfigureServices");

        }
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



        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {

            Console.WriteLine("StartUp:ConfigureContainer");

            //    Assembly service = Assembly.Load("MyNetCoreWithAutofac");
            //    Assembly iservice = Assembly.Load("MyNetCoreWithAutofac");
            //    containerBuilder.RegisterAssemblyTypes(service, iservice)
            //    .Where(t => t.FullName.EndsWith("Service") && !t.IsAbstract) //������service��β�������Ͳ����ǳ���ġ�
            //        .InstancePerLifetimeScope() //�������ڣ���
            //        .AsImplementedInterfaces()
            //    .PropertiesAutowired(); //����ע��
            //}
        }
    }

}
