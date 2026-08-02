using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.AppConfigs
{
    public class ConfigTipException : ApplicationException
    {
        public ConfigTipException():base("配置提示消息")
        {
        }

        public ConfigTipException(string message) : base(message)
        {
        }

        public ConfigTipException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigTipException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
