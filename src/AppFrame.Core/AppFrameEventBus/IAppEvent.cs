using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameEventBus
{
    /// <summary>
    /// 事件模型 需要实现的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppEvent<T> : IAppEventBase where T : EventArgs
    {
        void Subscribe(Action<object, T> eventHandler);
        void UnSubscribe(Action<object, T> eventHandler);
        void Publish(object sender, T args);
    }
}
