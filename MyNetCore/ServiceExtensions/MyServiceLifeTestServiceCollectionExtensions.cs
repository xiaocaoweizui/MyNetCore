using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyNetCore.ServiceLifeTime;

namespace MyNetCore.ServiceExtention
{
    /// <summary>
    /// 生命周期测试问题 
    /// </summary>
    public static class MyServiceLifeTestServiceCollectionExtensions
    {
        /// <summary>
        /// 生命周期的测试
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceLifeTimeTest(this IServiceCollection services)
        {
             services.AddSingleton<ISingletonService>(new SingletonService());


            return services;
        }
    }


}
