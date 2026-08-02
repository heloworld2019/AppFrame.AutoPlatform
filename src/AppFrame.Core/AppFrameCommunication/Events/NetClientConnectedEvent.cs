using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Events
{
    public class NetClientConnectedEvent : AppEventBase<NetClientConnectedEventArgs>, IAppEvent<NetClientConnectedEventArgs>
    {
    }
    public class NetClientConnectedEventArgs : AppEventArgsBase<string>
    {
        public NetClientConnectedEventArgs(string msg)
        {
            Value = msg;
        }
    }
}
