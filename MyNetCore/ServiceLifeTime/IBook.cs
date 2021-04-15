using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.ServiceLifeTime
{

    public interface IBook { };

    //对象实现了IDisposable接口后，当ServiceProvier释放时，自动回调用Dispose方法
    public class Book : IBook, IDisposable
    {
        public Book()
        {
            Console.WriteLine($"Book ( {this.GetHashCode()} ) Created!");
        }

        ~Book()
        {
            //Dispose();
        }


        public void Dispose()
        {
            Console.WriteLine($"Book ( {this.GetHashCode()} )  Dispose");
        }
    }
}
