using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.SloenoidException
{
    public class SolenoidNotExistException : ApplicationException
    {
        public SolenoidNotExistException() : base("不存在的阀")
        {
        }

        public SolenoidNotExistException(string message) : base(message)
        {
        }

        public SolenoidNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SolenoidNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
