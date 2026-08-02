using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.CommunicationException
{
    public class ModbusRtuConfigException : ApplicationException
    {
        public ModbusRtuConfigException() : base("ModbusRtu客户端配置参数错误,请检查!")
        {
        }

        public ModbusRtuConfigException(string message) : base(message)
        {
        }

        public ModbusRtuConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModbusRtuConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
