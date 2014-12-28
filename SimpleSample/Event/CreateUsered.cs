using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSample.Event
{
    public interface IEvent : IMessage
    {

    }

    public class CreateUsered : IEvent
    {
        public string Name { get; set; }
        public string EventId { get; set; }
    }


    [Flags]
    public enum LoggerType
    {
        Info,
        Error,
        Debug

    }

    public class LoggerEvent
    {
        public LoggerType LoggerType { get; set; }
        public Exception Exception { get; set; }
        public LoggerEvent(LoggerType loggertype,Exception exception)
        {
            this.LoggerType = loggertype;
            this.Exception = exception;
        }
    }

     
}
