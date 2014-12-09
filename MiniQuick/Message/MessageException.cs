using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Message
{
    public class MessageException : Exception
    {
        public MessageException() : base() { }
        public MessageException(string message) : base(message) { }

        public MessageException(string message, Exception innerException) : base(message, innerException) { }

        public MessageException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
