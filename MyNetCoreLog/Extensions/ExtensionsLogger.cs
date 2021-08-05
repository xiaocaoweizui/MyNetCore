using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreLog.Extensions
{
    public class ExtensionsLogger : ILogger
    {
        private readonly IOptions<ExtensionsConfigurationOptions> _config;
        public ExtensionsLogger(IOptions<ExtensionsConfigurationOptions> extensionsConfiguration)
        {
            _config = extensionsConfiguration;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.Value.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            Console.WriteLine($" 自定义日志输出： {logLevel} - {eventId.Id} : " + formatter(state, exception));

        }
    }
}
