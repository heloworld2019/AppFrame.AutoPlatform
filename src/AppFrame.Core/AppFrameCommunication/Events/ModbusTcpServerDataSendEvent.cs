using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class ModbusTcpServerDataSendEvent :AppEventBase<ModbusTcpServerDataSendEventArgs>,IAppEvent<ModbusTcpServerDataSendEventArgs>
    {
    }
    public class ModbusTcpServerDataSendEventArgs : AppEventArgsBase<ModbusTcpServerDataSendDto>
    {
        public ModbusTcpServerDataSendEventArgs(ModbusTcpServerDataSendDto dto)
        {
            Value = dto;
        }
    }
}
