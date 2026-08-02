using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameLogger
{
    public interface IAppLogger
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);

    }
}
