using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.MessageBus.CommandBus
{
    public interface ICommandBus
    {
        Commands.CommandResult Send<T>(T message);
 
        IDisposable Subscribe<T>(IObserver<T> handler);
        void UnSubscribe<T>();
    }

  
}
