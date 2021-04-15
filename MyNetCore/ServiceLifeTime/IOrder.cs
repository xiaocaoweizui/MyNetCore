using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.ServiceLifeTime
{

    public interface IOrder { };

    public class Order : IOrder, IDisposable
    {
        public Order()
        {
            Console.WriteLine($"Order ( {this.GetHashCode()} ) Created!");
        }

        public void Dispose()
        {
            Console.WriteLine($"Order ( {this.GetHashCode()} )  Dispose");
        }
    }
}
