using MiniQuick.Common;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.Message
{
   public abstract class MessageActor : IDisposable, IObserver<object> 
    {
       private string _ActorId;

       private ICommandExecutor _CommandExecutor;

       public MessageActor(string ActorId) 
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

        public MessageActor() :this(Guid.NewGuid().ToString("N"))
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
                this._CommandExecutor = new CommandExecutor(new ActorContext(value, this, OnError, OnCompleted));
                this._CommandExecutor.Execute();
           
            
        }
        
    }
}
