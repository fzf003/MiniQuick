using MiniQuick.Infrastructure.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MiniQuick
{
    /// <summary>
    /// 系统初始化配置程
    /// </summary>
    public class Configuration
    {
        private static Configuration _Instance = new Configuration();
        private static object objlock = new object();
        private static bool isruning = false;
        public static Configuration Instance { get; private set; }

        public bool IsRuning
        {
            get
            {
                return isruning;
            }
        }

        public static Configuration Create()
        {

            if (Instance == null)
            {
                lock (objlock)
                {
                    if (Instance == null)
                    {
                        Instance = _Instance;

                    }
                }
            }

            return Instance;

        }

       
  

        public Configuration RegisterParts<TService, TImplementer>(TImplementer instance)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectFactory.Current.RegisterInstance<TService, TImplementer>(instance);
            return this;
        }

        public Configuration RegisterParts<TService, TImplementer>()
            where TService : class
            where TImplementer : class, TService
        {
            ObjectFactory.Current.Register<TService, TImplementer>(LifeStyle.Singleton);
            return this;
        }



        public void Start()
        {
           isruning = true;
        }
    }

}
