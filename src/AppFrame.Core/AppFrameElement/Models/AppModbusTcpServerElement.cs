using AppFrame.Common.AppAttribute.Validate;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameConfig.Models;
using AppFrame.Core.AppFrameElement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    public class AppModbusTcpServerElement : AppConfigParameter, IAppIpAddress
    {
        public AppModbusTcpServerElement()
        {

        }
        public AppModbusTcpServerElement(int index,
                                         int parameterId,
                                         string parameterName,
                                         string parameterGroupName,
                                         string parameterDescription,
                                         string parameterCategory,
                                         bool enable,

                                         string iPAddress,
                                         int port
                                         )
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            IpAddress = iPAddress;
            Port = port;
        }
        [AppRegex(RegexText = AppStringsResource.IpAddressMatchString, ErrorInfo = AppStringsResource.IpAddressErrorString)]
        public string IpAddress { get; set; }
        [AppValueRange(0, int.MaxValue)]
        public int Port { get; set; }
    }
}
