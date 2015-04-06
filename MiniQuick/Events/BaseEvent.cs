using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.Events
{
    public class BaseEvent : IEvent
    {
        private int _MaxRetryCount = 3;
        private TimeSpan _timeinterval = TimeSpan.FromMilliseconds(1000);
         public string CommandId
        {
            get;
            set;
        }



        public int RetryCount
        {
            get { return this._MaxRetryCount; }
        }

        public TimeSpan Timeinterval
        {
            get { return this._timeinterval; }
        }
    }
}
