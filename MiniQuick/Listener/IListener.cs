using MiniQuick.MessageBus.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.Listener
{
    public interface IListener
    {
        void Start();
        void Pause();
        void Stop();
        IEventBus EventBus { get; }
    }
}
