using Microsoft.Extensions.DependencyInjection;
using MyNetCoreConfigureExtention;
using MyNetCoreConfigureExtention.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public static class OrderServiceExtension
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<OrderSerivceOptions>(configuration);
            //下面这句代码和上面这句代码等同
            //services.AddOptions<OrderSerivceOptions>().Configure(options =>
            //{
            //    configuration.Bind(options);
            //});

            //动态修改配置信息
            services.PostConfigure<OrderSerivceOptions>(options =>
          {
              options.MaxCount = +100;
          });

            services.AddSingleton<IOrderSerivce, OrderSerivce>();

            return services;
        }
    }
}
