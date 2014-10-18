using MiniQuick.Infrastructure.IOC;
using Spring.Aop.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Aop
{
    public static class ServiceProxyFactoryExtensions
    {
        public static T AsProxy<T>(this T self, Action<ProxyFactory> factoryregister)
        {
            return ObjectFactory.GetService<IServiceProxyFactory<T>>().Create(self, factoryregister);
        }
    }
}
