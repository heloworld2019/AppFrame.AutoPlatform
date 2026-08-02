using AppFrame.Common.AppAttribute.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.Dto
{
    public class AppAxisDto
    {
        public int Index { get; set; }
        public string ParameterName { get; set; }
        public string ParameterDescription { get; set; }
        public bool Enable { get; set; }
        public ushort CardId { get; set; }
        public ushort AxisId { get; set; }
        public string AxisName { get; set; }
        public string AxisUnit { get; set; }
        public double PulseEquiv { get; set; }
        public double StartVelocity { get; set; }
        public double StopVelocity { get; set; }
        public ushort HomeMode { get; set; }
        public double HomeOffset { get; set; }
        public double HomeLowVelocity { get; set; }
        public double HomeHighVelocity { get; set; }
        public double HomeTimeOut { get; set; }
        public double JogVelocity { get; set; }
        public double JogAcc { get; set; }
        public double JogDec { get; set; }
        public double RunVelocity { get; set; }
        public double RunAcc { get; set; }
        public double RunDec { get; set; }
        public double StopTime { get; set; }
        public bool PositiveSoftLimitEnable { get; set; }
        public bool NegitiveSoftLimitEnable { get; set; }
        public double PositiveSoftLimit { get; set; }
        public double NegitiveSoftLimit { get; set; }
        public double LocationTimeOut { get; set; }
    }
}
