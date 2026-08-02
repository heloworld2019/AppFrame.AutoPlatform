using AppFrame.Core.AppFrameCommunication.Events;
using AppFrame.Core.AppFrameCommunication.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Interfaces
{
    public interface IModbusTcpServer : IModbusBase
    {
        bool IsStarted { get; }
        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <returns></returns>
        bool StartService();
        /// <summary>
        /// 停止服务器
        /// </summary>
        void StopService();
        /// <summary>
        /// 过滤客户端
        /// </summary>
        /// <param name="IpAddresses">要过滤的客户端IP地址</param>
        void SetTrustedIpAddress(List<string> IpAddresses);
        /// <summary>
        /// 移除过滤
        /// </summary>
        void RemoveTrusted();
        /// <summary>
        /// 订阅数据监控
        /// </summary>
        /// <param name="address">要监控的地址</param>
        void SubcribeDataMonitor(string address);
        /// <summary>
        /// 取消数据监控
        /// </summary>
        void UnsubcribeDataMonitor();
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="onDataRecived"></param>
        /// <param name="onDataSend"></param>
        /// <param name="onDataMonitor"></param>
        /// <param name="onClientOnline"></param>
        /// <param name="onClientOffline"></param>
        void SubscribeEvent(Action<object, ModbusTcpServerDataRecivedEventArgs> onDataRecived=null,
                            Action<object, ModbusTcpServerDataSendEventArgs> onDataSend = null,
                            Action<object, ModbusTcpServerDataMonitorChangedEventArgs> onDataMonitor = null,
                            Action<object, ModbusTcpClientOnlineEventArgs> onClientOnline = null,
                            Action<object, ModbusTcpClientOfflineEventArgs> onClientOffline = null);
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="onDataRecived"></param>
        /// <param name="onDataSend"></param>
        /// <param name="onDataMonitor"></param>
        /// <param name="onClientOnline"></param>
        /// <param name="onClientOffline"></param>
        void UnSubscribeEvent(Action<object, ModbusTcpServerDataRecivedEventArgs> onDataRecived = null,
                              Action<object, ModbusTcpServerDataSendEventArgs> onDataSend = null,
                              Action<object, ModbusTcpServerDataMonitorChangedEventArgs> onDataMonitor = null,
                              Action<object, ModbusTcpClientOnlineEventArgs> onClientOnline = null,
                              Action<object, ModbusTcpClientOfflineEventArgs> onClientOffline = null);
    }
}
