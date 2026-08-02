using AppFrame.Core.AppFrameCommunication.Events;
using AppFrame.Core.AppFrameCommunication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Interfaces
{
    public interface INetServer : IDisposable
    {
        /// <summary>
        /// 所有在线的客户端
        /// </summary>
        List<AppNetClientSession> OnlineClients { get; }
        /// <summary>
        /// 服务器是否启动
        /// </summary>
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
        /// 断开指定的客户端
        /// </summary>
        /// <param name="clientId"></param>
        void DisConnectAssignClient(AppNetClientSession client);
        /// <summary>
        /// 向指定的客户端发送消息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="appNetServerSendParameter"></param>
        /// <returns></returns>
        void SendData(AppNetClientSession client, byte[] data);
        void SendData(AppNetClientSession client, byte[] data, int offset, int length);
        void SendData(AppNetClientSession client, string data);
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="onDataRecived"></param>
        /// <param name="onDataSend"></param>
        /// <param name="onDataMonitor"></param>
        /// <param name="onClientOnline"></param>
        /// <param name="onClientOffline"></param>
        void SubscribeEvent(Action<object, NetServerDataRevicedEventArgs> onDataRecived = null,
                            Action<object, NetServerShutdownEventArgs> onShutdown = null,
                            Action<object, NetServerClientConnectingEventArgs> onConnecting = null,
                            Action<object, NetServerClientOnlineEventArgs> onClientOnline = null,
                            Action<object, NetServerClientOfflineEventArgs> onClientOffline = null);
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="onDataRecived"></param>
        /// <param name="onDataSend"></param>
        /// <param name="onDataMonitor"></param>
        /// <param name="onClientOnline"></param>
        /// <param name="onClientOffline"></param>
        void UnSubscribeEvent(Action<object, NetServerDataRevicedEventArgs> onDataRecived = null,
                            Action<object, NetServerShutdownEventArgs> onShutdown = null,
                            Action<object, NetServerClientConnectingEventArgs> onConnecting = null,
                            Action<object, NetServerClientOnlineEventArgs> onClientOnline = null,
                            Action<object, NetServerClientOfflineEventArgs> onClientOffline = null);
    }
}
