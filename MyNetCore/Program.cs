using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace MyNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
                    //����Ӧ������ʱ��Ҫ�����ã��� ��Ҫ�����Ķ˿ں�URL,����Ƕ���Լ�������
                    Console.WriteLine("ConfigureHostConfiguration ");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //ע��Ӧ�ó����Ҫ�ļ�������������õ���������������
                    Console.WriteLine("ConfigureWebHostDefaults ");
                    webBuilder.UseStartup<Startup>();

                    //webBuilder.ConfigureServices(services =>
                    //{
                    //    Console.WriteLine("StartUp:ConfigureServices");
                    //    services.AddRazorPages();
                    //});
                    //webBuilder.Configure(app =>
                    //{
                    //    //����ע���Լ����м��
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