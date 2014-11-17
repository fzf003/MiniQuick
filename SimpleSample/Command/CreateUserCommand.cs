using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSample.Command
{
    public interface ICommand:IMessage
    {

    }
    public class CreateUserCommand:ICommand
    {
        public string Name { get; set; }
        public string MessageId { get; set; }
        public Result ResultStatus { get; set; }
        public CreateUserCommand()
        {
            this.ResultStatus = new Result();
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
    }
}
