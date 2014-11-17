using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniQuick.Message;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Infrastructure.Log;
using SimpleSample.Command;
using MiniQuick.MessageBus.CommandBus;

namespace SimpleSample.Commandhandler
{


    public class UserSimpleHandler : MessageActor, ICommandHandler<CreateUserCommand>
    {
        static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(UserSimpleHandler));

        public void Handle(CreateUserCommand command)
        { 
            var result= new Result();
            try
            {
                 
                Console.WriteLine(string.Format("接收到:{0}-{1}", command.MessageId, command.Name));
                 result.IsSuccess = true;
                 command.ResultStatus = result;
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                command.ResultStatus = result;
            }

        }


       

    }
}
