using AppFrame.Common.AppAttribute.Validate;
using AppFrame.Core.AppFrameConfig.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Models
{
    public class AppModbusRtuElement : AppConfigParameter
    {
        public AppModbusRtuElement()
        {

        }
        public AppModbusRtuElement(int index, 
                                   int parameterId, 
                                   string parameterName, 
                                   string parameterGroupName, 
                                   string parameterDescription, 
                                   string parameterCategory, 
                                   bool enable, 

                                   string portName, 
                                   int baudRate, 
                                   Parity parity, 
                                   int dataBit, 
                                   StopBits stopBit)
        {
            Index = index;
            ParameterId = parameterId;
            ParameterName = parameterName;
            ParameterGroupName = parameterGroupName;
            ParameterDescription = parameterDescription;
            ParameterCategoryName = parameterCategory;
            Enable = enable;
            PortName = portName;
            BaudRate = baudRate;
            Parity = parity;
            DataBit = dataBit;
            StopBit = stopBit;
        }
        [AppRequired(ErrorInfo = "端口名不能为空")]
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBit { get; set; }
        public StopBits StopBit { get; set; }
    }
}
