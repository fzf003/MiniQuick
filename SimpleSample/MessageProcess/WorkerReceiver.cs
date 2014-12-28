using MiniQuick.Common;
using MiniQuick.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSample.MessageProcess
{
    public class WorkerReceiver<T>
    {
        private  Worker _worker;

        private BlockingCollection<T> _queue = new BlockingCollection<T>();

        public WorkerReceiver(Worker worker)
        {
            _worker = worker;
        }

        public WorkerReceiver(Action<T> action)
        {
            this._worker = new Worker(typeof(T).FullName, () => {
                T message = default(T);
                if ((message = _queue.Take()) != null)
                {
                    action(message);
                }
            });
        }

        

        public void Post(T message)
        {
            _queue.Add(message);
        }

      
        public void Start()
        {
            this._worker.Start();
        }

        public void Stop()
        {
             _worker.Stop();
        }

    }
}
