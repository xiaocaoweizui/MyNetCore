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
            //花式注册，注册接口实现的实例：对应ServiceDescriptor.ImplementationInstance
            services.AddSingleton<ISingletonService>(new SingletonService());
            services.AddScoped<IScopeService, ScopeService>();
            // services.AddTransient<ITransientService, TransientService>();

            //注册接口实现的工厂：对应ServiceDescriptor.ImplementationFactory
            // services.AddTransient<ITransientService>((t) => { return new TransientService(); });

            services.Add(new ServiceDescriptor(typeof(ITransientService), typeof(TransientService), ServiceLifetime.Transient));


            services.AddSingleton<IService, MyService>();

            var provider = services.BuildServiceProvider();
            Console.WriteLine($"主管道 ServiceProvider 的 hashCode :{ provider.GetHashCode()}");

          
            Console.WriteLine($"主管道 ServiceProvider 的 ISingletonService 的 hashCode :{   provider.GetService<ISingletonService>().GetHashCode()}");
            Console.WriteLine($"主管道 ServiceProvider 的 IScopeService 的 hashCode :{   provider.GetService<IScopeService>().GetHashCode()}");
            Console.WriteLine($"主管道 ServiceProvider 的 ITransientService 的 hashCode :{   provider.GetService<ITransientService>().GetHashCode()}");

            return services;
        }
    }


}
