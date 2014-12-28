using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;

namespace MiniQuick.Channel
{
    public interface IChannel<T>
    {
        IObservable<T> Receiver { get;  }

        Task SendAsync(T message);
    }

    public  class AbstractChannel<T>:IChannel<T>
    {
         private ISubject<T> _points;
       
        public AbstractChannel()
        {
            _points = new ReplaySubject<T>();
            var src = _points.ObserveOn(DefaultScheduler.Instance);
            this.Receiver= OutMessage(src);
        }
 
        IObservable<T> OutMessage(IObservable<T> source)
        {
            return Observable.Create<T>(observer =>
            {
                var d = source.Subscribe(observer);

                return Disposable.Create(() =>
                {
                    d.Dispose();
                });
            });
        }

       

        public Task SendAsync(T message)
        {
           return Observable.Start
                (() => { this._points.OnNext(message); }).ToTask();
        }

        public IObservable<T> Receiver
        {
            get;
            private set;
        }
    }

    
}
