using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.MessageBus.CommandBus
{
    public interface ICommandHandler<T> : IObserver<object> 
    {
        void Handle(T command);
    }
}
