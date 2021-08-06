using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyNetCoreLog.Extensions;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 生命周期测试问题 
    /// </summary>
    public static class LoggerServiceCollectionExtensions
    {
        /// <summary>
        /// 生命周期的测试
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyLogger(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ExtensionsConfigurationOptions>(configuration);

            services.AddScoped<ILoggerProvider, ExtensionsLoggerProvider>();
            var provider = services.BuildServiceProvider().GetService<ILoggerProvider>();

            services.AddLogging(config =>
            {
                config.ClearProviders();
                config.AddProvider(provider);
          
            });
            return services;
        }
    }


}
