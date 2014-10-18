using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Infrastructure.IOC
{
    /// <summary>
    /// 供开发者使用的服务定位
    /// </summary>
    public class ObjectFactory
    {
        public static IObjectContainer Current { get; private set; }

        public static void SetContainer(IObjectContainer container)
        {
            Current = container;
        }

        public static T GetService<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public static T GetService<T>(string key)
        {
            return ServiceLocator.Current.GetInstance<T>(key);
        }

        public static IEnumerable<T> GetServices<T>()
        {
            return ServiceLocator.Current.GetAllInstances<T>();
        }

        public static IServiceLocator Servicelocator
        {
            get
            {
                return ServiceLocator.Current;
            }
        }

    }
}
