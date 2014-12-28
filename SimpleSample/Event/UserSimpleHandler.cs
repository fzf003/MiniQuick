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

namespace SimpleSample.Commandhandler
{
    public class UserEventHandler : MessageActor, IEventHandler<CreateUsered>
    {
        static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(UserEventHandler));

        public void Handle(CreateUsered command)
        { 
                Console.WriteLine(string.Format("接收到:{0}-{1}", command.EventId, command.Name));
        }
    }

    public class LoggerActor : MessageActor,
        IEventHandler<LoggerEvent>,
        IEventHandler<string>
    {
         
        public void Handle(LoggerEvent @event)
        {
            Console.WriteLine(@event.Exception.Message);
        }


        public void Handle(string @event)
        {
            Console.WriteLine(@event);
        }
    }
}
