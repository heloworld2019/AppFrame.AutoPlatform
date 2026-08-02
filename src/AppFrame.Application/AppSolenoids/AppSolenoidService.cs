using AppFrame.Common.AppResult;
using AppFrame.Common.Extensions.ValidateAttribute;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameException.SloenoidException;
using AppFrame.Entity.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.AppSolenoids
{
    public class AppSolenoidService : GlobalObjectManager, IAppSolenoidService
    {
        private readonly IMapper _mapper;

        public AppSolenoidService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void ActionBack(AppSolenoidDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var model = AppSolenoids[dto.ParameterName];
            if (model == null)
            {
                throw new Exception("不存在的阀");
            }
            model.ActionBack();
        }

        public async Task<bool> ActionBackAsync(AppSolenoidDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var model = AppSolenoids[dto.ParameterName];
            if (model == null)
            {
                throw new Exception("不存在的阀");
            }
            await model.ActionBackAsync();
            return true;
        }

        public void ActionGo(AppSolenoidDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var model = AppSolenoids[dto.ParameterName];
            if (model == null)
            {
                throw new SolenoidNotExistException();
            }
            model.ActionGo();
        }

        public async Task<bool> ActionGoAsync(AppSolenoidDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var model = AppSolenoids[dto.ParameterName];
            if (model == null)
            {
                throw new SolenoidNotExistException();
            }
            await model.ActionGoAsync();
            return true;
        }

        public DataResult<List<AppSolenoidDto>> GetAllSolenoidCongfigs()
        {
            var dtos = new List<AppSolenoidDto>();
            foreach (var item in AppConfigs.AppSolenoidConfigs)
            {
                dtos.Add(_mapper.Map<AppSolenoidDto>(item));
            }
            return new DataResult<List<AppSolenoidDto>>(dtos);
        }

        public DataResult<AppSolenoidDto> UpdateSolenoidConfig(AppSolenoidDto dto)
        {
            var config = _mapper.Map<AppSolenoidElement>(dto);
            var result = config.Validate(); //数据校验
            if (!string.IsNullOrWhiteSpace(result))
            {
                return new DataResult<AppSolenoidDto>(result);
            }
            var model = AppConfigs.AppSolenoidConfigs.FirstOrDefault(p => p.ParameterName.Equals(dto.ParameterName));
            if (model == null) throw new SolenoidNotExistException($"不存在的阀{dto.ParameterName},无法更新参数!");
            model.Index = dto.Index;
            model.ParameterDescription = dto.ParameterDescription;
            model.Enable = dto.Enable;
            model.SolenoidName = dto.SolenoidName;
            model.GoOutputId = dto.GoOutputId;
            model.BackOutputId = dto.BackOutputId;
            model.GoSensorInputId = dto.GoSensorInputId;
            model.BackSensorInputId = dto.BackSensorInputId;
            model.GoAlarmId = dto.GoAlarmId;
            model.BackAlarmId = dto.BackAlarmId;
            model.GoSensorShield = dto.GoSensorShield;
            model.BackSensorShield = dto.BackSensorShield;
            model.GoAlarmTime = dto.GoAlarmTime;
            model.BackAlarmTime = dto.BackAlarmTime;
            model.GoDoneDelay = dto.GoDoneDelay;
            model.BackDoneDelay = dto.BackDoneDelay;
            model.ActionTimeIfGoSensorShield = dto.ActionTimeIfGoSensorShield;
            model.ActionTimeIfBackSensorShield = dto.ActionTimeIfBackSensorShield;
            return new DataResult<AppSolenoidDto>(_mapper.Map<AppSolenoidDto>(model));
        }
    }
}
