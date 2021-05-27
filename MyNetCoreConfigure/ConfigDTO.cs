using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreConfigure
{
    public class ConfigDTO
    {
        public ConfigChildDTO MyKey { get; set; }
        public string MyKey1 { get; set; }
    }

    public class ConfigChildDTO
    {
        public string Level1 { get; set; }
        public string Level2 { get; set; }

        private int Num { get;} = 100;
    }

}
