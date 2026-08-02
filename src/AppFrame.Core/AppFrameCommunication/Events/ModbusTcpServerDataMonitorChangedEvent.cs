using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class ModbusTcpServerDataMonitorChangedEvent : AppEventBase<ModbusTcpServerDataMonitorChangedEventArgs>, IAppEvent<ModbusTcpServerDataMonitorChangedEventArgs>
    {

    }
    public class ModbusTcpServerDataMonitorChangedEventArgs : AppEventArgsBase<ModbusTcpServerDataMonitorDto>
    {
        public ModbusTcpServerDataMonitorChangedEventArgs(ModbusTcpServerDataMonitorDto dto)
        {
            Value = dto;
        }
    }
}
