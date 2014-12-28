using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.Message
{
    public class BaseCommand : ICommand
    {
        protected TaskCompletionSource<bool> _task = new TaskCompletionSource<bool>();
        public BaseCommand()
        {

        }
        public string CommandId
        {
            get;
            set;
        }

        public Task Completion
        {
            get { return _task.Task; }
        }

        public void OnError(Exception ex)
        {
            this._task.SetException(ex);
        }

        public void OnCompleted()
        {
            this._task.SetResult(true);
        }
    }
}
