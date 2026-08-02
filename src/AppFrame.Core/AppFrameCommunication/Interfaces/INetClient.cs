using AppFrame.Core.AppFrameCommunication.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Interfaces
{
    public interface INetClient : IDisposable
    {
        bool UseReconnection { get; set; }
        bool IsConnected { get; }
        bool Connect();
        void DisConnect();
        void SendData(byte[] data);
        void SendData(byte[] data, int offset, int length);
        void SendData(string data);
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="onDataRecived"></param>
        /// <param name="onConnected"></param>
        /// <param name="onDisconnected"></param>
        void SubscribeEvent(Action<object, NetClientDataRecivedEventArgs> onDataRecived = null,
                            Action<object, NetClientConnectedEventArgs> onConnected = null,
                            Action<object, NetClientDisconnectedEventArgs> onDisconnected = null);
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="onDataRecived"></param>
        /// <param name="onConnected"></param>
        /// <param name="onDisconnected"></param>
        void UnSubscribeEvent(Action<object, NetClientDataRecivedEventArgs> onDataRecived = null,
                              Action<object, NetClientConnectedEventArgs> onConnected = null,
                              Action<object, NetClientDisconnectedEventArgs> onDisconnected = null);
    }
}
