using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.MotionAxisException
{
    public class MotionAxisMasterResetFaildException : ApplicationException
    {
        public MotionAxisMasterResetFaildException() :base("错误:总线复位失败")
        {
        }

        public MotionAxisMasterResetFaildException(string message) : base(message)
        {
        }

        public MotionAxisMasterResetFaildException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MotionAxisMasterResetFaildException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
