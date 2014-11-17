using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.MessageBus.CommandBus
{
    public interface ICommandBus<T>
    {
        void Send(T message);
        IDisposable Subscribe(IObserver<T> handler);
    }

  
}
