using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreConfigureExtention
{
    public class MyConfigureProvider : ConfigurationProvider
    {
        void Load(bool reload)
        {
            this.Data["DateTime"] = DateTime.Now.ToString();

        }
    }
}
