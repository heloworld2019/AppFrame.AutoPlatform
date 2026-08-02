using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class NetServerClientConnectingEvent : AppEventBase<NetServerClientConnectingEventArgs>, IAppEvent<NetServerClientConnectingEventArgs>
    {
    }
    public class NetServerClientConnectingEventArgs : AppEventArgsBase<AppNetClientSession>
    {
        public NetServerClientConnectingEventArgs(AppNetClientSession dto)
        {
            Value = dto;
        }
    }
}
