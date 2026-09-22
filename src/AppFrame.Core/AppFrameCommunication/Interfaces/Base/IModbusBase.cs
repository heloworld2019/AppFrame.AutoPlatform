using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Interfaces.Base
{
    public interface IModbusBase : IDisposable
    {
        /// <summary>
        /// 写入值 (注意:每次仅写入一种数据类型)
        /// </summary>
        /// <param name="address"></param>
        /// <param name="doubleValues"></param>
        /// <param name="ulongValues"></param>
        /// <param name="floatValues"></param>
        /// <param name="uintValues"></param>
        /// <param name="longValues"></param>
        /// <param name="boolValues"></param>
        /// <param name="byteValue"></param>
        /// <param name="intValues"></param>
        /// <returns></returns>
        bool Write(string address,
                   bool[] boolValue = null,
                   byte[] byteValue = null,
                   ushort[] ushortValue = null,
                   short[] shortValue = null,
                   int[] intValue = null,
                   uint[] uintValue = null,
                   ulong[] ulongValue = null,
                   long[] longValue = null,
                   float[] floatValue = null,
                   double[] doubleValue = null,
                   string stringValue = null
                   );

        Task<bool> WriteAsync(string address,
                   bool[] boolValue = null,
                   byte[] byteValue = null,
                   ushort[] ushortValue = null,
                   short[] shortValue = null,
                   int[] intValue = null,
                   uint[] uintValue = null,
                   ulong[] ulongValue = null,
                   long[] longValue = null,
                   float[] floatValue = null,
                   double[] doubelValue = null,
                   string stringValue = null
                   );
        // <summary>
        /// 读取bool值
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        bool[] ReadBool(string address, ushort length);
        Task<bool[]> ReadBoolAsync(string address, ushort length);
        /// <summary>
        /// 读取short值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        short[] ReadInt16(string address, ushort length);
        Task<short[]> ReadInt16Async(string address, ushort length);
        /// <summary>
        /// 读取ushort值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        ushort[] ReadUInt16(string address, ushort length);
        Task<ushort[]> ReadUInt16Async(string address, ushort length);
        /// <summary>
        /// 读取int值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        int[] ReadInt32(string address, ushort length);
        Task<int[]> ReadInt32Async(string address, ushort length);
        /// <summary>
        /// 读取uint值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        uint[] ReadUInt32(string address, ushort length);
        Task<uint[]> ReadUInt32Async(string address, ushort length);
        /// <summary>
        /// 读取long值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        long[] ReadInt64(string address, ushort length);
        Task<long[]> ReadInt64Async(string address, ushort length);
        /// <summary>
        /// 读取ulong值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        ulong[] ReadUInt64(string address, ushort length);
        Task<ulong[]> ReadUInt64Async(string address, ushort length);
        /// <summary>
        /// 读取float值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        float[] ReadFloat(string address, ushort length);
        Task<float[]> ReadFloatAsync(string address, ushort length);
        /// <summary>
        /// 读取double值
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        double[] ReadDouble(string address, ushort length);
        Task<double[]> ReadDoubleAsync(string address, ushort length);
        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        string ReadString(string address, ushort length);
        Task<string> ReadStringAsync(string address, ushort length);
    }
}
