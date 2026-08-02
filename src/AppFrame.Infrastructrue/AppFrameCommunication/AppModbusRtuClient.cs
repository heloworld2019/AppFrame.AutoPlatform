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
    internal class AppModbusRtuClient : GlobalObjectManager, IModbusRtuClient
    {
        private ModbusRtu _busRtuClient;
        private readonly AppModbusRtuElement _appModbusRtuElement;
        public AppModbusRtuClient(AppModbusRtuElement appModbusRtuElement)
        {
            if (appModbusRtuElement == null)
            {
                throw new ArgumentNullException(nameof(appModbusRtuElement));
            }
            if (!string.IsNullOrWhiteSpace(appModbusRtuElement.Validate()))
            {
                throw new ModbusRtuConfigException();
            }
            _appModbusRtuElement = appModbusRtuElement;
            EventBus.Subscribe<AppDisposeEvent, AppDisposeEventArgs>(DisposeCallback);
        }

        private void DisposeCallback(object arg1, AppDisposeEventArgs arg2)
        {
            Dispose();
        }

        public bool IsOpened { get; private set; }

        private bool _addressStartWithZero;
        public bool AddressStartWithZero
        {
            get { return _addressStartWithZero; }
            set { _addressStartWithZero = value; SetStartAddress(value); }
        }

        private void SetStartAddress(bool value)
        {
            if (_busRtuClient != null) _busRtuClient.AddressStartWithZero = value;
        }
        public bool Open()
        {
            _busRtuClient = new ModbusRtu();
            _busRtuClient.SerialPortInni(sp =>
            {
                sp.PortName = _appModbusRtuElement.PortName;
                sp.BaudRate = _appModbusRtuElement.BaudRate;
                sp.DataBits = _appModbusRtuElement.DataBit;
                sp.StopBits = _appModbusRtuElement.StopBit;
                sp.Parity = _appModbusRtuElement.Parity;
            });
            IsOpened = false;
            AddressStartWithZero = true;
            var result = _busRtuClient.Open();
            IsOpened = result.IsSuccess;
            return IsOpened;
        }
        public void Close()
        {
            if (_busRtuClient != null && _busRtuClient.IsOpen())
            {
                IsOpened = false;
                _busRtuClient.Close();
            }
        }
        public void Dispose()
        {
            _busRtuClient.Dispose();
            _busRtuClient = null;
        }

        public byte[] ReadFromCoreServer(byte[] send)
        {
            var result = _busRtuClient.ReadFromCoreServer(send);
            return result.Content;
        }

        public async Task<byte[]> ReadFromCoreServerAsync(byte[] send)
        {
            var result = await _busRtuClient.ReadFromCoreServerAsync(send);
            return result.Content;
        }
        public bool[] ReadBool(string address, ushort length)
        {
            var result = _busRtuClient.ReadBool(address, length);
            return result.Content;
        }

        public async Task<bool[]> ReadBoolAsync(string address, ushort length)
        {
            var result = await _busRtuClient.ReadBoolAsync(address, length);
            return result.Content;
        }

        public short[] ReadInt16(string address, ushort length)
        {
            var result = _busRtuClient.ReadInt16(address, length);
            return result.Content;
        }

        public async Task<short[]> ReadInt16Async(string address, ushort length)
        {
            var result = await _busRtuClient.ReadInt16Async(address, length);
            return result.Content;
        }

        public ushort[] ReadUInt16(string address, ushort length)
        {
            var result = _busRtuClient.ReadUInt16(address, length);
            return result.Content;
        }

        public async Task<ushort[]> ReadUInt16Async(string address, ushort length)
        {
            var result = await _busRtuClient.ReadUInt16Async(address, length);
            return result.Content;
        }

        public int[] ReadInt32(string address, ushort length)
        {
            var result = _busRtuClient.ReadInt32(address, length);
            return result.Content;
        }

        public async Task<int[]> ReadInt32Async(string address, ushort length)
        {
            var result = await _busRtuClient.ReadInt32Async(address, length);
            return result.Content;
        }

        public uint[] ReadUInt32(string address, ushort length)
        {
            var result = _busRtuClient.ReadUInt32(address, length);
            return result.Content;
        }

        public async Task<uint[]> ReadUInt32Async(string address, ushort length)
        {
            var result = await _busRtuClient.ReadUInt32Async(address, length);
            return result.Content;
        }

        public long[] ReadInt64(string address, ushort length)
        {
            var result = _busRtuClient.ReadInt64(address, length);
            return result.Content;
        }

        public async Task<long[]> ReadInt64Async(string address, ushort length)
        {
            var result = await _busRtuClient.ReadInt64Async(address, length);
            return result.Content;
        }

        public ulong[] ReadUInt64(string address, ushort length)
        {
            var result = _busRtuClient.ReadUInt64(address, length);
            return result.Content;
        }

        public async Task<ulong[]> ReadUInt64Async(string address, ushort length)
        {
            var result = await _busRtuClient.ReadUInt64Async(address, length);
            return result.Content;
        }

        public float[] ReadFloat(string address, ushort length)
        {
            var result = _busRtuClient.ReadFloat(address, length);
            return result.Content;
        }

        public async Task<float[]> ReadFloatAsync(string address, ushort length)
        {
            var result = await _busRtuClient.ReadFloatAsync(address, length);
            return result.Content;
        }

        public double[] ReadDouble(string address, ushort length)
        {
            var result = _busRtuClient.ReadDouble(address, length);
            return result.Content;
        }

        public async Task<double[]> ReadDoubleAsync(string address, ushort length)
        {
            var result = await _busRtuClient.ReadDoubleAsync(address, length);
            return result.Content;
        }

        public string ReadString(string address, ushort length)
        {
            var result = _busRtuClient.ReadString(address, length);
            return result.Content;
        }

        public async Task<string> ReadStringAsync(string address, ushort length)
        {
            var result = await _busRtuClient.ReadStringAsync(address, length);
            return result.Content;
        }

        public bool Write(string address, bool[] boolValue = null, byte[] byteValue = null, ushort[] ushortValue = null, 
                         short[] shortValue = null, int[] intValue = null, uint[] uintValue = null, ulong[] ulongValue = null, 
                         long[] longValue = null, float[] floatValue = null, double[] doubelValue = null, string stringValue = null)
        {
            OperateResult result;
            if (boolValue != null)
            {
                result = _busRtuClient.Write(address, boolValue);
                return result.IsSuccess;
            }
            if (byteValue != null)
            {
                result = _busRtuClient.Write(address, byteValue);
                return result.IsSuccess;
            }
            if (ushortValue != null)
            {
                result = _busRtuClient.Write(address, ushortValue);
                return result.IsSuccess;
            }
            if (shortValue != null)
            {
                result = _busRtuClient.Write(address, shortValue);
                return result.IsSuccess;
            }
            if (intValue != null)
            {
                result = _busRtuClient.Write(address, intValue);
                return result.IsSuccess;
            }
            if (uintValue != null)
            {
                result = _busRtuClient.Write(address, uintValue);
                return result.IsSuccess;
            }
            if (ulongValue != null)
            {
                result = _busRtuClient.Write(address, ulongValue);
                return result.IsSuccess;
            }
            if (longValue != null)
            {
                result = _busRtuClient.Write(address, longValue);
                return result.IsSuccess;
            }
            if (floatValue != null)
            {
                result = _busRtuClient.Write(address, floatValue);
                return result.IsSuccess;
            }
            if (doubelValue != null)
            {
                result = _busRtuClient.Write(address, doubelValue);
                return result.IsSuccess;
            }
            if (stringValue != null)
            {
                result = _busRtuClient.Write(address, stringValue);
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
                result = await _busRtuClient.WriteAsync(address, boolValue);
                return result.IsSuccess;
            }
            if (byteValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, byteValue);
                return result.IsSuccess;
            }
            if (ushortValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, ushortValue);
                return result.IsSuccess;
            }
            if (shortValue != null)
            {
                result =  await _busRtuClient.WriteAsync(address, shortValue);
                return result.IsSuccess;
            }
            if (intValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, intValue);
                return result.IsSuccess;
            }
            if (uintValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, uintValue);
                return result.IsSuccess;
            }
            if (ulongValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, ulongValue);
                return result.IsSuccess;
            }
            if (longValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, longValue);
                return result.IsSuccess;
            }
            if (floatValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, floatValue);
                return result.IsSuccess;
            }
            if (doubelValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, doubelValue);
                return result.IsSuccess;
            }
            if (stringValue != null)
            {
                result = await _busRtuClient.WriteAsync(address, stringValue);
                return result.IsSuccess;
            }
            return false;
        }
    }
}
