using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MiniQuick.Common
{
    public static class Helper
    {
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
