using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniQuick.MessageBus.CommandBus
{
    internal sealed class SendCommandRequest<T>
    {
        public T Payload { get; private set; }
        public CancellationToken CancellationToken { get; private set; }
        public Action<bool> OnSendComplete { get; private set; }

        public SendCommandRequest(T payload, CancellationToken cancellationToken)
            : this(payload, cancellationToken, success => { }) { }

        public SendCommandRequest(T payload, CancellationToken cancellationToken, Action<bool> onSendComplete)
        {
            Payload = payload;
            CancellationToken = cancellationToken;
            OnSendComplete = onSendComplete;
        }
    }
}
