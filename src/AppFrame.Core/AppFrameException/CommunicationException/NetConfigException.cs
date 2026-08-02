using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.CommunicationException
{
    public class NetConfigException : ApplicationException
    {
        public NetConfigException() : base("Net配置参数错误,请检查!")
        {
        }

        public NetConfigException(string message) : base(message)
        {
        }

        public NetConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NetConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
