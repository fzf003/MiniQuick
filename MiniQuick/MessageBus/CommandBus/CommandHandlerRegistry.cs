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

        public ConcurrentDictionary<Type, object> Subjects
        {
            get
            {
                return this._subjects;
            }
        }
   }
}
