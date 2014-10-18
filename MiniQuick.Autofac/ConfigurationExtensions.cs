using Autofac;
using Microsoft.Practices.ServiceLocation;
using MiniQuick.Autofac.Extensions;
using MiniQuick.Infrastructure.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MiniQuick.Autofac
{
    public static class ConfigurationExtensions
    {
        private static IContainer _rootContainer;

        public static Configuration UseAutoFac(this Configuration configuration)
        {
            configuration.UseAutoFac(null);

            return configuration;
        }

        public static Configuration UseAutoFac(this Configuration configuration, Action<ContainerBuilder> contaierconfig)
        {
                var rootBuilder = new ContainerBuilder();
                if (contaierconfig != null)
                {
                    contaierconfig(rootBuilder);
                }
               _rootContainer = rootBuilder.Build();
               _rootContainer.CreateServiceLocator();
    

            return configuration;
        }

        public static Configuration RegisterModules(this Configuration configuration,params Assembly[] assemblies)
        {
            var rootBuilder = new ContainerBuilder();
            rootBuilder.RegisterAssemblyModules(assemblies);
            if (_rootContainer == null)
            {
                UseAutoFac(null);
            }
             rootBuilder.Update(_rootContainer);
            
            return configuration;
        }




        static void CreateServiceLocator(this IContainer rootContainer)
        {
            ObjectFactory.SetContainer(new AutofacAdapter(rootContainer));

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(rootContainer));
        }

    }
}
