using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class NetServerClientOfflineEvent :AppEventBase<NetServerClientOfflineEventArgs>, IAppEvent<NetServerClientOfflineEventArgs>
    {
    }
    public class NetServerClientOfflineEventArgs : AppEventArgsBase<NetServerClientOfflineDto>
    {
        public NetServerClientOfflineEventArgs(NetServerClientOfflineDto dto)
        {
            Value = dto;
        }
    }
}
