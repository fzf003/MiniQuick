using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniQuick.Autofac;
using Autofac;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.Log4net;
using log4net;
using System.Reflection;
using MiniQuick.Infrastructure.Log;
using MiniQuick.MessageBus;
using SimpleSample.Command;
using SimpleSample.Commandhandler;
using System.Reactive.Linq;
using System.Reactive;
using MiniQuick.MessageBus.CommandBus;
using MiniQuick.MessageBus.EventBus;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            SendDomainEvent();

            SendCommand();

             Console.WriteLine("to do something!!!!");  
             Console.WriteLine("=======================华丽的分割线==============================");
             Console.ReadKey();
        }

        static void SendDomainEvent()
        {
            IEventBus<string> eventbus= new DefaultEventBus<string>();

            eventbus.Subscribe(new Handler<string>((string item) => { Console.WriteLine(item);}));

            eventbus.PublishAsync(Guid.NewGuid().ToString());
        }

        static void SendCommand()
        {
            CreateUserCommand createuser = new CreateUserCommand();

            createuser.Name = "fzf003";

            createuser.MessageId = "1";

            ICommandBus<CreateUserCommand> bus = new DefaultCommandBus<CreateUserCommand>();
          
            bus.Subscribe(new UserSimpleHandler());

            bus.SendAsync(createuser);
        }

        static void Init()
        {
            MiniQuick.Configuration.Create()
                     .UseAutoFac()
                     .RegisterModules(Assembly.GetExecutingAssembly())
                     .UseLog4Net()
                     .Start();
        }
    }

    public class SimpleModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Log4NetLoggerFactory>().As<ILoggerFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(DefaultCommandBus<>)).As(typeof(ICommandBus<>)).SingleInstance();
        }
    }
}
