using AppFrame.Core.AppFrameCommand.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameCommand
{
    public class AppCommandFactory : IAppCommandFactory
    {
        public IAppCommand CreateAxisAbsCommand(string parameterName, double position, Guid commandGuid)
        {
            return new AxisAbsCommand(parameterName, position, commandGuid);
        }

        public IAppCommand CreateAxisHomeCommand(string parameterName, Guid commandGuid)
        {
            return new AxisHomeCommand(parameterName, commandGuid);
        }

        public IAppCommand CreateAxisRelCommand(string parameterName, double distance, Guid commandGuid)
        {
            return new AxisRelCommand(parameterName, distance, commandGuid);
        }

        public IAppCommand CreateDelayCommand(double delayTime, Guid commandGuid)
        {
            return new DelayCommand(delayTime, commandGuid);
        }

        public IAppCommand CreateSolenoidBackCommand(string parameterName, Guid commandGuid)
        {
            return new SolenoidBackCommand(parameterName, commandGuid);
        }

        public IAppCommand CreateSolenoidGoCommand(string parameterName, Guid commandGuid)
        {
            return new SolenoidGoCommand(parameterName, commandGuid);
        }

        public IAppCommand CreateWaitInputCommand(string parameterName, double timeout,bool condition, Guid commandGuid)
        {
            return new WaitInputCommand(parameterName, timeout, condition, commandGuid);
        }

        public IAppCommand CreateWriteOutputCommand(string parameterName, bool value, double timeout, Guid commandGuid)
        {
            return new WriteOutputCommand(parameterName, value, timeout, commandGuid);
        }

    }
}
