using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.MessageBus.EventBus
{
    public class BaseEvent : IEvent
    {
        protected TaskCompletionSource<EventResult> _tcs;
        public BaseEvent()
        {
            this._tcs = new TaskCompletionSource<EventResult>();
        }
        public string CommandId
        {
            get;
            set;
        }

        public Task<EventResult> Completion
        {
            get
            {
                return this._tcs.Task;
            }
        }

        public void OnError(Exception ex)
        {
            this._tcs.SetResult(new EventResult(EventStatus.Failed, ex));
        }

        public void OnCompleted()
        {
            this._tcs.SetResult(new EventResult(EventStatus.Success, null));
        }
    }
}
