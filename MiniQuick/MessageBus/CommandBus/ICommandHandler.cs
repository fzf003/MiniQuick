using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.MessageBus.CommandBus
{
    public interface ICommandHandler<T>
    {
        void Handle(T command);
    }
}
