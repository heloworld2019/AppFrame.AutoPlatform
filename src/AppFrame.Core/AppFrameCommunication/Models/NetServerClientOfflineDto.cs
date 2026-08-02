using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class NetServerClientOfflineDto : AppNetClientSession
    {

        public NetServerClientOfflineDto(string clientId, string clientIpAddress, int clientPort, bool isManual, string message) 
                                  : base(clientId, clientIpAddress, clientPort)
        {
            IsManual = isManual;
            Message = message;
        }
        public bool IsManual { get; set; }
        public string Message { get; set; }
    }
}
