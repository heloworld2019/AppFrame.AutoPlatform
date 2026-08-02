using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class NetClientDisconnectedEvent : AppEventBase<NetClientDisconnectedEventArgs>, IAppEvent<NetClientDisconnectedEventArgs>
    {
    }
    public class NetClientDisconnectedEventArgs : AppEventArgsBase<NetClientDisconnectedDto>
    {
        public NetClientDisconnectedEventArgs(NetClientDisconnectedDto dto)
        {
            Value = dto;
        }
    }
}
