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
    public  class MyServiceLifeTest
    {
        /// <summary>
        /// 生命周期的测试
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public MyServiceLifeTest()
        {
            using (ServiceProvider rootProvider=new ServiceCollection()
                        .AddTransient<IOrder,Order>()
                        .AddScoped<IBook,Book>()
                        .AddSingleton<IProduct, Product>()
                        .BuildServiceProvider())
            {
                IOrder order1 = rootProvider.GetService<IOrder>();
                IOrder order2= rootProvider.GetService<IOrder>();

                IBook book1 = rootProvider.GetService<IBook>();
                IBook book2 = rootProvider.GetService<IBook>();

                IProduct product1 = rootProvider.GetService<IProduct>();
                IProduct product2 = rootProvider.GetService<IProduct>();


                IOrder order3 = null;
                IBook book3 = null;
                IProduct product3 = null;

                using(var scope = rootProvider.CreateScope())
                {
                    var childProvider = scope.ServiceProvider;
                    order3 = childProvider.GetService<IOrder>();
                    book3 = childProvider.GetService<IBook>();
                    product3 = childProvider.GetService<IProduct>();

                }

                var childProvider2 = rootProvider.CreateScope().ServiceProvider;
                IOrder order4 = childProvider2.GetService<IOrder>();
                //Order4被释放了没？


                //通过比较对象的哈希值对比
                Console.WriteLine($"order1==order2:{order1.GetHashCode()==order2.GetHashCode()}");

                Console.WriteLine($"order1的hashCode:{order1.GetHashCode() }");
                Console.WriteLine($"order2的hashCode:{order2.GetHashCode() }");
                Console.WriteLine($"order3的hashCode:{order3.GetHashCode() }");
                Console.WriteLine($"order4的hashCode:{order4.GetHashCode() }");

                Console.WriteLine($"book1的hashCode:{book1.GetHashCode() }");
                Console.WriteLine($"book2的hashCode:{book2.GetHashCode() }");
                Console.WriteLine($"book3的hashCode:{book3.GetHashCode() }");

                Console.WriteLine($"product1的hashCode:{product1.GetHashCode() }");
                Console.WriteLine($"product2的hashCode:{product2.GetHashCode() }");
                Console.WriteLine($"product3的hashCode:{product3.GetHashCode() }");
            }
        }
    }


}
