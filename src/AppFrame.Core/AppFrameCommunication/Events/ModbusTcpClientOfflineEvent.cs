using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class ModbusTcpClientOfflineEvent : AppEventBase<ModbusTcpClientOfflineEventArgs>, IAppEvent<ModbusTcpClientOfflineEventArgs>
    {
    }
    public class ModbusTcpClientOfflineEventArgs : AppEventArgsBase<ModbusTcpClientConnectDto>
    {
        public ModbusTcpClientOfflineEventArgs(ModbusTcpClientConnectDto dto)
        {
            Value = dto;
        }
    }
}
