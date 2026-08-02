using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommon.Global.Events
{
    /// <summary>
    /// 释放资源事件,系统关闭前将触发此事件,需要释放资源的对象可以订阅此事件
    /// </summary>
    public class AppDisposeEvent : AppEventBase<AppDisposeEventArgs>, IAppEvent<AppDisposeEventArgs>
    {

    }
    public class AppDisposeEventArgs : AppEventArgsBase<bool>
    {

    }
}
