using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Channel
{
    public interface ChannelManager<T>
    {
        void RegisterChannel(IChannel<T> channel);

        IChannel<T> GetChannel(T message);
    }


   
}
