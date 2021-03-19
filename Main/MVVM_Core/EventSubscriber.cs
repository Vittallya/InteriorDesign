using System;
using System.Collections.Generic;
using System.Text;

namespace Main.MVVM_Core
{
    public class EventSubscriber : IDisposable
    {

        private readonly Action<EventSubscriber> action;

        public Type MesType { get; }
        public Type Sub { get; }

        public bool IsOnce { get; set; }
        public EventSubscriber(Type mesType, Type sub, Action<EventSubscriber> action, bool isOnce)
        {
            this.MesType = mesType;
            Sub = sub;
            this.action = action;
            IsOnce = isOnce;
        }
        public void Dispose()
        {
            action?.Invoke(this);
        }
    }
}
