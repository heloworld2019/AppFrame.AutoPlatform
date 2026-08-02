using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameEventBus
{
    /// <summary>
    /// 事件参数 要实现的抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AppEventArgsBase<T> : EventArgs
    {
        public T Value { get; set; }
    }
}
