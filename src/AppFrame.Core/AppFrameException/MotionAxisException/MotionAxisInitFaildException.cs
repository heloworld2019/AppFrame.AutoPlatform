using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.MotionAxisException
{
    public class MotionAxisInitFaildException : ApplicationException
    {
        public MotionAxisInitFaildException():base("初始化控制卡失败")
        {
        }

        public MotionAxisInitFaildException(string message) : base(message)
        {
        }

        public MotionAxisInitFaildException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MotionAxisInitFaildException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
