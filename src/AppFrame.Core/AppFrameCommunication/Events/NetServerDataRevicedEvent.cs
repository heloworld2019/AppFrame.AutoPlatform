using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class NetServerDataRevicedEvent : AppEventBase<NetServerDataRevicedEventArgs>, IAppEvent<NetServerDataRevicedEventArgs>
    {
    }
    public class NetServerDataRevicedEventArgs : AppEventArgsBase<NetServerDataReviceDto>
    {
        public NetServerDataRevicedEventArgs(NetServerDataReviceDto dto)
        {
            Value = dto;
        }
    }
}
