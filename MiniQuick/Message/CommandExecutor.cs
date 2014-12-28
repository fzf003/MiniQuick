using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniQuick.Message
{
    public interface ICommandExecutor
    {
        void Execute();
    }
    public class CommandExecutor:ICommandExecutor
    {
        private IActorContext _context;
        public CommandExecutor(IActorContext context)
        {
            this._context = context;
        }
        public void Execute()
        {
            bool flag = false;
            try
            {
                ((dynamic)_context.Actor).Handle((dynamic)_context.Message);
                flag = true;
            }
            catch(Exception ex)
            {
                OnError((BaseCommand)this._context.Message,ex);
                this._context.OnException(ex);
            }
            finally
            {
                if(flag)
                {
                    OnCompleted((BaseCommand)this._context.Message);
                    this._context.OnCompleted();
                }
            }
        }

        protected void OnCompleted(BaseCommand command)
        {
            command.OnCompleted();
        }

        protected void OnError(BaseCommand command,Exception ex)
        {
            command.OnError(ex);
        }

    }
}
