using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.ServiceLifeTime
{

    public interface ITransientService { };

    public class TransientService : ITransientService
    {
        public TransientService()
        {
            Console.WriteLine("TransientService Created!");
        }
    }
}
