using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.MessageBus.CommandBus
{
    public interface ICommandBus
    {
        void Send<T>(T message);
        IDisposable Subscribe<T>(IObserver<T> handler);
    }

  
}
