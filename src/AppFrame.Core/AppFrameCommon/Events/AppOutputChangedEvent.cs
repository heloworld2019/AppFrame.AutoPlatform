using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommon.Events
{
    public class AppOutputChangedEvent : AppEventBase<AppOutputChangedEventArgs>,IAppEvent<AppOutputChangedEventArgs>
    {
    }
}
