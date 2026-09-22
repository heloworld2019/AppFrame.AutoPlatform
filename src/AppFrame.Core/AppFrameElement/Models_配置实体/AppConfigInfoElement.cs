using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    public class AppConfigInfoElement : AppConfigParameter
    {
        public AppConfigInfoElement()
        {

        }
        public AppConfigInfoElement(int index,
                                    int parameterId,
                                    string parameterName,
                                    string parameterGroupName,
                                    string parameterDescription,
                                    string parameterCategory,
                                    bool enable,
                                    string configName,
                                    string createTime, 
                                    string updateTime,
                                    bool isCurrent = false)
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            ConfigName = configName;
            CreateTime = createTime;
            UpdateTime = updateTime;
            IsCurrent = isCurrent;
        }
        public string ConfigName { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public bool IsCurrent { get; set; }
    }
}
