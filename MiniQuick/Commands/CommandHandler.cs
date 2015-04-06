using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Threading.Tasks;
using MiniQuick.Common;
using MiniQuick.Infrastructure.IOC;


namespace MiniQuick.Commands
{
    public abstract class CommandHandler : IDisposable, IObserver<ICommand>
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

        public void OnNext(ICommand value)
        {
             Execute(value);
        }

        private void Execute(ICommand value)
        {
            try
            {
                new Action(() =>
                {
                    ((dynamic)this).Handle((dynamic)value);

                }).Retry(value.Timeinterval, value.RetryCount);

            }
            catch (Exception ex)
            {
                OnError(ex);
                throw ex;
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
