using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class ModbusTcpServerDataRecivedEvent: AppEventBase<ModbusTcpServerDataRecivedEventArgs>,IAppEvent<ModbusTcpServerDataRecivedEventArgs>
    {
    }
    public class ModbusTcpServerDataRecivedEventArgs : AppEventArgsBase<ModbusTcpServerDataReciveDto>
    {
        public ModbusTcpServerDataRecivedEventArgs(ModbusTcpServerDataReciveDto dto)
        {
            Value = dto;
        }
    }
}
