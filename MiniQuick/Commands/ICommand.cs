using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.Commands
{
    public interface ICommand : IMessage
    {
        string CommandId { get; set; }


        int RetryCount { get; }

        TimeSpan Timeinterval{get;}
  

    }

    public interface ICommand<T> : ICommand
    {
 
    }


}
