using MiniQuick.Infrastructure.IOC;
using MiniQuick.Infrastructure.Log;
using MiniQuick.Message;
using MiniQuick.MessageBus.CommandBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BBS
{
    public interface IBBSService:ICommandHandler<PostCommand>
    {

    }

    public class BBSService : CommandActor,IBBSService
    {
        static ILogger logger = ObjectFactory.GetService<ILoggerFactory>().Create(typeof(BBSService));
        public BBSService():base(Guid.NewGuid().ToString("N"))
        {

        }

      
 
        public void Handle( PostCommand command)
        {
            Console.WriteLine("发布一个帖子" + "Thread-" + Thread.CurrentThread.ManagedThreadId + "===Id:" + this.ActorId+"==="+command.PostName+"PPPPP-"+this.GetHashCode());
            //throw new Exception("出错了");

        }

      
    }



   

    
}
