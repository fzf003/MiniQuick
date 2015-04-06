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
using MiniQuick.MessageBus.EventBus;
using SimpleSample.Event;
using MiniQuick.Commands;

namespace SimpleSample.Commandhandler
{


    public class UserSimpleHandler : CommandHandler,
                                    ICommandHandler<CreateUserCommand>
                                   
                                  
                                    
    {
        static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(UserSimpleHandler));

        
      
        public override void OnCompleted()
        {
            Console.WriteLine("完成..");
            //base.OnCompleted();
        }

     

        public void Handle(CreateUserCommand command)
        {
            var eventbus = ObjectFactory.GetService<IEventBus>();
 
            eventbus.PublishAsync(new CreateUsered() { Name = "张三" });

            Console.WriteLine(command.Name+"Thread-"+Thread.CurrentThread.ManagedThreadId+"==="+Guid.NewGuid().ToString());

         
           
        }

    }
}
