using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.MotionAxisException
{
    public class MotionAxisMasterResetTimeoutException : ApplicationException
    {
        public MotionAxisMasterResetTimeoutException() :base("总线复位超时")
        {
        }

        public MotionAxisMasterResetTimeoutException(string message) : base(message)
        {
        }

        public MotionAxisMasterResetTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MotionAxisMasterResetTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
