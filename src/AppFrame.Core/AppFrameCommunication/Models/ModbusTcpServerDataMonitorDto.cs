using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommunication.Models
{
    public class ModbusTcpServerDataMonitorDto
    {
        public ModbusTcpServerDataMonitorDto(string monitorAddress, short beforeValue, short afterValue)
        {
            MonitorAddress = monitorAddress;
            BeforeValue = beforeValue;
            AfterValue = afterValue;
        }

        public string MonitorAddress { get; set; }
        public short BeforeValue { get; set; }
        public short AfterValue { get; set; }
    }
}
