using MiniQuick.Actor;
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

     
}
