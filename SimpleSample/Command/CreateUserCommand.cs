using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleSample.Command
{
    public class CreateUserCommand
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
