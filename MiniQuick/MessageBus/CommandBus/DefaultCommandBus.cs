﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace MiniQuick.MessageBus.CommandBus
{


    public class DefaultCommandBus: AbstractCommandBus, ICommandBus
    {
        public DefaultCommandBus()
            : base(CommandHandlerRegistry.Instance)
        {

        }


     
    }
}
