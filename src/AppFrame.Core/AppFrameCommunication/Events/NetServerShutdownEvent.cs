using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class NetServerShutdownEvent : AppEventBase<NetServerShutdownEventArgs>, IAppEvent<NetServerShutdownEventArgs>
    {

    }
    public class NetServerShutdownEventArgs : AppEventArgsBase<string>
    {

    }
}
