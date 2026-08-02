using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameIndustrialControl.Interfaces
{
    public interface IAppAxis
    {
        AppAxisElement AppAxisConfig { get; }
        MotionAxisStateMachine AxisState { get; }
        double CurrentVelocity { get; }
        double CurrentPosition { get; }
        double CurrentPulsePosition { get; }
        int StatuWord { get; }
        int ControlWord { get; }
        short ErrorCode { get; }
        string ErrorMessage { get; }
        bool MoveDone { get; }
        bool HomeDone { get; }
        bool IsPowerOn { get; }
        bool IsErrorStop { get; }
        bool IsAlarming { get; }
        bool PositiveLimitSwitch { get; }
        bool NegitiveLimitSwitch { get; }
        bool HomeSwitch { get; }
        bool PositiveSoftLimitSwitch { get; }
        bool NegitiveSoftLimitSwitch { get; }
        void SetPulseEquiv();
        void PowerOn();
        void PowerOff();
        void Home();
        Task<bool> HomeAsync();
        void Reset();
        void Stop();
        void AbsoluteMove(double position);
        void RelativeMove(double distance);
        Task<bool> AbsoluteMoveAsync(double position);
        Task<bool> RelativeMoveAsync(double distance);
        void PositiveJog();
        void NegativeJog();
        void StopJog();
        void PositiveInch(double inchValue);
        void NegativeInch(double inchValue);
        void ReadObjectDictionary(string index, string subindex, ushort length, ref int value);
        void WriteObjectDictionary(string index, string subindex, ushort length, int value); 
    }
}
