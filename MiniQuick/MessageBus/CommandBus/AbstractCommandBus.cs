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
using System.Reactive.Disposables;
using MiniQuick.Commands;
 

namespace MiniQuick.MessageBus.CommandBus
{

    public abstract class AbstractCommandBus
    {
        private readonly CommandHandlerRegistry _subjects;
        public AbstractCommandBus(CommandHandlerRegistry subject)
        {
            this._subjects = subject;
        }


        public CommandResult Send<T>(T message) 
        {
            try
            {
                var observer = (ISubject<T>)_subjects.GetCommandHandler(typeof(T));
                observer.OnNext(message);
                return new CommandResult(CommandStatus.Success);
            }
            catch(Exception ex)
            {
                return new CommandResult(CommandStatus.Failed, ex);
            }
        }

        public IDisposable Subscribe<T>(IObserver<T> handler)
        {
            var subject = new Subject<T>();
            _subjects.AddCommandHandler(typeof(T), subject);
             subject.Subscribe(handler);

            return Disposable.Create(() =>
            {
                subject.OnCompleted();
                subject.Dispose();
                _subjects.RemoveCommandHandler(typeof(T));
               
            });
        }

        public void UnSubscribe<T>()
        {
            this._subjects.RemoveCommandHandler(typeof(T));
        }

    }

}
