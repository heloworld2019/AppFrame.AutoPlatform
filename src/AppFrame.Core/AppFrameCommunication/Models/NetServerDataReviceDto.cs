using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class NetServerDataReviceDto : AppNetClientSession
    {

        public NetServerDataReviceDto(string clientId, string clientIpAddress, int clientPort, byte[] byteData, string stringData) 
                               : base(clientId, clientIpAddress, clientPort)
        {
            ByteData = byteData;
            StringData = stringData;
        }

        public byte[] ByteData { get; set; }
        public string StringData { get; set; }
    }
}
