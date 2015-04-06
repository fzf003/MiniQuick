using MiniQuick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using System.Threading;
namespace MiniQuick.MessageBus.CommandBus
{
    public static class CommandBusExtensions
    {
 
        public static Task<CommandResult> SendAsync<T>(this ICommandBus bus, T mesage)
        {
             return GetResponse<T>(bus, mesage).ToTask();
        }

        static IObservable<CommandResult> GetResponse<T>(ICommandBus bus,T param)
        {
            return Observable.Create<CommandResult>(
                response => {
                    
                   return Observable.ToAsync<ICommandBus, T, CommandResult>(Execute)(bus, param).Subscribe(response); 
                }
            );
        }

        static CommandResult Execute<T>(ICommandBus bus,T param)
        {
                return bus.Send<T>(param);
        }

        public static IDisposable Subscribe<T>(this ICommandBus bus, Action<T> action)
        {
            return bus.Subscribe(new Handler<T>(action));
        }

    }
}
