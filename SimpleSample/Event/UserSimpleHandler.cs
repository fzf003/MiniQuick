using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniQuick.Message;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Infrastructure.Log;
using SimpleSample.Command;
using SimpleSample.Event;
using MiniQuick.MessageBus.EventBus;
using MiniQuick.MessageBus.CommandBus;
using MiniQuick.Events;

namespace SimpleSample.Commandhandler
{
    public class UserEventHandler : MiniQuick.Events.EventHandler, IEventHandler<CreateUsered>
    {
        static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(UserEventHandler));

        public void Handle(CreateUsered command)
        {
            throw new Exception("dsds");
            Console.WriteLine(string.Format("接收到:{0}-{1}", command.EventId, command.Name));
        }
    }

    public class LoggerActor : MiniQuick.Events.EventHandler,
        IEventHandler<LoggerEvent>
    {

        public void Handle(LoggerEvent @event)
        {
            Console.WriteLine(@event.Exception.Message);
        }



    }
}
