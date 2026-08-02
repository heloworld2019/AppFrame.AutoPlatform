using AppFrame.Common.Extensions.ValidateAttribute;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameCommon.Global.Events;
using AppFrame.Core.AppFrameCommunication.Events;
using AppFrame.Core.AppFrameCommunication.Interfaces;
using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameException.CommunicationException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TouchSocket.Core;
using TouchSocket.Core.ByteManager;
using TouchSocket.Core.Config;
using TouchSocket.Sockets;

namespace AppFrame.Infrastructrue.AppFrameCommunication
{
    internal class NetServer : GlobalObjectManager, INetServer
    {
        private static readonly object _locker = new object();
        private readonly AppNetServerElement _appNetServerElement;
        private static NetServer _instance;
        private TcpService _service; 
        private NetServer(AppNetServerElement appNetServerElement)
        {
            if(appNetServerElement == null)
            {
                throw new ArgumentNullException(nameof(appNetServerElement));
            }
            if (!string.IsNullOrWhiteSpace(appNetServerElement.Validate()))
            {
                throw new NetConfigException();
            }
            _appNetServerElement = appNetServerElement;
            OnlineClients = new List<AppNetClientSession>();
            _service = new TcpService();
            EventBus.Subscribe<AppDisposeEvent, AppDisposeEventArgs>(DisposeCallback);
        }
        public static NetServer Create(AppNetServerElement appNetServerElement)
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new NetServer(appNetServerElement));
            }
        }
        private void DisposeCallback(object arg1, AppDisposeEventArgs arg2)
        {
            Dispose();
        }

        public List<AppNetClientSession> OnlineClients { get; private set; }

        public bool IsStarted { get; private set; }

        public bool StartService()
        {
            IsStarted = false;
            var host = new StringBuilder();
            host.Append(_appNetServerElement.IpAddress);
            host.Append(":");
            host.Append(_appNetServerElement.Port);
            _service.Connecting -= ClientConnecting;
            _service.Connected -= ClientConnected;
            _service.Disconnected -= ClientDisconnected;
            _service.Received -= DataReceived;

            _service.Connecting += ClientConnecting;
            _service.Connected += ClientConnected;
            _service.Disconnected += ClientDisconnected;
            _service.Received += DataReceived;

            _service.Setup(new TouchSocketConfig()//载入配置
                    .SetListenIPHosts(new IPHost[] { new IPHost(host.ToString()) })//同时监听两个地址
                    .SetMaxCount(10000)
                    .SetThreadCount(10))
                    .Start();
            IsStarted = true;
            return IsStarted;
        }
        private void ClientConnecting(SocketClient client, ClientOperationEventArgs e)
        {
            EventBus.Publish<NetServerClientConnectingEvent, NetServerClientConnectingEventArgs>
                     (this, new NetServerClientConnectingEventArgs(new AppNetClientSession(client.ID, client.IP, client.Port)));
        }
        private void ClientConnected(SocketClient client, TouchSocketEventArgs e)
        {
            lock (_locker)
            {
                var model = OnlineClients.FirstOrDefault(p => p.ClientId.Equals(client.ID) && p.ClientIpAddress.Equals(client.IP));
                if (model == null)
                {
                    OnlineClients.Add(new AppNetClientSession(client.ID, client.IP, client.Port));
                }
            } 
            EventBus.Publish<NetServerClientOnlineEvent, NetServerClientOnlineEventArgs>
                     (this, new NetServerClientOnlineEventArgs(new AppNetClientSession(client.ID, client.IP, client.Port)));
        }
        private void ClientDisconnected(SocketClient client, ClientDisconnectedEventArgs e)
        {
            lock (_locker)
            {
                var model = OnlineClients.FirstOrDefault(p => p.ClientId.Equals(client.ID) && p.ClientIpAddress.Equals(client.IP));
                if (model != null)
                {
                    OnlineClients.Remove(model);
                }
            }
            EventBus.Publish<NetServerClientOfflineEvent, NetServerClientOfflineEventArgs>
                     (this, new NetServerClientOfflineEventArgs(new NetServerClientOfflineDto(client.ID, client.IP, client.Port, e.Manual, e.Message)));
        }
        private void DataReceived(SocketClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
        {
            EventBus.Publish<NetServerDataRevicedEvent, NetServerDataRevicedEventArgs>(this, new NetServerDataRevicedEventArgs
                           (new NetServerDataReviceDto(client.ID, client.IP, client.Port, byteBlock.ToArray(), Encoding.UTF8.GetString(byteBlock.ToArray()))));
        }
        public void StopService()
        {
            _service.Stop();
            IsStarted = false;
            EventBus.Publish<NetServerShutdownEvent, NetServerShutdownEventArgs>(this, new NetServerShutdownEventArgs());
        }
        public void Dispose()
        {
            IsStarted = false;
            OnlineClients = null;
            _service.SafeDispose();
            _service = null;
            _instance = null;
        }

        public void SendData(AppNetClientSession client, byte[] data)
        {
            _service.Send(client.ClientId, data);
        }

        public void SendData(AppNetClientSession client, byte[] data, int offset, int length)
        {
            _service.Send(client.ClientId, data, offset, length);
        }

        public void SendData(AppNetClientSession client, string data)
        {
            _service.Send(client.ClientId, data);
        }
        public void DisConnectAssignClient(AppNetClientSession client)
        {
            if (client == null) return;
            if (string.IsNullOrWhiteSpace(client.ClientId)) return;
            if (_service.TryGetSocketClient(client.ClientId, out SocketClient outClient))
            {
                outClient.SafeDispose();
                lock (_locker)
                {
                    var model = OnlineClients.FirstOrDefault(x => x.ClientId.Equals(outClient.ID) && x.ClientIpAddress.Equals(outClient.IP));
                    if (model != null)
                        OnlineClients.Remove(model);
                }
            }
        }
        public void SubscribeEvent(Action<object, NetServerDataRevicedEventArgs> onDataRecived = null, 
                                   Action<object, NetServerShutdownEventArgs> onShutdown = null, 
                                   Action<object, NetServerClientConnectingEventArgs> onConnecting = null, 
                                   Action<object, NetServerClientOnlineEventArgs> onClientOnline = null, 
                                   Action<object, NetServerClientOfflineEventArgs> onClientOffline = null)
        {
            if(onDataRecived != null)
            {
                EventBus.Subscribe<NetServerDataRevicedEvent, NetServerDataRevicedEventArgs>(onDataRecived);
            }
            if (onShutdown != null)
            {
                EventBus.Subscribe<NetServerShutdownEvent, NetServerShutdownEventArgs>(onShutdown);
            }
            if (onConnecting != null)
            {
                EventBus.Subscribe<NetServerClientConnectingEvent, NetServerClientConnectingEventArgs>(onConnecting);
            }
            if (onClientOnline != null)
            {
                EventBus.Subscribe<NetServerClientOnlineEvent, NetServerClientOnlineEventArgs>(onClientOnline);
            }
            if (onClientOffline != null)
            {
                EventBus.Subscribe<NetServerClientOfflineEvent, NetServerClientOfflineEventArgs>(onClientOffline);
            }
        }

        public void UnSubscribeEvent(Action<object, NetServerDataRevicedEventArgs> onDataRecived = null, 
                                     Action<object, NetServerShutdownEventArgs> onShutdown = null, 
                                     Action<object, NetServerClientConnectingEventArgs> onConnecting = null, 
                                     Action<object, NetServerClientOnlineEventArgs> onClientOnline = null, 
                                     Action<object, NetServerClientOfflineEventArgs> onClientOffline = null)
        {
            if (onDataRecived != null)
            {
                EventBus.UnSubscribe<NetServerDataRevicedEvent, NetServerDataRevicedEventArgs>(onDataRecived);
            }
            if (onShutdown != null)
            {
                EventBus.UnSubscribe<NetServerShutdownEvent, NetServerShutdownEventArgs>(onShutdown);
            }
            if (onConnecting != null)
            {
                EventBus.UnSubscribe<NetServerClientConnectingEvent, NetServerClientConnectingEventArgs>(onConnecting);
            }
            if (onClientOnline != null)
            {
                EventBus.UnSubscribe<NetServerClientOnlineEvent, NetServerClientOnlineEventArgs>(onClientOnline);
            }
            if (onClientOffline != null)
            {
                EventBus.UnSubscribe<NetServerClientOfflineEvent, NetServerClientOfflineEventArgs>(onClientOffline);
            }
        }
    }
}
