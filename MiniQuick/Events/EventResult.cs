using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Events
{
    public class EventResult
    {
        public EventStatus Status { get; set; }
      
        public Exception ErrorMessage { get;  set; }


        public EventResult(EventStatus status, Exception errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }
    }
    public enum EventStatus
    {
        Success = 1,
        Failed,
    }
}
