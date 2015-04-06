using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MiniQuick.Events;
using MiniQuick.Infrastructure.IOC;
using MiniQuick.MessageBus.EventBus;

namespace MiniQuick.Domain
{
    [Serializable]
    public abstract class AggregateRoot<TId> : IAggregateRoot<TId>
    {

        private TId _id;

        protected AggregateRoot(TId key)
        {
            this._id = key;
        }

        private IEventBus EventBus
        {
            get
            {

                return ObjectFactory.Current.Resolve<IEventBus>();
            }
        }

        public virtual TId Key
        {
            get
            {
                return this._id;
            }
            protected set
            {
                this._id = value;
            }
        }


        public virtual void PublishEvent<TEvent>(TEvent @event) where TEvent : IEvent
        {
            EventBus.PublishAsync(@event);
        }

        public virtual void PublishAndApplyEvent<TEvent>(TEvent @event) where TEvent : IEvent
        {
            EventBus.PublishAsync(@event);
            ApplyEventOnAggregate(@event);
        }

        private void ApplyEventOnAggregate<TEvent>(TEvent @event) where TEvent : IEvent
        {
            try
            {
                var method = GetType().GetMethod("ApplyChange", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(TEvent) }, null);
                method.Invoke(this, new[] { @event as Object });
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(" 聚合根转换失败！{0}", typeof(TEvent)), ex);
            }
        }
    }
}
