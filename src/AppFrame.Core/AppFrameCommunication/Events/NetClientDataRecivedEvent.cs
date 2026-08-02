using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class NetClientDataRecivedEvent : AppEventBase<NetClientDataRecivedEventArgs>, IAppEvent<NetClientDataRecivedEventArgs>
    {
    }
    public class NetClientDataRecivedEventArgs : AppEventArgsBase<NetClientDataRecivedDto>
    {
        public NetClientDataRecivedEventArgs(NetClientDataRecivedDto dto)
        {
            Value = dto;
        }
    }
}
