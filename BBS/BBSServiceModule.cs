using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace BBS
{
    public class BBSServiceModule:Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<BBSService>().AsSelf().SingleInstance();
        }
    }
}
