using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.MessageBus.CommandBus
{
    public interface ICommand : IMessage
    {
        string CommandId { get; set; }

        Task<CommandResult> Completion { get; }
        int RetryCount { get; }

        TimeSpan Timeinterval{get;}
  
        void OnError(Exception ex);

        void OnCompleted();

    }

    public interface ICommand<T> : ICommand
    {
 
    }


}
