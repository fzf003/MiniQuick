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
using MiniQuick.Common;
using System.Reactive.Subjects;
using System.Reactive.Concurrency;
using SimpleSample.Model;
using System.Data.SqlClient;
using PetaPoco;
using SimpleSample.Business.Interface;
using SimpleSample.Business;
using System.Collections.Concurrent;
using MiniQuick.Common;

namespace SimpleSample
{
    class Program
    {

        static List<Timestamped<string>> GetMessage()
        {
            
            List<Timestamped<string>> list = new List<Timestamped<string>>();
            
            for (int i = 0; i < 100;i++ )
            {
                list.Add(new Timestamped<string>(Guid.NewGuid().ToString("N"), DateTimeOffset.Now));
            }

            return list;

                
        }


       static ConcurrentStack<string> _socketArgsPool = new ConcurrentStack<string>();

        static void Main(string[] args)
        {
            Init();




            SendDomainEvent();

            SendCommand();

            BBS();

          //  Rx();
    


          
             Console.WriteLine("to do something!!!!");  
             Console.WriteLine("=======================华丽的分割线==============================");
             Console.ReadKey();
        }


       
       
        static void Rx()
        {
            var subject = new Subject<KeyValuePair<int, string>>();

            var groups = subject.GroupBy(x => x.Key);

            groups.SelectMany(x => x).Subscribe(p => Console.WriteLine(p.Value));

            subject.OnNext(new KeyValuePair<int, string>(1, "a"));
            subject.OnNext(new KeyValuePair<int, string>(2, "b"));
        
            subject.OnNext(new KeyValuePair<int, string>(1, "c"));
            subject.OnCompleted();



            
        }


        static void BBS()
        {
            ICommandBus bus = ObjectFactory.GetService<ICommandBus>()
                                                        .AsProxy<ICommandBus>((factory) =>
                                                        {
                                                            factory.AddAdvice(new AroundAdvice());
                                                            factory.AddAdvice(new ThrowsAdvice());
                                                        });

            bus.Subscribe<PostCommand>(ObjectFactory.GetService<IBBSService>());
          
            {
                PostCommand createuser = new PostCommand();

                createuser.PostName = "我的帖子";

                createuser.CreateTime = DateTime.Now;

                bus.Send(createuser);


            }

            
        }

        

        static void SendDomainEvent()
        {
            IEventBus eventbus = new DefaultEventBus();

            eventbus.Subscribe<CreateUsered>(new UserEventHandler());
           

            eventbus.PublishAsync(new CreateUsered() { Name = "张三" });
            
        }

        static void SendCommand()
        {
            ICommandBus bus = new DefaultCommandBus();
        
            
                CreateUserCommand createuser = new CreateUserCommand();

                createuser.Name = "fzf003";

                createuser.MessageId = "oopp".ToString();

                bus.Subscribe<CreateUserCommand>((cc)=>Console.WriteLine(cc.Name));

                
                    bus.SendAsync(createuser);
              
               
            
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
            builder.RegisterType(typeof(DefaultCommandBus)).As(typeof(ICommandBus)).SingleInstance();
            builder.RegisterGeneric(typeof(ServiceProxyFactory<>)).As(typeof(IServiceProxyFactory<>)).SingleInstance();
            builder.Register(p => new PetaPocoRepository()).As<IRepository>();
        }
    }
}
