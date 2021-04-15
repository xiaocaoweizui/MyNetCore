using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.ServiceLifeTime
{

    public interface ISingletonService { };

    public class SingletonService: ISingletonService
    {
        public SingletonService()
        {
            Console.WriteLine("SingletonService Created!");
        }
    }
}
