﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Commands
{
    public class CommandResult
    {

        public CommandStatus Status { get;  set; }
      
        public Exception ErrorMessage { get;  set; }
        public CommandResult(CommandStatus status):this(status,null)
        {
            Status = status;
          
        }

        public CommandResult(CommandStatus status, Exception errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }
    }


    public enum CommandStatus
    {
        Success = 1,
        Failed,
    }
}
