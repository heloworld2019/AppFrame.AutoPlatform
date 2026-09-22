using AppFrame.Common.AppAttribute.Filter;
using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    /// <summary>
    /// 输入元件
    /// </summary>
    /// 

  //  静态配置：从配置文件读取。存储 IO 点位的固定参数：索引、参数 ID、InputName点位名称、启用标记、描述信息。
  //只保存静态配置，不存实时采集值。


    public class AppInputElement: AppConfigParameter
    {
        public AppInputElement()
        {

        }
        public AppInputElement(int index, 
                               int parameterId,
                               string parameterName,
                               string parameterGroupName,
                               string parameterDescription,
                               string parameterCategory,
                               bool enable,
                               string inputName
                               )
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            InputName = inputName;
        }
        public string InputName { get; set; }
        [ConfigFilter]
        public bool Value { get; set; }
    }
}
