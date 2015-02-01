using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.MessageBus.CommandBus
{
   
    public class BaseCommand : ICommand
    {
        protected TaskCompletionSource<CommandResult> _tcs;

        private  int MaxRetryCount = 3;

        private TimeSpan _timeinterval = TimeSpan.Zero;
        public BaseCommand()
        {
            this._tcs = new TaskCompletionSource<CommandResult>();
        }
        public string CommandId
        {
            get;
            set;
        }

        public Task<CommandResult> Completion
        {
            get
            {
                return this._tcs.Task;
            }
        }

        public void OnError(Exception ex)
        {
            this._tcs.SetResult(new CommandResult(CommandStatus.Failed,ex));
        }

        public void OnCompleted()
        {
            this._tcs.SetResult(new CommandResult(CommandStatus.Success,null));
        }


        public int RetryCount
        {
            get { return this.MaxRetryCount; }
        }


        TimeSpan ICommand.Timeinterval
        {
            get { return this._timeinterval; }
        }
    }
}
