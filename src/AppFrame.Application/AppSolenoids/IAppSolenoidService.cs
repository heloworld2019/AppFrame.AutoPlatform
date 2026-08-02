using AppFrame.Common.AppResult;
using AppFrame.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.AppSolenoids
{
    public interface IAppSolenoidService
    {
        DataResult<List<AppSolenoidDto>> GetAllSolenoidCongfigs();
        DataResult<AppSolenoidDto> UpdateSolenoidConfig(AppSolenoidDto dto);
        void ActionGo(AppSolenoidDto dto);
        void ActionBack(AppSolenoidDto dto);
        Task<bool> ActionGoAsync(AppSolenoidDto dto);
        Task<bool> ActionBackAsync(AppSolenoidDto dto);
    }
}
