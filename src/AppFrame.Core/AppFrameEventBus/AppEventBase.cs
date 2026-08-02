using AppFrame.Common.Extensions.ConcurrentBag;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameEventBus
{
    /// <summary>
    /// 事件模型的基类实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AppEventBase<T> : IAppEvent<T> where T : EventArgs
    {
        //缓存所有注册的事件集合
        protected readonly ConcurrentBag<Action<object, T>> _events = new ConcurrentBag<Action<object, T>>();
        public void Publish(object sender, T args)
        {
            foreach (var item in _events)
            {
                item.Invoke(sender, args);
            }
        }

        public void Subscribe(Action<object, T> eventHandler)
        {
            if (!_events.Contains(eventHandler))
            {
                _events.Add(eventHandler);
            }
        }

        public void UnSubscribe(Action<object, T> eventHandler)
        {
            if (_events.Contains(eventHandler))
            {
                _events.Remove(eventHandler);
            }
        }
    }
}
