using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace MiniQuick.Infrastructure.Process
{
    public interface IReplayService<T> : IObserver<T>
    {
        IObservable<T> ReplayMessage();
    }

    public class ReplayService<T> : IReplayService<T>
    {
        private ISubject<T> _points;

        public ReplayService()
        {
            _points = new ReplaySubject<T>();
        }

        public IObservable<T> ReplayMessage()
        {
            var src = _points.ObserveOn(DefaultScheduler.Instance);
            return OutMessage(src);
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

        public void OnNext(T value)
        {
            _points.OnNext(value);
        }

        public void OnError(Exception error)
        {

        }

        public void OnCompleted()
        {

        }


    }
}
