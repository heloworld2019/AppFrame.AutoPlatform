using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameIndustrialControl.Interfaces
{
    public interface IAppSolenoid
    {
        AppSolenoidElement AppSolenoidConfig { get; }
        string TipMessage { get; }
        bool GoDone { get; }
        bool BackDone { get; }
        void ActionGo();
        void ActionBack();
        Task<bool> ActionGoAsync();
        Task<bool> ActionBackAsync();
    }
}
