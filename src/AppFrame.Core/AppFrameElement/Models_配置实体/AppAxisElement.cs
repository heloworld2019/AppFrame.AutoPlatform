using AppFrame.Common.AppAttribute.Validate;
using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    public class AppAxisElement : AppConfigParameter
    {
        public AppAxisElement()
        {

        }
        public AppAxisElement(int index,
                              int parameterId,
                              string parameterName,
                              string parameterGroupName,
                              string parameterDescription,
                              string parameterCategory,
                              bool enable,
                              ushort cardId,
                              ushort axisId,
                              string axisName,
                              string axisUnit,
                              double pulseEquiv = 1000,
                              double startVelocity = 0,
                              double stopVelocity = 500,
                              ushort homeMode = 26,
                              double homeOffset = 0,
                              double homeLowVelocity = 10,
                              double homeHighVelocity = 50,
                              double homeTimeout = 60,
                              double jogVelocity = 20,
                              double jogAcc = 0.1,
                              double jogDec = 0.1,
                              double runVelocity = 300,
                              double runAcc = 0.1,
                              double runDec = 0.1,
                              double stopTime = 0.01,
                              bool positiveSoftLimitEnable = false,
                              bool negitiveSoftLimitEnable = false,
                              double positiveSoftLimit = 0,
                              double negitiveSoftLimit = 0,
                              double locationTimeOut = 10)
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            CardId = cardId;
            AxisId = axisId;
            AxisName = axisName;
            AxisUnit = axisUnit;
            PulseEquiv = pulseEquiv;
            StartVelocity = startVelocity;
            StopVelocity = stopVelocity;
            HomeMode = homeMode;
            HomeOffset = homeOffset;
            HomeLowVelocity = homeLowVelocity;
            HomeHighVelocity = homeHighVelocity;
            HomeTimeOut = homeTimeout;
            JogVelocity = jogVelocity;
            JogAcc = jogAcc;
            JogDec = jogDec;
            RunVelocity = runVelocity;
            RunAcc = runAcc;
            RunDec = runDec;
            StopTime = stopTime;
            PositiveSoftLimitEnable = positiveSoftLimitEnable;
            NegitiveSoftLimitEnable = negitiveSoftLimitEnable;
            PositiveSoftLimit = positiveSoftLimit;
            NegitiveSoftLimit = negitiveSoftLimit;
            LocationTimeOut = locationTimeOut;
        }
        [AppValueRange((ushort)0, (ushort)8)]
        public ushort CardId { get; set; }
        [AppValueRange((ushort)0, (ushort)164)]
        public ushort AxisId { get; set; }
        [AppRequired(ErrorInfo = "轴名称不能为空")]
        public string AxisName { get; set; }
        public string AxisUnit { get; set; }
        [AppValueRange((double)0, 1048576)]
        public double PulseEquiv { get; set; }
        [AppValueRange((double)0, 10)]
        public double StartVelocity { get; set; }
        [AppValueRange((double)0, 1000)]
        public double StopVelocity { get; set; }
        [AppValueRange((ushort)1, (ushort)35)]
        public ushort HomeMode { get; set; }
        [AppValueRange((double)0, 1000)]
        public double HomeOffset { get; set; }
        [AppValueRange((double)0, 50)]
        public double HomeLowVelocity { get; set; }
        [AppValueRange((double)0, 500)]
        public double HomeHighVelocity { get; set; }
        [AppValueRange((double)0, 120)]
        public double HomeTimeOut { get; set; }
        [AppValueRange((double)0, 50)]
        public double JogVelocity { get; set; }
        [AppValueRange((double)0, 1)]
        public double JogAcc { get; set; }
        [AppValueRange((double)0, 1)]
        public double JogDec { get; set; }
        [AppValueRange((double)0, 500)]
        public double RunVelocity { get; set; }
        [AppValueRange((double)0, 1)]
        public double RunAcc { get; set; }
        [AppValueRange((double)0, 1)]
        public double RunDec { get; set; }
        [AppValueRange(0, 0.05)]
        public double StopTime { get; set; }
        public bool PositiveSoftLimitEnable { get; set; }
        public bool NegitiveSoftLimitEnable { get; set; }
        [AppValueRange((double)0, 1000)]
        public double PositiveSoftLimit { get; set; }
        [AppValueRange((double)-1000, 0)]
        public double NegitiveSoftLimit { get; set; }
        [AppValueRange((double)0, 60)]
        public double LocationTimeOut { get; set; }
    }
}
