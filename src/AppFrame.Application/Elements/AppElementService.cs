using AppFrame.Common.AppResult;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameException.AppElements;
using AppFrame.Entity.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Elements
{
    public class AppElementService : GlobalObjectManager, IAppElementService
    {
        private readonly IMapper _mapper;

        public AppElementService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public DataResult<AppAlarmDto> UpdateAlarm(AppAlarmDto dto)
        {
            var model = AppConfigs.AppAlarm.FirstOrDefault(p => p.ParameterName.Equals(dto.ParameterName));
            if (model == null) throw new ElementNotExistException($"配置参数{dto.ParameterName}不存在,无法更新!");
            model.AlarmName = dto.AlarmName;
            model.ParameterDescription = dto.ParameterDescription;
            model.Value = dto.Value;
            model.Enable = dto.Enable;
            model.Index = dto.Index;
            model.AlarmDegree = dto.AlarmDegree;
            var alarm = _mapper.Map<AppAlarmDto>(model);
            return new DataResult<AppAlarmDto>(alarm);
        }

        public DataResult<AppDataDto> UpdateData(AppDataDto dto)
        {
            var model = AppConfigs.AppData.FirstOrDefault(p => p.ParameterName.Equals(dto.ParameterName));
            if (model == null) throw new ElementNotExistException($"配置参数{dto.ParameterName}不存在,无法更新!");
            model.DataName = dto.DataName;
            model.ParameterDescription = dto.ParameterDescription;
            model.Value = dto.Value;
            model.Enable = dto.Enable;
            model.Index = dto.Index;
            var data = _mapper.Map<AppDataDto>(model);
            return new DataResult<AppDataDto>(data);
        }

        public DataResult<AppInputDto> UpdateInput(AppInputDto dto)
        {
            var model = AppConfigs.AppInput.FirstOrDefault(p => p.ParameterName.Equals(dto.ParameterName));
            if (model == null) throw new ElementNotExistException($"配置参数{dto.ParameterName}不存在,无法更新!");
            model.InputName = dto.InputName;
            model.ParameterDescription = dto.ParameterDescription;
            model.Enable = dto.Enable;
            model.Index = dto.Index;
            var input = _mapper.Map<AppInputDto>(model);
            return new DataResult<AppInputDto>(input);
        }

        public DataResult<AppOutputDto> UpdateOutput(AppOutputDto dto)
        {
            var model = AppConfigs.AppOutput.FirstOrDefault(p => p.ParameterName.Equals(dto.ParameterName));
            if (model == null) throw new ElementNotExistException($"配置参数{dto.ParameterName}不存在,无法更新!");
            model.OutputName = dto.OutputName;
            model.ParameterDescription = dto.ParameterDescription;
            model.Value = dto.Value;
            model.Enable = dto.Enable;
            model.Index = dto.Index;
            var output = _mapper.Map<AppOutputDto>(model);
            return new DataResult<AppOutputDto>(output);
        }
        public DataResult<List<AppAlarmDto>> GetAppAlarms()
        {
            var dtos = new List<AppAlarmDto>();
            foreach (var item in AppConfigs.AppAlarm)
            {
                dtos.Add(_mapper.Map<AppAlarmDto>(item));
            }
            return new DataResult<List<AppAlarmDto>>(dtos);
        }
        public DataResult<List<AppDataDto>> GetAppDatas()
        {
            var dtos = new List<AppDataDto>();
            foreach (var item in AppConfigs.AppData)
            {
                dtos.Add(_mapper.Map<AppDataDto>(item));
            }
            return new DataResult<List<AppDataDto>>(dtos);
        }
        public DataResult<List<AppInputDto>> GetAppInputs()
        {
            var dtos = new List<AppInputDto>();
            foreach (var item in AppConfigs.AppInput)
            {
                dtos.Add(_mapper.Map<AppInputDto>(item));
            }
            return new DataResult<List<AppInputDto>>(dtos);
        }
        public DataResult<List<AppOutputDto>> GetAppOutputs()
        {
            var dtos = new List<AppOutputDto>();
            foreach (var item in AppConfigs.AppOutput)
            {
                dtos.Add(_mapper.Map<AppOutputDto>(item));
            }
            return new DataResult<List<AppOutputDto>>(dtos);
        }
    }
}
