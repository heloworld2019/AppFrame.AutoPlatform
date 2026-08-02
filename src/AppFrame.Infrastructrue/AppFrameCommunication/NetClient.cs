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
using System.Threading.Tasks;
using TouchSocket.Core.ByteManager;
using TouchSocket.Core.Config;
using TouchSocket.Core.Plugins;
using TouchSocket.Sockets;

namespace AppFrame.Infrastructrue.AppFrameCommunication
{
    internal class NetClient : GlobalObjectManager, INetClient
    {
        private readonly AppNetClientElement _appNetClientElement;
        private readonly TcpClient _client;
        public NetClient(AppNetClientElement appNetClientElement)
        {
            if(appNetClientElement == null)
            {
                throw new ArgumentNullException(nameof(appNetClientElement));
            }
            if (!string.IsNullOrWhiteSpace(appNetClientElement.Validate()))
            {
                throw new NetConfigException();
            }
            _appNetClientElement = appNetClientElement;
            _client = new TcpClient();
            UseReconnection = true;
            EventBus.Subscribe<AppDisposeEvent, AppDisposeEventArgs>(DisposeCallback);
        }

        private void DisposeCallback(object arg1, AppDisposeEventArgs arg2)
        {
            Dispose();
        }

        public bool IsConnected { get; private set; }
        public bool UseReconnection { get ; set ; }

        public bool Connect()
        {
            IsConnected = false;
            var host = new StringBuilder();
            host.Append(_appNetClientElement.IpAddress);
            host.Append(":");
            host.Append(_appNetClientElement.Port);
            //声明配置
            var config = new TouchSocketConfig();
            config.SetRemoteIPHost(new IPHost(host.ToString()))
                  .UsePlugin()
                  .SetBufferLength(1024 * 10)
                  .ConfigurePlugins(a =>
                  {
                      if (UseReconnection)
                      {
                          a.UseReconnection(5, true, 1000);
                      }
                  });
            //订阅事件
            _client.Connected -= ClientConnected;
            _client.Disconnected -= ClientDisConnected;
            _client.Received -= ClientRecived;

            _client.Connected += ClientConnected;
            _client.Disconnected += ClientDisConnected;
            _client.Received += ClientRecived;
            //载入配置
            _client.Setup(config);
            _client.Connect();
            IsConnected = true;
            return IsConnected;
        }
        private void ClientConnected(IClient client, MsgEventArgs e)
        {
            EventBus.Publish<NetClientConnectedEvent, NetClientConnectedEventArgs>(this, new NetClientConnectedEventArgs(e.Message));
        }
        private void ClientDisConnected(IClient client, ClientDisconnectedEventArgs e)
        {
            EventBus.Publish<NetClientDisconnectedEvent, NetClientDisconnectedEventArgs>(this, new NetClientDisconnectedEventArgs(new NetClientDisconnectedDto(e.Manual, e.Message)));
        }
        private void ClientRecived(TcpClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
        {
            EventBus.Publish<NetClientDataRecivedEvent, NetClientDataRecivedEventArgs>
                     (this, new NetClientDataRecivedEventArgs(new NetClientDataRecivedDto(byteBlock.ToArray(), Encoding.UTF8.GetString(byteBlock.ToArray()))));
        }
        public void DisConnect()
        {
            if (_client == null) return;
            _client.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            _client.Close();
            _client.SafeDispose();
            IsConnected = false;
        }

        public void Dispose()
        {
            DisConnect();
        }

        public void SubscribeEvent(Action<object, NetClientDataRecivedEventArgs> onDataRecived = null, 
                                   Action<object, NetClientConnectedEventArgs> onConnected = null, 
                                   Action<object, NetClientDisconnectedEventArgs> onDisconnected = null)
        {
            if(onDataRecived != null)
            {
                EventBus.Subscribe<NetClientDataRecivedEvent, NetClientDataRecivedEventArgs>(onDataRecived);
            }
            if(onConnected != null)
            {
                EventBus.Subscribe<NetClientConnectedEvent, NetClientConnectedEventArgs>(onConnected);
            }
            if(onDisconnected != null)
            {
                EventBus.Subscribe<NetClientDisconnectedEvent, NetClientDisconnectedEventArgs>(onDisconnected);
            }
        }

        public void UnSubscribeEvent(Action<object, NetClientDataRecivedEventArgs> onDataRecived = null, 
                                     Action<object, NetClientConnectedEventArgs> onConnected = null, 
                                     Action<object, NetClientDisconnectedEventArgs> onDisconnected = null)
        {
            if (onDataRecived != null)
            {
                EventBus.UnSubscribe<NetClientDataRecivedEvent, NetClientDataRecivedEventArgs>(onDataRecived);
            }
            if (onConnected != null)
            {
                EventBus.UnSubscribe<NetClientConnectedEvent, NetClientConnectedEventArgs>(onConnected);
            }
            if (onDisconnected != null)
            {
                EventBus.UnSubscribe<NetClientDisconnectedEvent, NetClientDisconnectedEventArgs>(onDisconnected);
            }
        }

        public void SendData(byte[] data)
        {
            if (!IsConnected)
                throw new Exception(NetClientNoConnectedString);
            _client.Send(data);
        }

        public void SendData(string data)
        {
            if (!IsConnected)
                throw new Exception(NetClientNoConnectedString);
            _client.Send(data);
        }

        public void SendData(byte[] data, int offset, int length)
        {
            if (!IsConnected)
                throw new Exception(NetClientNoConnectedString);
            _client.Send(data, offset, length);
        }
    }
}
