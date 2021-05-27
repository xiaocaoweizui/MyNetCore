using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreConfigureExtention.Option
{

    public interface IApplySerivce
    {
        int GetMaxCount();
    }

    public class ApplyServiceOptions
    {
        public int MaxCount { get; set; } = 100;
    }

    public class ApplyService : IApplySerivce
    {
        ApplyServiceOptions _options;

        public ApplyService(ApplyServiceOptions options)
        {
            this._options = options;
        }

        public int GetMaxCount()
        {
            return _options.MaxCount;
        }
    }
}
