using MyNetCoreConfigureExtention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public static class MyConfigureBuilderExtension
    {
        public static IConfigurationBuilder AddMyConfigure(this IConfigurationBuilder builder)
        {
            builder.Add(new MyConfigureSource());
            return builder;
        }
    }
}
