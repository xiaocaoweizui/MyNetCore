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
using MyNetCoreConfigureExtention.Option;

namespace MyNetCoreConfigureExtention
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
            //����api��·��ע��
            services.AddControllers();
            //��׼��ע�᣺
            services.AddSingleton<ApplyServiceOptions>();
            services.AddSingleton<IApplySerivce, ApplyService>();

            //�����е����ԣ�option���������е���Ϣ��
            //������󣬹���������޸������ã��໹�Ƕ�ȡ�ĵ�һ�ε�����
            services.Configure<OrderSerivceOptions>(Configuration.GetSection("MySection2"));
            services.AddSingleton<IOrderSerivce, OrderSerivce>();

            //��������ȡ����̬��������
            //IOptionsSnapshot
            services.Configure<BookSerivceOptions>(Configuration.GetSection("MySection2"));
            services.AddScoped<IBookSerivce, BookSerivce>();

            //��������ȡ����̬��������
            //IOptionsMonitor
            services.Configure<PhoneSerivceOptions>(Configuration.GetSection("MySection2"));
            services.AddSingleton<IPhoneSerivce, PhoneSerivce>();

            //���ϴ���Ҳ�����þ�̬��չ��������


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
                //ʹ��Controllers��ӳ��
                endpoints.MapControllers();
                
            });
        }
    }
}
