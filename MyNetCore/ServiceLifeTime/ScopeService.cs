using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore.ServiceLifeTime
{

    public interface IScopeService { };

    public class ScopeService : IScopeService
    {
        public ScopeService()
        {
            Console.WriteLine("ScopeService Created!");
        }
    }
}
