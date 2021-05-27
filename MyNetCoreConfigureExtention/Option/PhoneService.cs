using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreConfigureExtention.Option
{

    public interface IPhoneSerivce
    {
        int GetMaxCount();
    }
    public class PhoneSerivceOptions
    {
        public int MaxCount { get; set; }
    }
    public class PhoneSerivce : IPhoneSerivce
    {
        IOptionsMonitor<PhoneSerivceOptions> _options;

        public PhoneSerivce(IOptionsMonitor<PhoneSerivceOptions> options)
        {
            this._options = options;

            _options.OnChange(options =>
            {
                Console.WriteLine($"配置发送了变化:{GetMaxCount()}");
            });
        }

        public int GetMaxCount()
        {
            return _options.CurrentValue.MaxCount;
        }
    }
}
