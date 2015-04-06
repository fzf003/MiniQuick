using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.Message
{
    public interface IMessageChannel<T>
    {
        IObservable<T> Receiver { get; }

        Task SendAsync(T message);
    }
}
