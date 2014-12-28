using MiniQuick.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSample.Command
{
   
    
    public class CreateUserCommand:BaseCommand
    {
        public string Name { get; set; }
      //  public Task<CommandResult> ResultStatus { get; set; }
        public CreateUserCommand()
        {
            
        }

        //public Action CallBack { get; set; }


     


        public Action ResultCallback
        {
            get;
            set;
        }






    }

   
}
