using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Models
{
    public class AppAlarmVo
    {
        public string AlarmName { get; set; }
        public int Index { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public bool Enable { get; set; }
        public bool Value { get; set; }
    }
}
