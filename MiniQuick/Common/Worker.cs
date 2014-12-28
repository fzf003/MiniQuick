using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniQuick.Common
{
    public interface IWorker
    {
        string WorkerName { get; }
        Worker Start();
        void Stop();
    }

    public class Worker : IWorker
    {
        private readonly string _actionName;
        private readonly Action _action;
        private CancellationTokenSource _cts = null;

        public string WorkerName
        {
            get { return _actionName; }
        }


        public Worker(string workerName, Action action)
        {
            _actionName = workerName;
            _action = action;
        }

        public Worker Start()
        {
            _cts = new CancellationTokenSource();

            Task.Factory.StartNew((state) =>
            {
                CancellationToken token = (CancellationToken)state;
                while (!token.IsCancellationRequested)
                {
                    this._action();
                }
            }, _cts.Token, TaskCreationOptions.LongRunning);

            return this;
        }



        public void Stop()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

    }
}
