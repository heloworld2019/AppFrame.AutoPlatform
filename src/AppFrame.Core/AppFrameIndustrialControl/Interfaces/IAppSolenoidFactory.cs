using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameIndustrialControl.Interfaces
{
    public interface IAppSolenoidFactory
    {
        IAppSolenoid CreateSolenoid(AppSolenoidElement appSolenoidElement);
    }
}
