using AppFrame.Common.AppResult;
using AppFrame.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.AppMotion
{
    public interface IAppMotionService
    {
        DataResult<List<AppAxisDto>> GetAllAxisConfigs();
        DataResult<AppAxisDto> UpdateAxisConfig(AppAxisDto dto);
        DataResult<AppAxisRealtimeDataDto> GetAxisRealtimeData(string parameterName);
        DataResult<bool> SetPulseEquiv(string parameterName);
        DataResult<bool> PowerOn(string parameterName);
        DataResult<bool> PowerOff(string parameterName);
        DataResult<bool> Home(string parameterName);
        Task<DataResult<bool>> HomeAsync(string parameterName);
        DataResult<bool> Reset(string parameterName);
        DataResult<bool> Stop(string parameterName);
        DataResult<bool> AbsoluteMove(string parameterName, double position);
        DataResult<bool> RelativeMove(string parameterName, double distance);
        Task<DataResult<bool>> AbsoluteMoveAsync(string parameterName, double position);
        Task<DataResult<bool>> RelativeMoveAsync(string parameterName, double distance);
        DataResult<bool> PositiveJog(string parameterName);
        DataResult<bool> NegativeJog(string parameterName);
        DataResult<bool> StopJog(string parameterName);
        DataResult<bool> PositiveInch(string parameterName, double inchValue);
        DataResult<bool> NegativeInch(string parameterName, double inchValue);
        DataResult<int> ReadObjectDictionary(string parameterName, string index, string subindex, ushort length);
        DataResult<bool> WriteObjectDictionary(string parameterName, string index, string subindex, ushort length, int value);
    }
}
