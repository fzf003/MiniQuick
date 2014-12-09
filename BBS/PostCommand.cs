using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBS
{
    public class PostCommand:IMessage
    {
        public string PostName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
