using MiniQuick.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniQuick.Infrastructure
{
    public class LoopProcessor<T> : ILoopProcessor<T>
    {
        private readonly List<WorkerProcess> _workerlist = new List<WorkerProcess>();

        private int index = 0;

        private BlockingCollection<T> queue = new BlockingCollection<T>();
        public LoopProcessor(Action<T> action, int workercount = 1)
        {
            for (int i = 0; i < workercount; i++)
            {
                this._workerlist.Add(new WorkerProcess(GetActionName(i), action));
            }
        }

        private string GetActionName(int index)
        {
            return string.Format("{0}-Worker{1}", GetType().Name, index);
        }

        public void Start()
        {
            this._workerlist.ForEach(p => p.Start());
        }

        public void Stop()
        {
            this._workerlist.ForEach(p => p.Stop());
        }


        public void PostMessage(T message)
        {
            int next = Interlocked.Increment(ref index) % this._workerlist.Count;

            this._workerlist[next].PostMessage(message);
        }

        class WorkerProcess
        {
            private readonly BlockingCollection<T> _queue = new BlockingCollection<T>();
            private readonly Worker _worker;
            public WorkerProcess(string actionName, Action<T> action)
            {
                _queue = new BlockingCollection<T>();
                _worker = new Worker(actionName, () => action(_queue.Take()));
            }
            public void PostMessage(T message)
            {
                _queue.Add(message);
            }
            public void Start()
            {
                _worker.Start();
            }
            public void Stop()
            {
                _worker.Stop();
            }
        }


    }
}
