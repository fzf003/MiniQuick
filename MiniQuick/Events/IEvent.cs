using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Events
{
    public interface IEvent:IMessage
    {
         int RetryCount { get; }
         TimeSpan Timeinterval { get; }
        
    }
}
