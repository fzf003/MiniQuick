using MiniQuick.Common;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;

namespace MiniQuick.MessageBus.CommandBus
{
   public abstract class CommandActor : IDisposable, IObserver<ICommand> 
    {
       private string _ActorId;

       

       public CommandActor(string ActorId) 
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

       public CommandActor()
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

        public void OnNext(ICommand value)
        {
            
                try
                {
                    new Action(() =>
                    {

                        ((dynamic)this).Handle((dynamic)value);

                    }).Retry(value.Timeinterval, value.RetryCount);

                    value.OnCompleted();
                }
                catch (Exception ex)
                {
                    value.OnError(ex);
                    OnError(ex);
                }


            
        }
        
    }
}
