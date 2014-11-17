using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.MessageBus.EventBus
{
    public interface IEventBus<T>
    {
        void Publish(T @event);

     

        IDisposable Subscribe(IObserver<T> handler);
    }
}
