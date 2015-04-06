using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.MessageBus.CommandBus
{
   public  class CommandHandlerRegistry
    {
        private static volatile CommandHandlerRegistry _Instance;

        private static object objlock = new object();

        private readonly ConcurrentDictionary<Type, object> _subjects = new ConcurrentDictionary<Type, object>();
        private CommandHandlerRegistry() { }
 
        public static CommandHandlerRegistry Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (objlock)
                    {
                        if (_Instance == null)
                            _Instance = new CommandHandlerRegistry();
                    }
                }
 
                return _Instance;
            }
        }

        public void AddCommandHandler(Type type, object commandhandlerobj)
        {
            object obj = null;
           if(!this._subjects.TryGetValue(type,out obj))
           {

               this._subjects.TryAdd(type, commandhandlerobj);
           }
           
          
        }


       public object GetCommandHandler(Type type)
       {
          object obj = null;
          this._subjects.TryGetValue(type, out obj);
          return obj;
       }

       public bool RemoveCommandHandler(Type type)
       {
           object obj = null;
           return this._subjects.TryRemove(type, out obj);
       }

       public ConcurrentDictionary<Type, object> Subjects
        {
            get
            {
                return this._subjects;
            }
        }
   }
}
