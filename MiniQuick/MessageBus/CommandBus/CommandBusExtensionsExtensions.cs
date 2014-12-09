using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.MessageBus.CommandBus
{
    public static class CommandBusExtensions
    {

        public static Task SendAsync<T>(this ICommandBus bus, T message)
        {
            var sendaction = new Action(() => { bus.Send<T>(message); });
            return Task.Factory.FromAsync(sendaction.BeginInvoke, sendaction.EndInvoke, null);
        }

        public static IDisposable Subscribe<T>(this ICommandBus bus, Action<T> action)
        {
            return bus.Subscribe(new Handler<T>(action));
        }

    }
}
