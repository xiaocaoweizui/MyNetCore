using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyNetCoreStart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 //查看 官方文档：https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.hosting.host.createdefaultbuilder?view=dotnet-plat-ext-5.0
                 Host.CreateDefaultBuilder(args)
                     .ConfigureAppConfiguration(builder =>
                     {
                         //配置自己的配置文件，供应用读取
                         Console.WriteLine("ConfigureAppConfiguration ");
                     })
                     .ConfigureServices(services =>
                     {
                         //往容器注入应用的组件
                         Console.WriteLine("ConfigureServices ");
                     })
                     .ConfigureHostConfiguration(builder =>
                     {
                         //配置应用启动时需要的配置，如 需要监听的端口和URL,可以嵌入自己的配置
                         Console.WriteLine("ConfigureHostConfiguration ");
                     })
                     .ConfigureWebHostDefaults(webBuilder =>
                     {
                         //注册应用程序必要的几个组件，如配置的组件、容器的组件
                         //查看代码：使用默认Kestrel进行http监听，并默认使用IIS集成，核心需要调用的
                         Console.WriteLine("ConfigureWebHostDefaults ");
                         webBuilder.UseStartup<Startup>();

                         //webBuilder.ConfigureServices(services =>
                         //{
                         //    Console.WriteLine("StartUp:ConfigureServices");
                         //    services.AddRazorPages();
                         //});
                         //webBuilder.Configure(app =>
                         //{
                         //    //用于注入自己的中间件
                         //    Console.WriteLine("StartUp:Configure");
                         //    app.UseHttpsRedirection();
                         //    app.UseStaticFiles();
                         //    //app.UseWebSockets()

                         //    app.UseRouting();

                         //    app.UseAuthorization();

                         //    app.UseEndpoints(endpoints =>
                         //    {
                         //        endpoints.MapRazorPages();
                         //    });
                         //});

                     });
    }
}
