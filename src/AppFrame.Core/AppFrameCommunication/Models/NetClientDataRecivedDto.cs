using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class NetClientDataRecivedDto
    {
        public NetClientDataRecivedDto(byte[] byteData, string stringData)
        {
            ByteData = byteData;
            StringData = stringData;
        }

        public byte[] ByteData { get; set; }
        public string StringData { get; set; }
    }
}
