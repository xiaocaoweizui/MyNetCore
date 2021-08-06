using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 //.ConfigureLogging((hostingContext, logging) =>
                 //{
                 //    logging.ClearProviders(); //ȥ��Ĭ����ӵ���־�ṩ����
                 //                              //��ӿ���̨���
                 //    logging.AddConsole();
                 //    //��ӵ������
                 //    logging.AddDebug();

                 //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
