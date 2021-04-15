using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.ServiceLifeTime
{

    public interface IProduct { };

    public class Product : IProduct, IDisposable
    {
        public Product()
        {
            Console.WriteLine($"Product ( {this.GetHashCode()} ) Created!");
        }

        public void Dispose()
        {
            Console.WriteLine($"Product ( {this.GetHashCode()} )  Dispose");
        }
    }
}
