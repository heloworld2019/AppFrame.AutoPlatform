using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameEventBus
{
    public sealed class AppEventBus
    {
        private readonly ConcurrentDictionary<Type, IAppEventBase> _events = new ConcurrentDictionary<Type, IAppEventBase>();
        private readonly static object _lock = new object();
        private static AppEventBus _instance;
        private AppEventBus()
        {

        }
        public static AppEventBus Instance => Create();
        private static AppEventBus Create()
        {
            lock (_lock)
            {
               return _instance ?? (_instance = new AppEventBus());
            }
        }
        /// <summary>
        /// 获取事件
        /// </summary>
        /// <typeparam name="TEvent">事件模型</typeparam>
        /// <returns></returns>
        public TEvent GetEvent<TEvent>() where TEvent : IAppEventBase
        {
            if (_events.ContainsKey(typeof(TEvent)))
            {
                return (TEvent)_events[typeof(TEvent)];
            }
            else
            {
                return default;
            }
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="TEvent">事件模型</typeparam>
        /// <typeparam name="TEventArgs">事件参数模型</typeparam>
        /// <param name="callback">事件回调</param>
        public void Subscribe<TEvent, TEventArgs>(Action<object, TEventArgs> callback) where TEvent : IAppEventBase, new() where TEventArgs : EventArgs
        {
            ((IAppEvent<TEventArgs>)GetEvent<TEvent>())?.UnSubscribe(callback);
            ((IAppEvent<TEventArgs>)GetEvent<TEvent>())?.Subscribe(callback);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="callback"></param>
        public void UnSubscribe<TEvent, TEventArgs>(Action<object, TEventArgs> callback) where TEvent : IAppEventBase, new() where TEventArgs : EventArgs
        {
            ((IAppEvent<TEventArgs>)GetEvent<TEvent>())?.UnSubscribe(callback);
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件模型</typeparam>
        /// <typeparam name="TEventArgs">事件参数模型</typeparam>
        /// <param name="sender">发布者</param>
        /// <param name="args">事件参数</param>
        public void Publish<TEvent, TEventArgs>(object sender, TEventArgs args) where TEvent : IAppEventBase, new() where TEventArgs : EventArgs
        {
            ((IAppEvent<TEventArgs>)GetEvent<TEvent>())?.Publish(sender, args);
        }
    }
}
