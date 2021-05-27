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
            //用于api的路由注册
            services.AddControllers();
            //标准的注册：
            services.AddSingleton<ApplyServiceOptions>();
            services.AddSingleton<IApplySerivce, ApplyService>();

            //将类中的属性（option）和配置中的信息绑定
            //多次请求，过程中如果修改了配置，类还是读取的第一次的数据
            services.Configure<OrderSerivceOptions>(Configuration.GetSection("MySection2"));
            services.AddSingleton<IOrderSerivce, OrderSerivce>();

            //多次请求读取，动态更新配置
            //IOptionsSnapshot
            services.Configure<BookSerivceOptions>(Configuration.GetSection("MySection2"));
            services.AddScoped<IBookSerivce, BookSerivce>();

            //多次请求读取，动态更新配置
            //IOptionsMonitor
            services.Configure<PhoneSerivceOptions>(Configuration.GetSection("MySection2"));
            services.AddSingleton<IPhoneSerivce, PhoneSerivce>();

            //以上代码也可以用静态扩展方法提炼


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
                //使用Controllers的映射
                endpoints.MapControllers();
                
            });
        }
    }
}
