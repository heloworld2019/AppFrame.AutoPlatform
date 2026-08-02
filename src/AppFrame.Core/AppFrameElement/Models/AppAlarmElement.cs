using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Core.AppFrameCommon.Events;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameConfig.Models;
using AppFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    /// <summary>
    /// 报警元件
    /// </summary>
    public class AppAlarmElement : AppConfigParameter
    {
        public AppAlarmElement()
        {

        }
        public AppAlarmElement(int index,
                               int parameterId,
                               string parameterName,
                               string parameterGroupName,
                               string parameterDescription,
                               string parameterCategory,
                               bool enable,
                               string alarmName
                               )
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            AlarmName = alarmName;
        }
        public string AlarmName { get; set; }
        [ConfigFilter]
        public AppAlarmDegreeEnum AlarmDegree { get; set; }

        private bool _value;
        [ConfigFilter]
        public  bool Value
        {
            get { return _value; }
            set 
            { 
                if(_value != value)
                {
                    GlobalObjectManager.EventBus.Publish<AppAlarmTriggerEvent, AppAlarmTriggerEventArgs>(this, new AppAlarmTriggerEventArgs(this));
                }
                _value = value;
            }
        }
    }
}
