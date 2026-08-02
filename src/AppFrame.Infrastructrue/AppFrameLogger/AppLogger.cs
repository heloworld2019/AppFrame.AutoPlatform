using AppFrame.Core.AppFrameLogger;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameLogger
{
    public class AppLogger : IAppLogger
    {
        private ILog _log;
        public void Debug(string message)
        {
            _log = LogManager.GetLogger("Debug");
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message); 
            } 
        }

        public void Error(string message)
        {
            _log = LogManager.GetLogger("Error");
            if (_log.IsErrorEnabled)
            {
                _log.Debug(message);
            }
        }

        public void Info(string message)
        {
            _log = LogManager.GetLogger("Info");
            if (_log.IsInfoEnabled)
            {
                _log.Debug(message);
            }
        }

        public void Warn(string message)
        {
            _log = LogManager.GetLogger("Warn");
            if (_log.IsWarnEnabled)
            {
                _log.Debug(message);
            }
        }
    }
}
