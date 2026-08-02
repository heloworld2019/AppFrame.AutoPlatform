using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class ModbusTcpServerDataReciveDto
    {
        public ModbusTcpServerDataReciveDto(object server, object client, byte[] data)
        {
            Server = server;
            Client = client;
            Data = data;
        }

        public object Server { get; set; }
        public object Client { get; set; }
        public byte[] Data { get; set; }
    }
}
