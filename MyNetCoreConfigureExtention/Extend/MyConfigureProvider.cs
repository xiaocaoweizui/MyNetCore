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
        public override void Load()
        {
            MyLoad();
        }

        void MyLoad()
        {
            //可以远程从其他地方读取配置，看平台的解决方案
            this.Data["DateTime"] = DateTime.Now.ToString();

        }
    }
}
