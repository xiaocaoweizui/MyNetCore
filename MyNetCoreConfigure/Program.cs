using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace MyNetCoreConfigure
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
                 //reloadOnChange 文件变更了自动刷新
                 builder.AddJsonFile("mysettings.json", optional: false, reloadOnChange: true);

                
                 //增加一个内存对象
                 builder.AddInMemoryCollection(new Dictionary<string, string>()
                 {
                     { "A","a"},
                     { "B","b"},
                     { "Section1:C","c"}
                 });

                 IConfigurationRoot root = builder.Build();
                 Console.WriteLine($"A：{root["A"]}");
                 Console.WriteLine($"B：{root["B"]}");
                 Console.WriteLine($"C：{root["C"]}");
                 IConfigurationSection section = root.GetSection("Section1");
                 Console.WriteLine($"C：{section["C"]}");
                 
                 //追踪文件发送变化：
                 //IChangeToken 只会执行一次,多次调整不会触发
                 //IChangeToken token = root.GetReloadToken();
                 //token.RegisterChangeCallback(state => {
                 //    Console.WriteLine($"MyKey1：{root["MyKey1"]}");
                 //}, root);

               
                 ChangeToken.OnChange(() => root.GetReloadToken(), () =>
                 {
                     Console.WriteLine($"MyKey1：{root["MyKey1"]}");
                 });

                 //绑定到强类型
                 var dto = new ConfigDTO();
                 root.Bind(dto);
                 //下面一句绑定私有变量
                 //root.Bind(dto, BinderOptions => { BinderOptions.BindNonPublicProperties = true; });
                 Console.WriteLine($"MyKey1：{dto.MyKey1}");
                 Console.WriteLine($"MyKey1：{dto.MyKey.Level1}");

             })
              .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
             });



    }
}
