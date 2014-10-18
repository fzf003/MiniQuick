using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Infrastructure.Log
{
    public interface ILoggerFactory
    {

        ILogger Create(string name);

        ILogger Create(Type type);
    }
}
