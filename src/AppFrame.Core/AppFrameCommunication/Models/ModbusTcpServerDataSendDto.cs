using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class ModbusTcpServerDataSendDto
    {
        public ModbusTcpServerDataSendDto(object sender, byte[] sendData)
        {
            Sender = sender;
            SendData = sendData;
        }

        public object Sender { get; set; }
        public byte[] SendData { get; set; }
    }
}
