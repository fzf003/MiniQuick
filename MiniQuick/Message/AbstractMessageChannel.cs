using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;


namespace MiniQuick.Message
{
    public class AbstractMessageChannel<T> : IMessageChannel<T>
    {
        private ISubject<T> _points;

        public AbstractMessageChannel()
        {
            _points = new ReplaySubject<T>();
            var src = _points.ObserveOn(DefaultScheduler.Instance);
            this.Receiver = ReceiverSubscribe(src);
        }

        IObservable<T> ReceiverSubscribe(IObservable<T> source)
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
            return Task.Factory.StartNew(() => this._points.OnNext(message));
        }

        public IObservable<T> Receiver
        {
            get;
            private set;
        }
    }
}
