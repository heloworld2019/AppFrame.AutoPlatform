using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.CommunicationException
{
    public class ModbusTcpConfigException : ApplicationException
    {
        public ModbusTcpConfigException() : base("ModbusTcp配置参数错误,请检查!")
        {
        }

        public ModbusTcpConfigException(string message) : base(message)
        {
        }

        public ModbusTcpConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModbusTcpConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
