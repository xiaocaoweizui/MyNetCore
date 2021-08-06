using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreLog.Extensions
{
    public class ExtensionsLoggerProvider : ILoggerProvider
    {
        private readonly IOptions<ExtensionsConfigurationOptions> _config;

        public ExtensionsLoggerProvider(IOptions<ExtensionsConfigurationOptions> extensionsConfiguration)
        {
            _config = extensionsConfiguration;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ExtensionsLogger(_config);
        }

        public void Dispose()
        {
        }
    }
}
