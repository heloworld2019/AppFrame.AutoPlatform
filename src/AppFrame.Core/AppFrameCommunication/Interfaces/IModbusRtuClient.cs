using AppFrame.Core.AppFrameCommunication.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Interfaces
{
    public interface IModbusRtuClient : IModbusClientBase
    {
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        bool Open();
        /// <summary>
        /// 关闭串口
        /// </summary>
        void Close();
        /// <summary>
        /// 串口是否打开 (true为已打开)
        /// </summary>
        bool IsOpened{ get;}
    }
}
