using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Message
{
    public class ActorMessageException : Exception
    {
        public ActorMessageException() : base() { }
        public ActorMessageException(string message) : base(message) { }

        public ActorMessageException(string message, Exception innerException) : base(message, innerException) { }

        public ActorMessageException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
