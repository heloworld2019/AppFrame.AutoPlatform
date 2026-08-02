using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.MotionAxisException
{
    public class MotionAxisGetCardInfoFaildException : ApplicationException
    {
        public MotionAxisGetCardInfoFaildException() : base("获取控制卡信息失败")
        {
        }

        public MotionAxisGetCardInfoFaildException(string message) : base(message)
        {
        }

        public MotionAxisGetCardInfoFaildException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MotionAxisGetCardInfoFaildException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
