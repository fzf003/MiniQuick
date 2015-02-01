using MiniQuick.Message;
using MiniQuick.MessageBus.CommandBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS
{

  

    public class PostCommand : BaseCommand
    {
        public string PostName { get; set; }

        public DateTime CreateTime { get; set; }

      
    }
}
