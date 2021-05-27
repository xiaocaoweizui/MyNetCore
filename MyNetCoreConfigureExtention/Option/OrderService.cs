using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreConfigureExtention.Option
{


    public interface IOrderSerivce
    {
        int GetMaxCount();
    }

    public class OrderSerivceOptions
    {
        public int MaxCount { get; set; }
    }
    public class OrderSerivce : IOrderSerivce
    {
        IOptions<OrderSerivceOptions> _options;

        public OrderSerivce(IOptions<OrderSerivceOptions> options)
        {
            this._options = options;
        }

        public int GetMaxCount()
        {
            return _options.Value.MaxCount;
        }
    }
}
