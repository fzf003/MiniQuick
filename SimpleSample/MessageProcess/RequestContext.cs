using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimpleSample.MessageProcess
{
    public class  RequestContext
    {
        
        public CancellationToken CancellationToken { get; private set; }
        public Action<bool> OnSendComplete { get; private set; }

        public RequestContext(object payload, CancellationToken cancellationToken)
            : this( cancellationToken, success => { }) { }

        public RequestContext(CancellationToken cancellationToken, Action<bool> onSendComplete)
        {
            CancellationToken = cancellationToken;
            OnSendComplete = onSendComplete;
        }
    }
}
