using AppFrame.Core.AppFrameCommunication.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Interfaces
{
    public interface IModbusTcpClient : IModbusClientBase
    {
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        bool Connect();
        /// <summary>
        /// 断开连接
        /// </summary>
        void DisConnect();
        /// <summary>
        /// 是否连接 (true为已连接)
        /// </summary>
        bool IsConnected { get; }
    }
}
