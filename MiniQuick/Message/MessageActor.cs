using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Message
{
   public abstract class MessageActor : IDisposable, IObserver<object> 
    {
       private object Message;
        public virtual void On(object message)
        {
            this.Message = message;
            ((dynamic)this).Handle((dynamic)message);
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
                    this.On(value);
                }
            }
            catch (Exception ex)
            {
                this.OnError(new MessageException(ex.Message,ex));
                throw ex;
            }
           
        }


    }
}
