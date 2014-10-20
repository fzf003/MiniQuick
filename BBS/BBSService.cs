using MiniQuick.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBS
{


 public class BBSService : MessageActor
    {

        public void When(PostCommand command)
        {
            Console.WriteLine("发布一个帖子");
        }
 
    }



    public class PostCommand
    {
        public string PostName { get; set; }

        public DateTime CreateTime { get; set; }
    }

    
}
