using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.MotionAxisException
{
    public class MotionAxisSlaveNotFoundException : ApplicationException
    {
        public MotionAxisSlaveNotFoundException() :base("控制卡未连接从站或未通过motion扫描下载从站配置信息")
        {
        }

        public MotionAxisSlaveNotFoundException(string message) : base(message)
        {
        }

        public MotionAxisSlaveNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MotionAxisSlaveNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
