using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    /// <summary>
    /// 其他元件
    /// </summary>
    public class AppDataElement : AppConfigParameter
    {
        public AppDataElement()
        {

        }
        public AppDataElement(int index,
                              int parameterId,
                              string parameterName,
                              string parameterGroupName,
                              string parameterDescription,
                              string parameterCategory,
                              bool enable,
                              string dataName,
                              string value)
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            DataName = dataName;
            Value = value;
        }
        public string DataName { get; set; }
        public string Value { get; set; }
    }
}
