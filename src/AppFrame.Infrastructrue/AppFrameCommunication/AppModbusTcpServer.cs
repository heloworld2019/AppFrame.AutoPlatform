using AppFrame.Common.Extensions.ValidateAttribute;
using AppFrame.Common.Utilities;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameCommon.Global.Events;
using AppFrame.Core.AppFrameCommunication.Events;
using AppFrame.Core.AppFrameCommunication.Interfaces;
using AppFrame.Core.AppFrameCommunication.Models;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameException.CommunicationException;
using HslCommunication;
using HslCommunication.Core.Net;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace AppFrame.Infrastructrue.AppFrameCommunication
{
    internal class AppModbusTcpServer : GlobalObjectManager, IModbusTcpServer
    {
        private static AppModbusTcpServer _instance;
        private static readonly object _lock = new object();
        private ModbusTcpServer _busTcpServer;
        private readonly AppModbusTcpServerElement _appModbusTcpServerElement;
        private ModBusMonitorAddress _busMonitorAddress;
        public bool IsStarted { get; private set; }

        private AppModbusTcpServer(AppModbusTcpServerElement appModbusTcpServerElement)
        {
            if (appModbusTcpServerElement == null)
            {
                throw new ArgumentNullException(nameof(appModbusTcpServerElement));
            }
            if (!string.IsNullOrWhiteSpace(appModbusTcpServerElement.Validate()))
            {
                throw new ModbusTcpConfigException();
            }
            _appModbusTcpServerElement = appModbusTcpServerElement;
            _busTcpServer = new ModbusTcpServer();
            EventBus.Subscribe<AppDisposeEvent, AppDisposeEventArgs>(DisposeCallback);
        }

        private void DisposeCallback(object arg1, AppDisposeEventArgs arg2)
        {
            Dispose();
        }

        public static AppModbusTcpServer Create(AppModbusTcpServerElement appModbusTcpServerElement)
        {
            lock (_lock)
            {
                return _instance ?? (_instance = new AppModbusTcpServer(appModbusTcpServerElement));
            }
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public bool StartService()
        {
            lock (_lock)
            {                
                _busTcpServer.OnDataSend -= _busTcpServer_OnDataSend;
                _busTcpServer.OnDataReceived -= _busTcpServer_OnDataReceived;
                _busTcpServer.OnClientOnline -= _busTcpServer_OnClientOnline;
                _busTcpServer.OnClientOffline -= _busTcpServer_OnClientOffline;

                _busTcpServer.OnDataSend += _busTcpServer_OnDataSend;
                _busTcpServer.OnDataReceived += _busTcpServer_OnDataReceived;
                _busTcpServer.OnClientOnline += _busTcpServer_OnClientOnline;
                _busTcpServer.OnClientOffline += _busTcpServer_OnClientOffline;

                _busTcpServer.ServerStart(_appModbusTcpServerElement.Port);
                IsStarted = _busTcpServer.IsStarted;
                return true;
            }           
        }
        //客户端下线时触发
        private void _busTcpServer_OnClientOffline(object server, AppSession session)
        {
            EventBus.Publish<ModbusTcpClientOfflineEvent, ModbusTcpClientOfflineEventArgs>
                     (this, new ModbusTcpClientOfflineEventArgs(new ModbusTcpClientConnectDto(session.IpAddress, session.IpEndPoint.Port, session.ClientUniqueID, session.OnlineTime)));
        }
        //客户端上线时触发
        private void _busTcpServer_OnClientOnline(object server, AppSession session)
        {
            EventBus.Publish<ModbusTcpClientOnlineEvent, ModbusTcpClientOnlineEventArgs>
                     (this, new ModbusTcpClientOnlineEventArgs(new ModbusTcpClientConnectDto(session.IpAddress, session.IpEndPoint.Port, session.ClientUniqueID, session.OnlineTime)));
        }
        //数据接受时触发
        private void _busTcpServer_OnDataReceived(object sender, object source, byte[] data)
        {
            EventBus.Publish<ModbusTcpServerDataRecivedEvent, ModbusTcpServerDataRecivedEventArgs>
                     (this, new ModbusTcpServerDataRecivedEventArgs(new ModbusTcpServerDataReciveDto(sender, source, data)));
        }
        //数据发送时触发
        private void _busTcpServer_OnDataSend(object sender, byte[] data)
        {
            EventBus.Publish<ModbusTcpServerDataSendEvent, ModbusTcpServerDataSendEventArgs>
                     (this, new ModbusTcpServerDataSendEventArgs(new ModbusTcpServerDataSendDto(sender, data)));
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopService()
        {
            lock (_lock)
            {
                if (_busTcpServer != null)
                {
                    _busTcpServer.ServerClose();
                    IsStarted = false;
                }
            }           
        }
        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void Dispose()
        {
            if (_busMonitorAddress != null)
                _busTcpServer.RemoveSubcrption(_busMonitorAddress);
            _busMonitorAddress = null;
            _busTcpServer.Dispose();
            _busTcpServer = null;
        }
        /// <summary>
        /// 过滤客户端
        /// </summary>
        /// <param name="IpAddresses"></param>
        public void SetTrustedIpAddress(List<string> IpAddresses)
        { 
            lock (_lock)
            {
                if (IpAddresses is null)
                {
                    throw new ArgumentNullException(nameof(IpAddresses));
                }
                foreach (string ipAddress in IpAddresses)
                {
                    if (!AppRegex.MatchIpAddress(ipAddress))
                    {
                        throw new Exception(IpAddressErrorString);
                    }
                }
                _busTcpServer.SetTrustedIpAddress(IpAddresses);
            }    
        }
        /// <summary>
        /// 移除过滤
        /// </summary>
        public void RemoveTrusted()
        {
            lock (_lock)
            {
                _busTcpServer.SetTrustedIpAddress(null);
            } 
        }
        /// <summary>
        /// 订阅数据监控
        /// </summary>
        /// <param name="address"></param>
        public void SubcribeDataMonitor(string address)
        {
            if(!ushort.TryParse(address, out ushort result))
            {
                throw new Exception(ModbusTcpServerSubscribeAddressErrorString);
            }
            _busMonitorAddress = new ModBusMonitorAddress
            {
                Address = result
            };
            _busMonitorAddress.OnChange += MonitorAddress_OnChanged;
            _busTcpServer.AddSubcription(_busMonitorAddress);
        }
        /// <summary>
        /// 取消订阅数据监控
        /// </summary>
        public void UnsubcribeDataMonitor()
        {
            if (_busMonitorAddress != null)
                _busTcpServer.RemoveSubcrption(_busMonitorAddress);
        }
        //订阅地址 数据更改时触发
        private void MonitorAddress_OnChanged(ModBusMonitorAddress arg1, short beforeValue, short afterValue)
        {
            //发布事件
            EventBus.Publish<ModbusTcpServerDataMonitorChangedEvent, ModbusTcpServerDataMonitorChangedEventArgs>
                            (this, new ModbusTcpServerDataMonitorChangedEventArgs(new ModbusTcpServerDataMonitorDto(arg1.Address.ToString(), beforeValue, afterValue)));
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="onDataRecived"></param>
        /// <param name="onDataSend"></param>
        /// <param name="onDataMonitor"></param>
        /// <param name="onClientOnline"></param>
        /// <param name="onClientOffline"></param>
        public void SubscribeEvent(Action<object, ModbusTcpServerDataRecivedEventArgs> onDataRecived = null, 
                                   Action<object, ModbusTcpServerDataSendEventArgs> onDataSend = null, 
                                   Action<object, ModbusTcpServerDataMonitorChangedEventArgs> onDataMonitor = null, 
                                   Action<object, ModbusTcpClientOnlineEventArgs> onClientOnline = null, 
                                   Action<object, ModbusTcpClientOfflineEventArgs> onClientOffline = null)
        {
            if(onDataRecived != null)
            {
                EventBus.Subscribe<ModbusTcpServerDataRecivedEvent, ModbusTcpServerDataRecivedEventArgs>(onDataRecived);
            }
            if(onDataSend != null)
            {
                EventBus.Subscribe<ModbusTcpServerDataSendEvent, ModbusTcpServerDataSendEventArgs>(onDataSend);
            }
            if(onDataMonitor != null)
            {
                EventBus.Subscribe<ModbusTcpServerDataMonitorChangedEvent, ModbusTcpServerDataMonitorChangedEventArgs>(onDataMonitor);
            }
            if(onClientOnline != null)
            {
                EventBus.Subscribe<ModbusTcpClientOnlineEvent, ModbusTcpClientOnlineEventArgs>(onClientOnline);
            }
            if (onClientOffline != null)
            {
                EventBus.Subscribe<ModbusTcpClientOfflineEvent, ModbusTcpClientOfflineEventArgs>(onClientOffline);
            }
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="onDataRecived"></param>
        /// <param name="onDataSend"></param>
        /// <param name="onDataMonitor"></param>
        /// <param name="onClientOnline"></param>
        /// <param name="onClientOffline"></param>
        public void UnSubscribeEvent(Action<object, ModbusTcpServerDataRecivedEventArgs> onDataRecived = null, 
                                     Action<object, ModbusTcpServerDataSendEventArgs> onDataSend = null, 
                                     Action<object, ModbusTcpServerDataMonitorChangedEventArgs> onDataMonitor = null, 
                                     Action<object, ModbusTcpClientOnlineEventArgs> onClientOnline = null, 
                                     Action<object, ModbusTcpClientOfflineEventArgs> onClientOffline = null)
        {
            if (onDataRecived != null)
            {
                EventBus.UnSubscribe<ModbusTcpServerDataRecivedEvent, ModbusTcpServerDataRecivedEventArgs>(onDataRecived);
            }
            if (onDataSend != null)
            {
                EventBus.UnSubscribe<ModbusTcpServerDataSendEvent, ModbusTcpServerDataSendEventArgs>(onDataSend);
            }
            if (onDataMonitor != null)
            {
                EventBus.UnSubscribe<ModbusTcpServerDataMonitorChangedEvent, ModbusTcpServerDataMonitorChangedEventArgs>(onDataMonitor);
            }
            if (onClientOnline != null)
            {
                EventBus.UnSubscribe<ModbusTcpClientOnlineEvent, ModbusTcpClientOnlineEventArgs>(onClientOnline);
            }
            if (onClientOffline != null)
            {
                EventBus.UnSubscribe<ModbusTcpClientOfflineEvent, ModbusTcpClientOfflineEventArgs>(onClientOffline);
            }
        }
        public bool[] ReadBool(string address, ushort length)
        {
            var result = _busTcpServer.ReadBool(address, length);
            return result.Content;
        }

        public async Task<bool[]> ReadBoolAsync(string address, ushort length)
        {
            var result = await _busTcpServer.ReadBoolAsync(address, length);
            return result.Content;
        }

        public short[] ReadInt16(string address, ushort length)
        {
            var result = _busTcpServer.ReadInt16(address, length);
            return result.Content;
        }

        public async Task<short[]> ReadInt16Async(string address, ushort length)
        {
            var result = await _busTcpServer.ReadInt16Async(address, length);
            return result.Content;
        }

        public ushort[] ReadUInt16(string address, ushort length)
        {
            var result = _busTcpServer.ReadUInt16(address, length);
            return result.Content;
        }

        public async Task<ushort[]> ReadUInt16Async(string address, ushort length)
        {
            var result = await _busTcpServer.ReadUInt16Async(address, length);
            return result.Content;
        }

        public int[] ReadInt32(string address, ushort length)
        {
            var result = _busTcpServer.ReadInt32(address, length);
            return result.Content;
        }

        public async Task<int[]> ReadInt32Async(string address, ushort length)
        {
            var result = await _busTcpServer.ReadInt32Async(address, length);
            return result.Content;
        }

        public uint[] ReadUInt32(string address, ushort length)
        {
            var result = _busTcpServer.ReadUInt32(address, length);
            return result.Content;
        }

        public async Task<uint[]> ReadUInt32Async(string address, ushort length)
        {
            var result = await _busTcpServer.ReadUInt32Async(address, length);
            return result.Content;
        }

        public long[] ReadInt64(string address, ushort length)
        {
            var result = _busTcpServer.ReadInt64(address, length);
            return result.Content;
        }

        public async Task<long[]> ReadInt64Async(string address, ushort length)
        {
            var result = await _busTcpServer.ReadInt64Async(address, length);
            return result.Content;
        }

        public ulong[] ReadUInt64(string address, ushort length)
        {
            var result = _busTcpServer.ReadUInt64(address, length);
            return result.Content;
        }

        public async Task<ulong[]> ReadUInt64Async(string address, ushort length)
        {
            var result = await _busTcpServer.ReadUInt64Async(address, length);
            return result.Content;
        }

        public float[] ReadFloat(string address, ushort length)
        {
            var result = _busTcpServer.ReadFloat(address, length);
            return result.Content;
        }

        public async Task<float[]> ReadFloatAsync(string address, ushort length)
        {
            var result = await _busTcpServer.ReadFloatAsync(address, length);
            return result.Content;
        }

        public double[] ReadDouble(string address, ushort length)
        {
            var result = _busTcpServer.ReadDouble(address, length);
            return result.Content;
        }

        public async Task<double[]> ReadDoubleAsync(string address, ushort length)
        {
            var result = await _busTcpServer.ReadDoubleAsync(address, length);
            return result.Content;
        }

        public string ReadString(string address, ushort length)
        {
            var result = _busTcpServer.ReadString(address, length);
            return result.Content;
        }

        public async Task<string> ReadStringAsync(string address, ushort length)
        {
            var result = await _busTcpServer.ReadStringAsync(address, length);
            return result.Content;
        }

        public bool Write(string address, bool[] boolValue = null, byte[] byteValue = null, ushort[] ushortValue = null,
                         short[] shortValue = null, int[] intValue = null, uint[] uintValue = null, ulong[] ulongValue = null,
                         long[] longValue = null, float[] floatValue = null, double[] doubelValue = null, string stringValue = null)
        {
            OperateResult result;
            if (boolValue != null)
            {
                result = _busTcpServer.Write(address, boolValue);
                return result.IsSuccess;
            }
            if (byteValue != null)
            {
                result = _busTcpServer.Write(address, byteValue);
                return result.IsSuccess;
            }
            if (ushortValue != null)
            {
                result = _busTcpServer.Write(address, ushortValue);
                return result.IsSuccess;
            }
            if (shortValue != null)
            {
                result = _busTcpServer.Write(address, shortValue);
                return result.IsSuccess;
            }
            if (intValue != null)
            {
                result = _busTcpServer.Write(address, intValue);
                return result.IsSuccess;
            }
            if (uintValue != null)
            {
                result = _busTcpServer.Write(address, uintValue);
                return result.IsSuccess;
            }
            if (ulongValue != null)
            {
                result = _busTcpServer.Write(address, ulongValue);
                return result.IsSuccess;
            }
            if (longValue != null)
            {
                result = _busTcpServer.Write(address, longValue);
                return result.IsSuccess;
            }
            if (floatValue != null)
            {
                result = _busTcpServer.Write(address, floatValue);
                return result.IsSuccess;
            }
            if (doubelValue != null)
            {
                result = _busTcpServer.Write(address, doubelValue);
                return result.IsSuccess;
            }
            if (stringValue != null)
            {
                result = _busTcpServer.Write(address, stringValue);
                return result.IsSuccess;
            }
            return false;
        }

        public async Task<bool> WriteAsync(string address, bool[] boolValue = null, byte[] byteValue = null, ushort[] ushortValue = null,
                                     short[] shortValue = null, int[] intValue = null, uint[] uintValue = null, ulong[] ulongValue = null,
                                     long[] longValue = null, float[] floatValue = null, double[] doubelValue = null, string stringValue = null)
        {
            OperateResult result;
            if (boolValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, boolValue);
                return result.IsSuccess;
            }
            if (byteValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, byteValue);
                return result.IsSuccess;
            }
            if (ushortValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, ushortValue);
                return result.IsSuccess;
            }
            if (shortValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, shortValue);
                return result.IsSuccess;
            }
            if (intValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, intValue);
                return result.IsSuccess;
            }
            if (uintValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, uintValue);
                return result.IsSuccess;
            }
            if (ulongValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, ulongValue);
                return result.IsSuccess;
            }
            if (longValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, longValue);
                return result.IsSuccess;
            }
            if (floatValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, floatValue);
                return result.IsSuccess;
            }
            if (doubelValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, doubelValue);
                return result.IsSuccess;
            }
            if (stringValue != null)
            {
                result = await _busTcpServer.WriteAsync(address, stringValue);
                return result.IsSuccess;
            }
            return false;
        }
    }
}
