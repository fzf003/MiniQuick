using MiniQuick.Message;
using MiniQuick.MessageBus.CommandBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBS
{


    public class BBSService : MessageActor, ICommandHandler<PostCommand>
    {

        public void Handle(PostCommand command)
        {
            Console.WriteLine("发布一个帖子");
        }
 
    }



   

    
}
