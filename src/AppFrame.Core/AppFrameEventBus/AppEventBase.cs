using AppFrame.Common.Extensions.ConcurrentBag;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameEventBus
{



//    ✅ 能做：
//保存一类事件的全部订阅回调委托
//新增订阅、移除订阅
//遍历所有订阅者，执行回调分发事件消息（AppEventArgsBase 子类实例）
//❌ 不能做
//不做全局事件管理（全局管理交给 AppEventBus）
//不知道其他类型事件的管理器实例
//不创建事件参数对象（事件参数 args 由发布方 ProductService 构造）
//不处理 UI 线程切换（回调在发布线程执行，需要 ViewModel 自己处理 Dispatcher）





    /// <summary>
    /// 事件模型的基类实现
    /// </summary>
    /// <typeparam name="T"></typeparam>   管理某种类型的事件
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
