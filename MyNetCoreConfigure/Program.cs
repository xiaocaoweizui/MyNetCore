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
                 //reloadOnChange �ļ�������Զ�ˢ��
                 builder.AddJsonFile("mysettings.json", optional: false, reloadOnChange: true);

                
                 //����һ���ڴ����
                 builder.AddInMemoryCollection(new Dictionary<string, string>()
                 {
                     { "A","a"},
                     { "B","b"},
                     { "Section1:C","c"}
                 });

                 //������滻,�˴����滻�ᱨ����ʾ -t �����ڣ���ôӦ���������滻�أ�
                 //var mapper = new Dictionary<string, string> { { "-t", "test3" } };
                 //builder.AddCommandLine(args, mapper);

                 IConfigurationRoot root = builder.Build();
                 Console.WriteLine($"A��{root["A"]}");
                 Console.WriteLine($"B��{root["B"]}");
                 Console.WriteLine($"C��{root["C"]}");
                 IConfigurationSection section = root.GetSection("Section1");
                 Console.WriteLine($"C��{section["C"]}");
                 
                 //׷���ļ����ͱ仯��
                 //IChangeToken ֻ��ִ��һ��,��ε������ᴥ��
                 //IChangeToken token = root.GetReloadToken();
                 //token.RegisterChangeCallback(state => {
                 //    Console.WriteLine($"MyKey1��{root["MyKey1"]}");
                 //}, root);

                 //���ı��༭���򿪣�Ϊʲô������Σ�
                 ChangeToken.OnChange(() => root.GetReloadToken(), () =>
                 {
                     Console.WriteLine($"MyKey1��{root["MyKey1"]}");
                 });

                 //�󶨵�ǿ����
                 var dto = new ConfigDTO();
                 root.Bind(dto);
                 //����һ���˽�б���
                 //root.Bind(dto, BinderOptions => { BinderOptions.BindNonPublicProperties = true; });
                 Console.WriteLine($"MyKey1��{dto.MyKey1}");
                 Console.WriteLine($"MyKey1��{dto.MyKey.Level1}");

             })
              .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
             });



    }
}
