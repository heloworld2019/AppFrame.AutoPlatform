using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommand.Interfaces
{
    public interface IAppCommandFactory
    {
        IAppCommand CreateAxisHomeCommand(string parameterName, Guid commandGuid);
        IAppCommand CreateAxisAbsCommand(string parameterName, double position, Guid commandGuid);
        IAppCommand CreateAxisRelCommand(string parameterName, double distance, Guid commandGuid);


        IAppCommand CreateSolenoidGoCommand(string parameterName, Guid commandGuid);
        IAppCommand CreateSolenoidBackCommand(string parameterName, Guid commandGuid);


        IAppCommand CreateDelayCommand(double delayTime, Guid commandGuid);
        IAppCommand CreateWaitInputCommand(string parameterName, double timeout,bool condition, Guid commandGuid);
        IAppCommand CreateWriteOutputCommand(string parameterName, bool value, double timeout, Guid commandGuid);
    }
}
