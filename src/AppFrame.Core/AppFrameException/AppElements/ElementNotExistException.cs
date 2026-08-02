using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameException.AppElements
{
    public class ElementNotExistException : ApplicationException
    {
        public ElementNotExistException() : base("不存在的配置参数,无法更新!")
        {
        }

        public ElementNotExistException(string message) : base(message)
        {
        }

        public ElementNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ElementNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
