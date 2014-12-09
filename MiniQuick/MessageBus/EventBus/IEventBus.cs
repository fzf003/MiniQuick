using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.MessageBus.EventBus
{
    public interface IEventBus
    {
        void Publish<T>(T @event);

     

        IDisposable Subscribe<T>(IObserver<T> handler);
    }
}
