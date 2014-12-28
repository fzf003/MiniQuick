using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Message
{

    public interface IActorContext
    {
        object Message { get; set; }

        MessageActor Actor { get; set; }

        Action OnCompleted { get; set; }

        Action<Exception> OnException { get; set; }

    }
    public class ActorContext : IActorContext
    {

        public MessageActor Actor
        {
            get;
            set;
        }
        public object Message { get;  set; }
        public ActorContext(object message, MessageActor actor, Action<Exception> onException, Action onCompleted)
        {
            this.Message = message;
            this.Actor = actor;
            this.OnException = onException;
            this.OnCompleted = onCompleted;
        }







        public Action OnCompleted
        {
            get;
            set;
        }

        public Action<Exception> OnException
        {
            get;
            set;
        }
    }
}
