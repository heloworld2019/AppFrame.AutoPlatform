using AppFrame.Common.AppAttribute.Validate;
using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    public class AppSolenoidElement : AppConfigParameter
    {
        public AppSolenoidElement()
        {

        }
        public AppSolenoidElement(int index,
                                  int parameterId,
                                  string parameterName,
                                  string parameterGroupName,
                                  string parameterDescription,
                                  string parameterCategory,
                                  bool enable,
                                  string solenoidName,
                                  int goOutputId,
                                  int backOutputId,
                                  int goSensorId,
                                  int backSensorId,
                                  int goAlarmId,
                                  int backAlarmId,
                                  bool isVacuum,
                                  bool isDoubleCoil,
                                  bool goSensorShield = false,
                                  bool backSensorShield = false,
                                  double goAlarmTime = 0.3,
                                  double backAlarmTime = 0.3,
                                  double goDoneDelay = 0.01,
                                  double backDoneDelay = 0.01,
                                  double actionTimeIfGoSensorShield = 0.3,
                                  double actionTimeIfBackSensorShield = 0.3)
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            SolenoidName = solenoidName;
            GoOutputId = goOutputId;
            BackOutputId = backOutputId;
            GoSensorInputId = goSensorId;
            BackSensorInputId = backSensorId;
            GoAlarmId = goAlarmId;
            BackAlarmId = backAlarmId;
            IsVacuum = isVacuum;
            IsDoubleCoil = isDoubleCoil;
            GoSensorShield = goSensorShield;
            BackSensorShield = backSensorShield;
            GoAlarmTime = goAlarmTime;
            BackAlarmTime = backAlarmTime;
            GoDoneDelay = goDoneDelay;
            BackDoneDelay = backDoneDelay;
            ActionTimeIfGoSensorShield = actionTimeIfGoSensorShield;
            ActionTimeIfBackSensorShield = actionTimeIfBackSensorShield;
        }
        [AppRequired(ErrorInfo = "名称不能为空")]
        public string SolenoidName { get; set; }
        [AppValueRange((ushort)0, (ushort)128)]
        public int GoOutputId { get; set; }
        [AppValueRange((ushort)0, (ushort)128)]
        public int BackOutputId { get; set; }
        [AppValueRange((ushort)0, (ushort)128)]
        public int GoSensorInputId { get; set; }
        [AppValueRange((ushort)0, (ushort)128)]
        public int BackSensorInputId { get; set; }
        [AppValueRange((ushort)0, (ushort)1000)]
        public int GoAlarmId { get; set; }
        [AppValueRange((ushort)0, (ushort)1000)]
        public int BackAlarmId { get; set; }
        public bool IsVacuum { get; set; }
        public bool IsDoubleCoil { get; set; }
        public bool GoSensorShield { get; set; }
        public bool BackSensorShield { get; set; }
        [AppValueRange(0, 5.0)]
        public double GoAlarmTime { get; set; }
        [AppValueRange(0, 5.0)]
        public double BackAlarmTime { get; set; }
        [AppValueRange(0, 5.0)]
        public double GoDoneDelay { get; set; }
        [AppValueRange(0, 5.0)]
        public double BackDoneDelay { get; set; }
        [AppValueRange(0, 5.0)]
        public double ActionTimeIfGoSensorShield { get; set; }
        [AppValueRange(0, 5.0)]
        public double ActionTimeIfBackSensorShield { get; set; }
    }
}
