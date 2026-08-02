using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Interfaces.Base
{
    public interface IModbusClientBase : IModbusBase
    {
        /// <summary>
        /// 从站地址是否从0开始 (默认为true)
        /// </summary>
        bool AddressStartWithZero { get; set; }
        /// <summary>
        /// 通过报文读取 只需要给定核心报文（如读取读取寄存器0：01 03 00 00 00 01）
        /// </summary>
        /// <param name="send"></param>
        /// <returns></returns>
        byte[] ReadFromCoreServer(byte[] send);
        Task<byte[]> ReadFromCoreServerAsync(byte[] send);
    }
}
