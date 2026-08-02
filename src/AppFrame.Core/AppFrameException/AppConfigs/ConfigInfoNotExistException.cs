using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.AppConfigs
{
    public class ConfigInfoNotExistException : ApplicationException
    {
        public ConfigInfoNotExistException() : base("配置文件信息不存在或参数错误,请检查!")
        {
        }

        public ConfigInfoNotExistException(string message) : base(message)
        {
        }

        public ConfigInfoNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigInfoNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
