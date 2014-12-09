using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;

namespace MiniQuick.Process
{
    public interface IMessageProcessService<T>
    {
        /// <summary>
        /// 开始并处理
        /// </summary>
        /// <param name="query"></param>
        void Process(Action<IObservable<T>, HistoricalScheduler> query);
    }
}
