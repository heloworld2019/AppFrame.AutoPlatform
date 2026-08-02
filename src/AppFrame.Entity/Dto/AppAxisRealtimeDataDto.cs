using AppFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.Dto
{
    public class AppAxisRealtimeDataDto
    {
        public double CurrentVelocity { get; set; }
        public double CurrentPosition { get; set; }
        public double CurrentPulsePosition { get; set; }
        public int StatuWord { get; set; }
        public int ControlWord { get; set; }
        public short ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool MoveDone { get; set; }
        public bool HomeDone { get; set; }
        public bool IsPowerOn { get; set; }
        public bool IsErrorStop { get; set; }
        public bool IsAlarming { get; set; }
        public bool PositiveLimitSwitch { get; set; }
        public bool NegitiveLimitSwitch { get; set; }
        public bool HomeSwitch { get; set; }
        public bool PositiveSoftLimitSwitch { get; set; }
        public bool NegitiveSoftLimitSwitch { get; set; }
        public MotionAxisStateMachine AxisStateMachine { get; set; }
    }
}
