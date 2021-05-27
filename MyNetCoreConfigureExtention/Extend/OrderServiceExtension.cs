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
            services.Configure<OrderSerivceOptions>(configuration.GetSection("MySection2"));
          
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
