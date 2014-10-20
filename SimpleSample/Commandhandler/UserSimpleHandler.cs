using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniQuick.Actor;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Infrastructure.Log;
using SimpleSample.Command;

namespace SimpleSample.Commandhandler
{
    public class UserSimpleHandler : MessageActor
    {
        ///static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(UserSimpleHandler));

        public void When(CreateUserCommand command)
        { 
            var result= new Result();
            try
            {
                throw new Exception("dd");
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


        public override void OnError(Exception error)
        {
            //  logger.Error(error);
            Console.WriteLine(error);
        }

    }
}
