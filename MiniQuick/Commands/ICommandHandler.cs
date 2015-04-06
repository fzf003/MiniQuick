using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Commands
{
    public interface ICommandHandler<T> : IObserver<ICommand> 
    {
        void Handle(T command);
    }
}
