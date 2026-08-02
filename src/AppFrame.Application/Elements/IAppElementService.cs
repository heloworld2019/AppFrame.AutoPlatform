using AppFrame.Common.AppResult;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Elements
{
    public interface IAppElementService
    {
        DataResult<List<AppInputDto>> GetAppInputs();
        DataResult<List<AppOutputDto>> GetAppOutputs();
        DataResult<List<AppAlarmDto>> GetAppAlarms();
        DataResult<List<AppDataDto>> GetAppDatas();
        DataResult<AppInputDto> UpdateInput(AppInputDto dto);
        DataResult<AppOutputDto> UpdateOutput(AppOutputDto dto);
        DataResult<AppAlarmDto> UpdateAlarm(AppAlarmDto dto);
        DataResult<AppDataDto> UpdateData(AppDataDto dto);
    }
}
