using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class NetServerClientOnlineEvent : AppEventBase<NetServerClientOnlineEventArgs>, IAppEvent<NetServerClientOnlineEventArgs>
    {
    }
    public class NetServerClientOnlineEventArgs : AppEventArgsBase<AppNetClientSession>
    {
        public NetServerClientOnlineEventArgs(AppNetClientSession dto)
        {
            Value = dto;
        }
    }
}
