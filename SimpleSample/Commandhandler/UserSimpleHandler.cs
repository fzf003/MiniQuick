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
using System.Threading.Tasks;

namespace SimpleSample.Commandhandler
{


    public class UserSimpleHandler : CommandActor,
                                    ICommandHandler<CreateUserCommand>,
                                    ICommandHandler<string>
                                  
                                    
    {
        static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(UserSimpleHandler));

        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        //public void Handle( CreateUserCommand command)
        //{
        //    Console.WriteLine(command.Name);
        //}

      
        public override void OnCompleted()
        {
            Console.WriteLine("完成..");
            //base.OnCompleted();
        }

        public override void OnError(Exception error)
        {
            //base.OnError(error);
            Console.WriteLine(error.Message);
             
        }

        public void Handle(CreateUserCommand command)
        {
           
            Console.WriteLine(command.Name+"Thread-"+Thread.CurrentThread.ManagedThreadId);

            //throw new Exception("sd");
         
           
        }

        public void Handle(string command)
        {
            Console.WriteLine(command+"Thread-"+Thread.CurrentThread.ManagedThreadId);
        }
    }
}
