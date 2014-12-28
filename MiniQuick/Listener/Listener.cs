using MiniQuick.Common;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.MessageBus.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniQuick.Listener
{
    public interface IListener
    {
        void Start();
        void Pause();
        void Stop();
        IEventBus EventBus { get; }
    }

    public class Listener<T> : Disposable, IListener, IObservable<T> where T : class
    {

        IEventBus eventbus;
        Task worktask;
        public IEventBus EventBus
        {
            get
            {
                return this.eventbus;
            }
        }
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public Listener()
        {
            this.eventbus = ObjectFactory.GetService<IEventBus>();

            this.OnDispose = () =>
            {
                worktask.Dispose();
            };
        }

        public void Start()
        {
            worktask = Task.Factory.StartNew(ListenInternal, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
        }

        public void Pause()
        {
            _cancellationTokenSource.Cancel();
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        void ListenInternal(object state)
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                 

                }
                catch (ThreadAbortException) { }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }

     

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this.eventbus.Subscribe(observer);
        }
    }


    //public static class ListenerExtensions
    //{
    //    public static Listener<T> OnError<T>(this Listener<T> listener, Action<ChangeUserNamed> action) where T : class
    //    {
    //        listener.EventBus.Subscribe(action);
    //        return listener;
    //    }

    //    public static Listener<T> OnStatusChanged<T>(this Listener<T> listener, Action<CreateUsered> action) where T : class
    //    {
    //        listener.EventBus.Subscribe(action);
    //        return listener;
    //    }
    //}
}
