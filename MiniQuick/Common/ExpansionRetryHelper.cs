using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MiniQuick.Common
{
    public static class ExpansionRetryHelper
    {
         public static void Retry(this Action action, TimeSpan retryInterval, int retryCount = 3)
        {
            Retry<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount);
        }

        public static void Retry(this Action action, int retryCount = 3)
        {
            Retry<object>(() =>
            {
                action();
                return null;
            }, TimeSpan.Zero, retryCount);
        }

        public static T Retry<T>(this Func<T> func, int retryCount = 3)
        {
            return Retry<T>(() =>
            {
                return func();

            }, TimeSpan.Zero, retryCount);
        }

        public static T Retry<T>(this Func<T> action, TimeSpan retryInterval, int retryCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    Thread.Sleep(retryInterval);
                }
            }

            throw new AggregateException(exceptions);
        }
    
        public static bool Timeout(this Action action, TimeSpan timespan)
        {
            bool istimeout = false;
            Thread threadToKill = null;
            Action executeaction = () =>
            {
                threadToKill = Thread.CurrentThread;
                action();
            };

            IAsyncResult result = executeaction.BeginInvoke(null, null);
            if (result.AsyncWaitHandle.WaitOne(timespan))
            {
                executeaction.EndInvoke(result);
                istimeout = false;
            }
            else
            {
                try
                {
                    threadToKill.Abort();
                }
                catch
                {

                }
                istimeout = true;
            }
            return istimeout;
        }
    }
}
