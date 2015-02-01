using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Domain
{
    /// <summary>
    /// 聚合对像
    /// </summary>
    public interface IAggregateRoot<TId> 
    {
        TId Key { get; }
    }
}
