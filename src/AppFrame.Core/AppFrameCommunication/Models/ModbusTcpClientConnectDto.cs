using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class ModbusTcpClientConnectDto
    {
        public ModbusTcpClientConnectDto(string ipAddress, int port, string clientId, DateTime onlineTime)
        {
            IpAddress = ipAddress;
            Port = port;
            ClientId = clientId;
            OnlineTime = onlineTime;
        }

        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string ClientId { get; set; }
        public DateTime OnlineTime { get; set; }
    }
}
