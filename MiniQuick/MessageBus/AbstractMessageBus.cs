using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Reactive.Linq;
 

namespace MiniQuick.MessageBus.CommandBus
{

    public abstract class AbstractMessageBus<T>
    {
        private readonly ConcurrentDictionary<Type, object> _subjects;

        public AbstractMessageBus(ConcurrentDictionary<Type, object> subject)
        {
            this._subjects = subject;
        }

      
        public void Send(T message)
        {
            object subject;
            if (_subjects.TryGetValue(typeof(T), out subject))
            {
                var observer = (ISubject<T>)subject;
                observer.OnNext(message);
            }
        }

        public IDisposable Subscribe(IObserver<T> handler)
        {
            var subject = (ISubject<T>)_subjects.GetOrAdd(typeof(T), t => new Subject<T>());
            return subject.AsObservable().Subscribe(handler);
        }

    }

}
