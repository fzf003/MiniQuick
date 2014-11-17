using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.MessageBus.EventBus
{
     
    public class EventHandlerRegistry
    {
        private static volatile EventHandlerRegistry _Instance;

        private static object objlock = new object();

        private readonly ConcurrentDictionary<Type, object> _subjects = new ConcurrentDictionary<Type, object>();
        private EventHandlerRegistry() { }

        public static EventHandlerRegistry Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (objlock)
                    {
                        if (_Instance == null)
                            _Instance = new EventHandlerRegistry();
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
