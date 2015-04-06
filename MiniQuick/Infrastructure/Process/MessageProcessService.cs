using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Text;
using System.Reactive.Linq;
namespace MiniQuick.Infrastructure.Process
{
  
    public class MessageProcessService<T> : IMessageProcessService<T>
    {
        private readonly IEnumerable<Timestamped<T>> _source;

        public MessageProcessService(IEnumerable<Timestamped<T>> source)
        {
            _source = source;
        }

        public void Process(Action<IObservable<T>, HistoricalScheduler> query)
        {
            var enumerator = _source.GetEnumerator();

            var scheduler = new Scheduler(enumerator);

            query(scheduler.Source, scheduler);

            scheduler.Start();

        }

        class Scheduler : HistoricalScheduler
        {
            private readonly Subject<T> _subject = new Subject<T>();
            private readonly IEnumerator<Timestamped<T>> _enumerator;

            public Scheduler(IEnumerator<Timestamped<T>> enumerator)
            {
                _enumerator = enumerator;

                MoveNext(true);
            }

            public Scheduler(IEnumerator<Timestamped<T>> enumerator, DateTimeOffset startTime)
            {
                _enumerator = enumerator;

                this.AdvanceTo(startTime);
                MoveNext();
            }

            public void MoveNext(bool initializeInitialTimeFromLog = false)
            {
                var nextLog = default(Timestamped<T>);
                if (TryMoveNext(out nextLog))
                {
                    if (initializeInitialTimeFromLog)
                        this.AdvanceTo(nextLog.Timestamp);

                    ScheduleOnNext(nextLog);
                }
                else
                {
                    this.Schedule(_subject.OnCompleted);
                }
            }

            public IObservable<T> Source
            {
                get { return _subject.AsObservable(); }
            }

            private bool TryMoveNext(out Timestamped<T> value)
            {
                try
                {
                    if (_enumerator.MoveNext())
                    {
                        value = _enumerator.Current;
                        return true;
                    }
                }
                catch
                {
                    _enumerator.Dispose();
                    throw;
                }

                _enumerator.Dispose();

                value = default(Timestamped<T>);
                return false;
            }

            private void ScheduleOnNext(Timestamped<T> value)
            {
                this.Schedule(value.Timestamp, () =>
                {
                    _subject.OnNext(value.Value);
                    MoveNext();
                });
            }
        }
    }
}
