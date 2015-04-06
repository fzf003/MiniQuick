using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Events
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }
}
