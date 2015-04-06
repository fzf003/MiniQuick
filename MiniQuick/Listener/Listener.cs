using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MiniQuick.Common;
using MiniQuick.Events;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.MessageBus.EventBus;

namespace MiniQuick.Listener
{

    public class Listener<T> : Disposable, IListener, IObservable<T> where T : BaseEvent
    {

      private readonly  IEventBus eventbus;
       
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
            this.OnDispose=()=>{};
        }

        public void Start()
        {
            
        }

        public void Pause()
        {
             
        }

        public void Stop()
        {
             
        }

        

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this.eventbus.Subscribe(observer);
        }

       
    }


    public static class ListenerExtensions
    {
        public static Listener<T> OnError<T>(this Listener<T> listener, Action<T> action) where T : BaseEvent 
        {
            listener.EventBus.Subscribe(action);
            return listener;
        }

        public static Listener<T> OnStatusChanged<T>(this Listener<T> listener, Action<T> action) where T : BaseEvent 
        {
            listener.EventBus.Subscribe(action);
            return listener;
        }
    }
}
