//using MiniQuick.Common;
//using MiniQuick.Message;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reactive.Concurrency;
//using System.Reactive.Disposables;
//using System.Reactive.Linq;
//using System.Reactive.Subjects;
//using System.Text;
//using System.Threading;

//namespace SimpleSample.MessageProcess
//{
//    public interface IEventLoop<T>
//    {
//        void Start();

//        void Stop();


//        void Post(T @event);
//    }


//    public class EventLoopWorker
//    {
//        private readonly List<Worker> _workerlist = new List<Worker>();
//        private int index = 0;
//        private BlockingCollection<IMessage> queue = new BlockingCollection<IMessage>();
//        public EventLoopWorker(Action actionhand, int workercount = 1)
//        {
//            for (int i = 0; i < workercount; i++)
//            {
//                this._workerlist.Add(new Worker(GetActionName(i), Execute));
//            }
//        }

//        private string GetActionName(int index)
//        {

//            return string.Format("{0}-Worker{1}", GetType().Name, index);
//        }

//        public void Start()
//        {
//            this._workerlist.ForEach(p => p.Start());
//        }

//        public void Stop()
//        {
//            this._workerlist.ForEach(p => p.Stop());
//        }


//        public void Process()
//        {
//            int next = Interlocked.Increment(ref index) % this._workerlist.Count;
            
           

//            this._workerlist[next].Post()
//        }

//        private void Execute()
//        {
//            IMessage message;
//            if ((message = queue.Take()) != null)
//            {
                
//            }
//        }

//        public void PostMessage(IMessage message)
//        {
//            queue.Add(message);
//        }



//    }
//}
