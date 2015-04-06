using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.MessageBus.EventBus
{

    public class DefaultEventBus : AbstractEventBus, IEventBus
    {
        public DefaultEventBus()
            : base(EventHandlerRegistry.Instance)
        {

        }
        


        
    }



    public static class EventBusExtensions
    {

        public static Task PublishAsync<T>(this IEventBus bus, T message)
        {
            var sendaction = new Action(() => { bus.Publish<T>(message); });
            return Task.Factory.FromAsync(sendaction.BeginInvoke, sendaction.EndInvoke, null);
        }


        public static IDisposable Subscribe<T>(this IEventBus bus, Action<T> action)
        {
            return bus.Subscribe(new Handler<T>(action));
        }

    }

}
