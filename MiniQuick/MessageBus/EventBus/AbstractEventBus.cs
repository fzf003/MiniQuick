using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace MiniQuick.MessageBus.EventBus
{
    public abstract class AbstractEventBus
    {
        private readonly EventHandlerRegistry _subjects;

        public AbstractEventBus(EventHandlerRegistry subject)
        {
            this._subjects = subject;
        }


        public void Publish<T>(T message)
        {
            List<object> subject =this._subjects.GetEventHandler(typeof(T));
                  if(subject==null)
                  {
                      throw new Exception("没有注册信息");
                  }

            foreach(var item in subject)
            {
                var observer = (ISubject<T>)item;
                observer.OnNext(message);
            }
          
        }

        public IDisposable Subscribe<T>(IObserver<T> handler)
        {
            var subject = new Subject<T>();
            _subjects.AddEventHandler(typeof(T), subject);
            return subject.Subscribe(handler);
        }

    }
}
