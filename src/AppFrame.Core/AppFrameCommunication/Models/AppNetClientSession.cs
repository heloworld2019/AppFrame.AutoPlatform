using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class AppNetClientSession
    {
        public AppNetClientSession(string clientId, string clientIpAddress, int clientPort)
        {
            ClientId = clientId;
            ClientIpAddress = clientIpAddress;
            ClientPort = clientPort;
        }

        public string ClientId { get; set; }
        public string ClientIpAddress { get; set; }
        public int ClientPort { get; set; }
    }
}
