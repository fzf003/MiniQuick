using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniQuick.Message;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Infrastructure.Log;
using SimpleSample.Command;
using MiniQuick.MessageBus.CommandBus;
using System.Threading;

namespace SimpleSample.Commandhandler
{


    public class UserSimpleHandler : MessageActor,
                                    ICommandHandler<CreateUserCommand>,
                                    ICommandHandler<string>
    {
        static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(UserSimpleHandler));

        public void Handle(CreateUserCommand command)
        { 
            var result= new Result();
                Console.WriteLine(string.Format("接收到:{0}-{1}-{2}", command.MessageId, command.Name,Thread.CurrentThread.ManagedThreadId));
                 result.IsSuccess = true;
                 command.ResultStatus = result;
        

        }


        public override void OnCompleted()
        {
            //base.OnCompleted();

            Console.WriteLine("完成");
        }






        public void Handle(string command)
        {
            Console.WriteLine(command);
        }
    }
}
