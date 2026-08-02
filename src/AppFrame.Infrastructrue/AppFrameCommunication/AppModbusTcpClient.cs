using AppFrame.Common.Extensions.ValidateAttribute;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameCommon.Global.Events;
using AppFrame.Core.AppFrameCommunication.Interfaces;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameException.CommunicationException;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameCommunication
{
    internal class AppModbusTcpClient : GlobalObjectManager, IModbusTcpClient
    {
        private ModbusTcpNet _busTcpClient;
        private readonly AppModbusTcpClientElement _appModbusTcpClientElement;

        public AppModbusTcpClient(AppModbusTcpClientElement appModbusTcpClientElement)
        {
            if (appModbusTcpClientElement == null)
            {
                throw new ArgumentNullException(nameof(appModbusTcpClientElement));
            }
            if (!string.IsNullOrWhiteSpace(appModbusTcpClientElement.Validate()))
            {
                throw new ModbusTcpConfigException();
            }
            _appModbusTcpClientElement = appModbusTcpClientElement;
            EventBus.Subscribe<AppDisposeEvent, AppDisposeEventArgs>(DisposeCallback);
        }

        private void DisposeCallback(object arg1, AppDisposeEventArgs arg2)
        {
            Dispose();
        }

        private bool _addressStartWithZero;
        public bool AddressStartWithZero
        {
            get { return _addressStartWithZero; }
            set { _addressStartWithZero = value; SetStartAddress(value); }
        }
        public bool IsConnected { get; private set; }

        private void SetStartAddress(bool value)
        {
            if (_busTcpClient != null) _busTcpClient.AddressStartWithZero = value;
        }

        public bool Connect()
        {
            _busTcpClient = new ModbusTcpNet(_appModbusTcpClientElement.IpAddress, _appModbusTcpClientElement.Port);
            AddressStartWithZero = true;
            IsConnected = false;
            var result = _busTcpClient.ConnectServer();
            IsConnected = result.IsSuccess;
            return IsConnected;
        }

        public void DisConnect()
        {
            if (_busTcpClient != null)
            {
                _busTcpClient.ConnectClose();
                IsConnected = false;
            }
        }

        public void Dispose()
        {
            _busTcpClient.Dispose();
            _busTcpClient = null;
        }

        public byte[] ReadFromCoreServer(byte[] send)
        {
            var result = _busTcpClient.ReadFromCoreServer(send);
            return result.Content;
        }

        public async Task<byte[]> ReadFromCoreServerAsync(byte[] send)
        {
            var result = await _busTcpClient.ReadFromCoreServerAsync(send);
            return result.Content;
        }
        public bool[] ReadBool(string address, ushort length)
        {
            var result = _busTcpClient.ReadBool(address, length);
            return result.Content;
        }

        public async Task<bool[]> ReadBoolAsync(string address, ushort length)
        {
            var result = await _busTcpClient.ReadBoolAsync(address, length);
            return result.Content;
        }

        public short[] ReadInt16(string address, ushort length)
        {
            var result = _busTcpClient.ReadInt16(address, length);
            return result.Content;
        }

        public async Task<short[]> ReadInt16Async(string address, ushort length)
        {
            var result = await _busTcpClient.ReadInt16Async(address, length);
            return result.Content;
        }

        public ushort[] ReadUInt16(string address, ushort length)
        {
            var result = _busTcpClient.ReadUInt16(address, length);
            return result.Content;
        }

        public async Task<ushort[]> ReadUInt16Async(string address, ushort length)
        {
            var result = await _busTcpClient.ReadUInt16Async(address, length);
            return result.Content;
        }

        public int[] ReadInt32(string address, ushort length)
        {
            var result = _busTcpClient.ReadInt32(address, length);
            return result.Content;
        }

        public async Task<int[]> ReadInt32Async(string address, ushort length)
        {
            var result = await _busTcpClient.ReadInt32Async(address, length);
            return result.Content;
        }

        public uint[] ReadUInt32(string address, ushort length)
        {
            var result = _busTcpClient.ReadUInt32(address, length);
            return result.Content;
        }

        public async Task<uint[]> ReadUInt32Async(string address, ushort length)
        {
            var result = await _busTcpClient.ReadUInt32Async(address, length);
            return result.Content;
        }

        public long[] ReadInt64(string address, ushort length)
        {
            var result = _busTcpClient.ReadInt64(address, length);
            return result.Content;
        }

        public async Task<long[]> ReadInt64Async(string address, ushort length)
        {
            var result = await _busTcpClient.ReadInt64Async(address, length);
            return result.Content;
        }

        public ulong[] ReadUInt64(string address, ushort length)
        {
            var result = _busTcpClient.ReadUInt64(address, length);
            return result.Content;
        }

        public async Task<ulong[]> ReadUInt64Async(string address, ushort length)
        {
            var result = await _busTcpClient.ReadUInt64Async(address, length);
            return result.Content;
        }

        public float[] ReadFloat(string address, ushort length)
        {
            var result = _busTcpClient.ReadFloat(address, length);
            return result.Content;
        }

        public async Task<float[]> ReadFloatAsync(string address, ushort length)
        {
            var result = await _busTcpClient.ReadFloatAsync(address, length);
            return result.Content;
        }

        public double[] ReadDouble(string address, ushort length)
        {
            var result = _busTcpClient.ReadDouble(address, length);
            return result.Content;
        }

        public async Task<double[]> ReadDoubleAsync(string address, ushort length)
        {
            var result = await _busTcpClient.ReadDoubleAsync(address, length);
            return result.Content;
        }

        public string ReadString(string address, ushort length)
        {
            var result = _busTcpClient.ReadString(address, length);
            return result.Content;
        }

        public async Task<string> ReadStringAsync(string address, ushort length)
        {
            var result = await _busTcpClient.ReadStringAsync(address, length);
            return result.Content;
        }

        public bool Write(string address, bool[] boolValue = null, byte[] byteValue = null, ushort[] ushortValue = null,
                         short[] shortValue = null, int[] intValue = null, uint[] uintValue = null, ulong[] ulongValue = null,
                         long[] longValue = null, float[] floatValue = null, double[] doubelValue = null, string stringValue = null)
        {
            OperateResult result;
            if (boolValue != null)
            {
                result = _busTcpClient.Write(address, boolValue);
                return result.IsSuccess;
            }
            if (byteValue != null)
            {
                result = _busTcpClient.Write(address, byteValue);
                return result.IsSuccess;
            }
            if (ushortValue != null)
            {
                result = _busTcpClient.Write(address, ushortValue);
                return result.IsSuccess;
            }
            if (shortValue != null)
            {
                result = _busTcpClient.Write(address, shortValue);
                return result.IsSuccess;
            }
            if (intValue != null)
            {
                result = _busTcpClient.Write(address, intValue);
                return result.IsSuccess;
            }
            if (uintValue != null)
            {
                result = _busTcpClient.Write(address, uintValue);
                return result.IsSuccess;
            }
            if (ulongValue != null)
            {
                result = _busTcpClient.Write(address, ulongValue);
                return result.IsSuccess;
            }
            if (longValue != null)
            {
                result = _busTcpClient.Write(address, longValue);
                return result.IsSuccess;
            }
            if (floatValue != null)
            {
                result = _busTcpClient.Write(address, floatValue);
                return result.IsSuccess;
            }
            if (doubelValue != null)
            {
                result = _busTcpClient.Write(address, doubelValue);
                return result.IsSuccess;
            }
            if (stringValue != null)
            {
                result = _busTcpClient.Write(address, stringValue);
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
                result = await _busTcpClient.WriteAsync(address, boolValue);
                return result.IsSuccess;
            }
            if (byteValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, byteValue);
                return result.IsSuccess;
            }
            if (ushortValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, ushortValue);
                return result.IsSuccess;
            }
            if (shortValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, shortValue);
                return result.IsSuccess;
            }
            if (intValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, intValue);
                return result.IsSuccess;
            }
            if (uintValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, uintValue);
                return result.IsSuccess;
            }
            if (ulongValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, ulongValue);
                return result.IsSuccess;
            }
            if (longValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, longValue);
                return result.IsSuccess;
            }
            if (floatValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, floatValue);
                return result.IsSuccess;
            }
            if (doubelValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, doubelValue);
                return result.IsSuccess;
            }
            if (stringValue != null)
            {
                result = await _busTcpClient.WriteAsync(address, stringValue);
                return result.IsSuccess;
            }
            return false;
        }
    }
}
