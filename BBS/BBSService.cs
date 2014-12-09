using MiniQuick.Message;
using MiniQuick.MessageBus.CommandBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBS
{
    public interface IBBSService:ICommandHandler<PostCommand>
                                 ,ICommandHandler<string>,
                                 IObserver<object>
    {

    }

    public class BBSService : MessageActor,IBBSService
    {

        public void Handle(PostCommand command)
        {
             Console.WriteLine("发布一个帖子");
        }
 
        public void Handle(string command)
        {
            Console.WriteLine(command);
        }
    }



   

    
}
