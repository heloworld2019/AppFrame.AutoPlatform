using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class NetClientDisconnectedDto
    {
        public NetClientDisconnectedDto(bool isManual, string message)
        {
            IsManual = isManual;
            Message = message;
        }
        /// <summary>
        /// 是否时主动断开
        /// </summary>
        public bool IsManual { get; set; }
        public string Message { get; set; }
    }
}
