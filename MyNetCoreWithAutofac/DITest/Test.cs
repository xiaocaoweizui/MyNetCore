using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreWithAutofac.DITest
{
    public interface ITest {
        string show();
    }

    public class Test:ITest
    {
        public string show()
        {
           return "Test Show ！！！";
        }
    }
}
