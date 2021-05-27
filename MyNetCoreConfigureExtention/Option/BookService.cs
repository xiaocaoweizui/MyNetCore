using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreConfigureExtention.Option
{

    public interface IBookSerivce
    {
        int GetMaxCount();
    }
    public class BookSerivceOptions
    {
        public int MaxCount { get; set; }
    }
    public class BookSerivce : IBookSerivce
    {
        //服务必须注册 非单例模式
        IOptionsSnapshot<BookSerivceOptions> _options;

        public BookSerivce(IOptionsSnapshot<BookSerivceOptions> options)
        {
            this._options = options;
        }

        public int GetMaxCount()
        {
            return _options.Value.MaxCount;
        }
    }
}
