using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniQuick.Message;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Infrastructure.Log;
using SimpleSample.Command;
using SimpleSample.Event;

namespace SimpleSample.Commandhandler
{
    public class UserEventHandler : MessageActor
    {
        static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(UserEventHandler));

        public void Handle(CreateUsered command)
        { 
                Console.WriteLine(string.Format("接收到:{0}-{1}", command.EventId, command.Name));

        }


        public override void OnError(Exception error)
        {
            //  logger.Error(error);
            Console.WriteLine(error);
        }

    }
}
