using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.SloenoidException
{
    public class SolenoidConfigExceptoin : ApplicationException
    {
        public SolenoidConfigExceptoin():base("阀参数配置错误，请检查!")
        {
        }

        public SolenoidConfigExceptoin(string message) : base(message)
        {
        }

        public SolenoidConfigExceptoin(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SolenoidConfigExceptoin(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
