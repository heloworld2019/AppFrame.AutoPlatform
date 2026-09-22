using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models
{
    public class AppSolenoidVo
    {
        public int Index { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public bool Enable { get; set; }
        public string SolenoidName { get; set; }
        public int GoOutputId { get; set; }
        public int BackOutputId { get; set; }
        public int GoSensorInputId { get; set; }
        public int BackSensorInputId { get; set; }
        public int GoAlarmId { get; set; }
        public int BackAlarmId { get; set; }
        public bool IsVacuum { get; set; }
        public bool IsDoubleCoil { get; set; }
        public bool GoSensorShield { get; set; }
        public bool BackSensorShield { get; set; }
        public double GoAlarmTime { get; set; }
        public double BackAlarmTime { get; set; }
        public double GoDoneDelay { get; set; }
        public double BackDoneDelay { get; set; }
        public double ActionTimeIfGoSensorShield { get; set; }
        public double ActionTimeIfBackSensorShield { get; set; }
    }
}
