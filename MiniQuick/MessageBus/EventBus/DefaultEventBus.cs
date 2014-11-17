using MiniQuick.MessageBus.CommandBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.MessageBus.EventBus
{
  
    public class DefaultEventBus<T> : AbstractMessageBus<T>, IEventBus<T>
    {
        public DefaultEventBus()
            :base(EventHandlerRegistry.Instance.Subjects)
        {

        }
        public void Publish(T @event)
        {
            this.Send(@event);
        }


        
    }



    public static class EventBusExtensions
    {

        public static Task PublishAsync<T>(this IEventBus<T> bus, T message)
        {
            var sendaction = new Action(() => { bus.Publish(message); });
            return Task.Factory.FromAsync(sendaction.BeginInvoke, sendaction.EndInvoke, null);
        }
 

        public static IDisposable Subscribe<T>(this IEventBus<T> bus, Action<T> action)
        {
            return bus.Subscribe(new Handler<T>(action));
        }

    }

}
