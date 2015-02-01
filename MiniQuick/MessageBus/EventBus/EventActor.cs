using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniQuick.Common;
namespace MiniQuick.MessageBus.EventBus
{
    public abstract class EventActor : IDisposable, IObserver<object>
    {
        private string _ActorId;

        public EventActor(string ActorId)
        {
            this._ActorId = ActorId;

        }

        public string ActorId
        {
            get
            {
                return this._ActorId;
            }
        }

        public EventActor()
            : this(Guid.NewGuid().ToString("N"))
        {
        }



        public virtual void Dispose()
        {

        }

        public virtual void OnCompleted()
        {

        }

        public virtual void OnError(Exception error)
        {

        }

        public void OnNext(object value)
        {

            try
            {
                new Action(() =>
                {
                    ((dynamic)this).Handle((dynamic)value);

                }).Retry(TimeSpan.FromMilliseconds(1000));

                ((BaseEvent)value).OnCompleted();
            }
            catch (Exception ex)
            {
                ((BaseEvent)value).OnError(ex);
                OnError(ex);
            }



        }

    }
}
