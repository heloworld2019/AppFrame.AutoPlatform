using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameIndustrialControl
{
    public class AppSolenoidFactory : IAppSolenoidFactory
    {
        public IAppSolenoid CreateSolenoid(AppSolenoidElement appSolenoidElement)
        {
            return new AppSolenoid(appSolenoidElement);
        }
    }
}
