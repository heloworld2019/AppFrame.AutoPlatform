using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Core.AppFrameCommon.Events;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    /// <summary>
    /// 输出元件
    /// </summary>
    public class AppOutputElement : AppConfigParameter
    {
        public AppOutputElement()
        {

        }
        public AppOutputElement(int index,
                                int parameterId,
                                string parameterName,
                                string parameterGroupName,
                                string parameterDescription,
                                string parameterCategory,
                                bool enable,
                                string outputName
                                )
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            OutputName = outputName;
        }
        public string OutputName { get; set; }
        private bool _value;
        [ConfigFilter]
        public  bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                GlobalObjectManager.EventBus.Publish<AppOutputChangedEvent, AppOutputChangedEventArgs>(this, new AppOutputChangedEventArgs(this));
            }
        }
    }
}
