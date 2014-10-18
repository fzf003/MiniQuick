
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop.Framework;
using MiniQuick.Infrastructure.IOC;
namespace MiniQuick.Aop
{
    public interface IServiceProxyFactory<T>
    {
        T Create(T target, Action<ProxyFactory> advice);
    }

    public class ServiceProxyFactory<T> : IServiceProxyFactory<T>
    {
        IDictionary<string, T> dic = new Dictionary<string, T>();

        public T Create(T target, Action<ProxyFactory> advice)
        {
            T result = default(T);

            if (!dic.TryGetValue(target.GetType().FullName, out result))
            {
                var factory = new ProxyFactory(target);
                if (advice != null)
                {
                    advice(factory);
                }
                result = (T)factory.GetProxy();
                dic.Add(target.GetType().FullName, result);
            }

            return result;
        }
    }

  
}
