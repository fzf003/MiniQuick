using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Actor
{
   public abstract class MessageActor : IDisposable, IObserver<object> 
    {

        public virtual void Handle(object message)
        {
            ((dynamic)this).When((dynamic)message);
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
                if (value != null)
                {
                    this.Handle(value);
                }
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }
    }
}
