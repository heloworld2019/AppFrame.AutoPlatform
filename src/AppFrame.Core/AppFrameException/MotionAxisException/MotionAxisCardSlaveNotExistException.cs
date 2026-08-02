using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.MotionAxisException
{
    public class MotionAxisCardNotExistException : ApplicationException
    {
        public MotionAxisCardNotExistException() : base("控制卡不存在,请检查!")
        {
        }

        public MotionAxisCardNotExistException(string message) : base(message)
        {
        }

        public MotionAxisCardNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MotionAxisCardNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
