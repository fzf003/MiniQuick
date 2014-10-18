
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;
using Microsoft.Practices.ServiceLocation;
using MiniQuick.Infrastructure.IOC;

namespace MiniQuick.Autofac.Extensions
{
    public static class AutofacServiceLocatorExtensions
    {
        public static void CreateServiceLocator(this IContainer rootContainer)
        {
            ObjectFactory.SetContainer(new AutofacAdapter(rootContainer));

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(rootContainer));
        }

    }
}
