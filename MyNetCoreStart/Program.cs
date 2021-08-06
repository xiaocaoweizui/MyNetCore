using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
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
        //�鿴 �ٷ��ĵ���https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.hosting.host.createdefaultbuilder?view=dotnet-plat-ext-5.0
        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                     .ConfigureAppConfiguration(builder =>
                     {
                         //�����Լ��������ļ�����Ӧ�ö�ȡ
                         Console.WriteLine("ConfigureAppConfiguration ");
                     })
                     .ConfigureServices(services =>
                     {
                         //������ע��Ӧ�õ����
                         Console.WriteLine("ConfigureServices ");
                     })
                     .ConfigureHostConfiguration(builder =>
                     {
                         //����Ӧ�����ʱ��Ҫ�����ã��� ��Ҫ�����Ķ˿ں�URL,����Ƕ���Լ�������
                         Console.WriteLine("ConfigureHostConfiguration ");
                     })
                     .ConfigureWebHostDefaults(webBuilder =>
                     {
                         //ע��Ӧ�ó����Ҫ�ļ�������������õ���������������
                         //�鿴���룺ʹ��Ĭ��Kestrel����http��������Ĭ��ʹ��IIS���ɣ�������Ҫ���õ�
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
