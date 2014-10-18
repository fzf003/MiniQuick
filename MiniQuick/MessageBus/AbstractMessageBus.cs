using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace MiniQuick.MessageBus
{

    public abstract class AbstractMessageBus<T> 
    {
        ISubject<T> subject = new Subject<T>();

        public void Send(T message)
        {
            subject.OnNext(message);
        }

        public IDisposable Subscribe(IObserver<T> handler)
        {
            return subject.Subscribe(handler);
        }

    }

}
