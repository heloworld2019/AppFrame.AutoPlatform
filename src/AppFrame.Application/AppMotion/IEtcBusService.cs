using AppFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.AppMotion
{
    public interface IEtcBusService
    {
        MotionAxisBusState GetBusState();
        Task InitMotionCardAsync();
        Task SoftResetAsync();
        Task CoolResetAsync();
        void EStop();
        void ClearBusError();
    }
}
