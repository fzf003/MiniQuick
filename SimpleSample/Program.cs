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
using System.IO;
using SimpleSample.MessageProcess;
using BBS;
using MiniQuick.Aop;
using SimpleSample.Aop;
using SimpleSample.Event;

namespace SimpleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            SendDomainEvent();

            SendCommand();

          

            //BBS();

             Console.WriteLine("to do something!!!!");  
             Console.WriteLine("=======================华丽的分割线==============================");
             Console.ReadKey();
        }


        static void BBS()
        {
            ICommandBus<PostCommand> bus = ObjectFactory.GetService<ICommandBus<PostCommand>>();
                                                        //.AsProxy<ICommandBus<PostCommand>>((factory) =>
                                                        //{
                                                        //    factory.AddAdvice(new AroundAdvice());
                                                        //    factory.AddAdvice(new ThrowsAdvice());
                                                        //});

            bus.Subscribe(ObjectFactory.GetService<BBSService>());

            PostCommand createuser = new PostCommand();

            createuser.PostName = "我的帖子";

            createuser.CreateTime = DateTime.Now;

            bus.Send(createuser);
        }

        

        static void SendDomainEvent()
        {
            IEventBus<CreateUsered> eventbus = new DefaultEventBus<CreateUsered>();

            eventbus.Subscribe(new Handler<CreateUsered>((CreateUsered item) => { Console.WriteLine(item.Name); }));

            eventbus.PublishAsync(new CreateUsered() {  Name="张三" });
        }

        static void SendCommand()
        {
            CreateUserCommand createuser = new CreateUserCommand();

            createuser.Name = "fzf003";

            createuser.MessageId = "1";

            ICommandBus<CreateUserCommand> bus = new DefaultCommandBus<CreateUserCommand>();
          
            bus.Subscribe(new UserSimpleHandler());

            bus.Send(createuser);
            Console.WriteLine("result:"+createuser.ResultStatus.IsSuccess);
        }

        static void Init()
        {
            Assembly[] assemblylist = { Assembly.GetExecutingAssembly(),
                                        Assembly.GetAssembly(typeof(BBSServiceModule))
                                      };

            MiniQuick.Configuration.Create()
                     .UseAutoFac()
                     .RegisterModules(assemblylist)
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
            builder.RegisterGeneric(typeof(ServiceProxyFactory<>)).As(typeof(IServiceProxyFactory<>)).SingleInstance();
        }
    }
}
