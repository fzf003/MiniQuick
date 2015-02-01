using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Reactive.Linq;
using MiniQuick.Message;
using System.Threading;
using System.Threading.Tasks;
 

namespace MiniQuick.MessageBus.CommandBus
{

    public abstract class AbstractCommandBus
    {
        private readonly CommandHandlerRegistry _subjects;
        public AbstractCommandBus(CommandHandlerRegistry subject)
        {
            this._subjects = subject;
        }

      
        public void Send<T>(T message) 
        {
            var observer = (ISubject<T>)_subjects.GetCommandHandler(typeof(T));
            observer.OnNext(message);
        }

        public IDisposable Subscribe<T>(IObserver<T> handler)
        {
            var subject = new Subject<T>();
            _subjects.AddCommandHandler(typeof(T), subject);
            return subject.Subscribe(handler); 
        }

    }

}
