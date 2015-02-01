using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.Infrastructure
{
    public interface ILoopProcessor<T>
    {
        void Start();

        void Stop();


        void PostMessage(T message);
    }
}
