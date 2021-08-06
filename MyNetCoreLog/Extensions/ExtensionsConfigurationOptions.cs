using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreLog.Extensions
{
    public class ExtensionsConfigurationOptions
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Trace;
    }
}
