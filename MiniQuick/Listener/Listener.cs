using MiniQuick.Common;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.MessageBus.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniQuick.Listener
{
    

    public class Listener<T> : Disposable, IListener, IObservable<T> where T :  class,new()
    {

      private readonly  IEventBus eventbus;
      private System.Collections.Concurrent.BlockingCollection<T> _queue = new System.Collections.Concurrent.BlockingCollection<T>();
       // CancellationTokenSource _cancellationTokenSource;
       // Task worktask;
      private readonly IWorker Worker;
      
        public IEventBus EventBus
        {
            get
            {
                return this.eventbus;
            }
        }

        
        public Listener()
        {
            this.eventbus = ObjectFactory.GetService<IEventBus>();
            this.Worker = new Worker(typeof(T).FullName, ListenInternal);
        }

        public void Start()
        {
            this.Worker.Start();
        }

        public void Pause()
        {
            this.Worker.Stop();
        }

        public void Stop()
        {
            this.Worker.Stop();
        }

        void ListenInternal()
        {
            T message;
            if((message= _queue.Take())!=null)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine(Guid.NewGuid().ToString());
            }

           // Console.WriteLine(message);
            //while (!_cancellationTokenSource.IsCancellationRequested)
            //{
            //    try
            //    {


            //    }
            //    catch (ThreadAbortException) { }
            //    catch (Exception exc)
            //    {
            //        Console.WriteLine(exc.Message);
            //    }
            //}
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this.eventbus.Subscribe(observer);
        }

       
    }


    //public static class ListenerExtensions
    //{
    //    public static Listener<T> OnError<T>(this Listener<T> listener, Action<T> action) where T : class,new()
    //    {
    //        listener.EventBus.Subscribe(action);
    //        return listener;
    //    }

    //    public static Listener<T> OnStatusChanged<T>(this Listener<T> listener, Action<T> action) where T : class,new()
    //    {
    //        listener.EventBus.Subscribe(action);
    //        return listener;
    //    }
    //}
}
