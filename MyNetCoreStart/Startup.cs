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
            ////用于做服务的注册
            Console.WriteLine("StartUp:ConfigureServices");

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //用于注入自己的中间件
            Console.WriteLine("StartUp:Configure");
            //使用路由组件
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
            //    .Where(t => t.FullName.EndsWith("Service") && !t.IsAbstract) //类名以service结尾，且类型不能是抽象的　
            //        .InstancePerLifetimeScope() //生命周期，，
            //        .AsImplementedInterfaces()
            //    .PropertiesAutowired(); //属性注入
            //}
        }
    }

}
