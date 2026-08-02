using AppFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameIndustrialControl.Interfaces
{
    public interface IAppAxisBus
    {
        MotionAxisBusState MasterState { get; }
        ushort CardId { get; }
        bool IsSoftReset { get; }
        bool IsCoolReset { get; }
        bool IsInitialized { get; }
        short BusErrorCode { get; }
        string BusErrorMessage { get; }
        Task InitMotionCardAsync();
        Task SoftResetAsync();
        Task CoolResetAsync();
        void WriteOutput(int index, bool value);
        void EStop();
        void ClearBusError();
    }
}
