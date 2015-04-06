using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniQuick.Common;
namespace MiniQuick.Events
{
    public abstract class EventHandler : IDisposable, IObserver<IEvent>
    {
        public virtual void Dispose()
        {

        }

        public virtual void OnCompleted()
        {

        }

        public virtual void OnError(Exception error)
        {
            PrintError(error.InnerException.Message);
        }

        public void OnNext(IEvent value)
        {
            Process(value);
        }

        private void Process(IEvent value)
        {
            try
            {

                new Action(() =>
                {
                    ((dynamic)this).Handle((dynamic)value);

                }).Retry(value.Timeinterval,value.RetryCount);

            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        void PrintError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

    }
}
