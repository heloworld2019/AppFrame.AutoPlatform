using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class ModbusTcpClientOnlineEvent :AppEventBase<ModbusTcpClientOnlineEventArgs>,IAppEvent<ModbusTcpClientOnlineEventArgs>
    {
    }
    public class ModbusTcpClientOnlineEventArgs :AppEventArgsBase<ModbusTcpClientConnectDto>
    {
        public ModbusTcpClientOnlineEventArgs(ModbusTcpClientConnectDto dto)
        {
            Value = dto;
        }
    }
}
