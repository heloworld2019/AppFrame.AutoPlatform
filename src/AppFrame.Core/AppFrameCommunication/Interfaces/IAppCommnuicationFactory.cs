using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Interfaces
{
    /// <summary>
    /// 系统通讯工厂接口
    /// </summary>
    public interface IAppCommnuicationFactory
    {
        IModbusRtuClient CreateModbusRtuClient(AppModbusRtuElement appModbusRtuElement);
        IModbusTcpClient CreateModbusTcpClient(AppModbusTcpClientElement appModbusTcpClientElement);
        IModbusTcpServer BuildModbusTcpServer(AppModbusTcpServerElement appModbusTcpServerElement);
        INetClient CreateNetClient(AppNetClientElement appNetClientElement);
        INetServer BuildNetServer(AppNetServerElement appNetServerElement);
    }
}
