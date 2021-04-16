using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore
{
    public interface IService { 
     void DoSomething()
        {

        }
    }
    public class MyService: IService
    {
        public void DoSomething()
        {
            Console.WriteLine("I am doing something!");
        }
    }
}
