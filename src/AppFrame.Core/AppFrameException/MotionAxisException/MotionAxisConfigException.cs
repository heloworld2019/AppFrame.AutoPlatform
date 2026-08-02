using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.MotionAxisException
{
    public class MotionAxisConfigException : ApplicationException
    {
        public MotionAxisConfigException() : base("轴参数配置错误,请检查!")
        {
        }

        public MotionAxisConfigException(string message) : base(message)
        {
        }

        public MotionAxisConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MotionAxisConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
