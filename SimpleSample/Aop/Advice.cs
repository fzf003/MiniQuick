using AopAlliance.Intercept;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Infrastructure.Log;
using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleSample.Aop
{
    public class AroundAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {

            Console.WriteLine(string.Format("执行了{0}方法,参数为:{1}", invocation.Method.Name,invocation.Arguments.FirstOrDefault()));
            return invocation.Proceed();
        }
    }

    public class ThrowsAdvice : IThrowsAdvice
    {
        ILogger logger;

        public ThrowsAdvice()
        {
            logger = ObjectFactory.GetService<ILoggerFactory>().Create(this.GetType());
        }

        public void AfterThrowing(MethodInfo method, Object[] args, Object target, Exception exception)
        {
            logger.Error(exception);
        }
    }
}
