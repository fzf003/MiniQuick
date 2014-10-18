using MiniQuick.Infrastructure.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Log4net
{
    public static class ConfigurationExtensions
    {
        public static Configuration UseLog4Net(this Configuration configuration)
        {
            UseLog4Net(configuration, "log4net.config");

            return configuration;
        }


        public static Configuration UseLog4Net(this Configuration configuration, string configFile)
        {
            configuration.RegisterParts<ILoggerFactory, Log4NetLoggerFactory>(new Log4NetLoggerFactory(configFile));

            return configuration;
        }


    }
}
