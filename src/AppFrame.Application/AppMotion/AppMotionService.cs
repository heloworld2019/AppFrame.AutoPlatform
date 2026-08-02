using AppFrame.Common.AppResult;
using AppFrame.Common.Extensions.ValidateAttribute;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Entity.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.AppMotion
{
    public class AppMotionService : GlobalObjectManager, IAppMotionService
    {
        private readonly IMapper _mapper;

        public AppMotionService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DataResult<bool> AbsoluteMove(string parameterName, double position)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.AbsoluteMove(position);
            return new DataResult<bool>(true);
        }

        public async Task<DataResult<bool>> AbsoluteMoveAsync(string parameterName, double position)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            var result = await axis.AbsoluteMoveAsync(position);
            return new DataResult<bool>(result);
        }

        public DataResult<List<AppAxisDto>> GetAllAxisConfigs()
        {
            var dtos = new List<AppAxisDto>();
            foreach (var item in AppConfigs.AppAxisConfigs)
            {
                dtos.Add(_mapper.Map<AppAxisDto>(item));
            }
            return new DataResult<List<AppAxisDto>>(dtos);
        }

        public DataResult<AppAxisRealtimeDataDto> GetAxisRealtimeData(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<AppAxisRealtimeDataDto>("轴不存在");
            var model = new AppAxisRealtimeDataDto()
            {
                CurrentVelocity = axis.CurrentVelocity,
                CurrentPosition = axis.CurrentPosition,
                CurrentPulsePosition = axis.CurrentPulsePosition,
                StatuWord = axis.StatuWord,
                ControlWord = axis.ControlWord,
                ErrorCode = axis.ErrorCode,
                ErrorMessage = axis.ErrorMessage,
                MoveDone = axis.MoveDone,
                HomeDone = axis.HomeDone,
                IsPowerOn = axis.IsPowerOn,
                IsAlarming = axis.IsAlarming,
                IsErrorStop = axis.IsErrorStop,
                PositiveLimitSwitch = axis.PositiveLimitSwitch,
                NegitiveLimitSwitch = axis.NegitiveLimitSwitch,
                HomeSwitch = axis.HomeSwitch,
                PositiveSoftLimitSwitch = axis.PositiveSoftLimitSwitch,
                NegitiveSoftLimitSwitch = axis.NegitiveSoftLimitSwitch
            };
            return new DataResult<AppAxisRealtimeDataDto>(model);
        }

        public DataResult<bool> Home(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.Home();
            return new DataResult<bool>(true);
        }

        public async Task<DataResult<bool>> HomeAsync(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            var result = await axis.HomeAsync();
            return new DataResult<bool>(result);
        }

        public DataResult<bool> NegativeInch(string parameterName, double inchValue)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.NegativeInch(inchValue);
            return new DataResult<bool>(true);
        }

        public DataResult<bool> NegativeJog(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.NegativeJog();
            return new DataResult<bool>(true);
        }

        public DataResult<bool> PositiveInch(string parameterName, double inchValue)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.PositiveInch(inchValue);
            return new DataResult<bool>(true);
        }

        public DataResult<bool> PositiveJog(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.PositiveJog();
            return new DataResult<bool>(true);
        }

        public DataResult<bool> PowerOff(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.PowerOff();
            return new DataResult<bool>(true);
        }

        public DataResult<bool> PowerOn(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.PowerOn();
            return new DataResult<bool>(true);
        }

        public DataResult<int> ReadObjectDictionary(string parameterName, string index, string subindex, ushort length)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<int>("轴不存在");
            int value = 0;
            axis.ReadObjectDictionary(index, subindex, length, ref value);
            return new DataResult<int>(value);
        }

        public DataResult<bool> RelativeMove(string parameterName, double distance)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.RelativeMove(distance);
            return new DataResult<bool>();
        }

        public async Task<DataResult<bool>> RelativeMoveAsync(string parameterName, double distance)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            await axis.RelativeMoveAsync(distance);
            return new DataResult<bool>(true);
        }

        public DataResult<bool> Reset(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.Reset();
            return new DataResult<bool>();
        }

        public DataResult<bool> SetPulseEquiv(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.SetPulseEquiv();
            return new DataResult<bool>(true);
        }

        public DataResult<bool> Stop(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.Stop();
            return new DataResult<bool>(true);
        }

        public DataResult<bool> StopJog(string parameterName)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.StopJog();
            return new DataResult<bool>(true);
        }

        public DataResult<AppAxisDto> UpdateAxisConfig(AppAxisDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var model = AppConfigs.AppAxisConfigs.FirstOrDefault(p => p.ParameterName.Equals(dto.ParameterName));
            if (model == null) return new DataResult<AppAxisDto>("轴不存在");
            var mapModel = _mapper.Map<AppAxisElement>(dto);
            var result = mapModel.Validate();
            if (!string.IsNullOrWhiteSpace(result))
            {
                return new DataResult<AppAxisDto>(result);
            }
            model.Index = dto.Index;
            model.ParameterDescription = dto.ParameterDescription;
            model.Enable = dto.Enable;
            model.CardId = dto.CardId;
            model.AxisId = dto.AxisId;
            model.AxisName = dto.AxisName;
            model.AxisUnit = dto.AxisUnit;
            model.PulseEquiv = dto.PulseEquiv;
            model.StartVelocity = dto.StartVelocity;
            model.StopVelocity = dto.StopVelocity;
            model.HomeMode = dto.HomeMode;
            model.HomeHighVelocity = dto.HomeHighVelocity;
            model.HomeLowVelocity = dto.HomeLowVelocity;
            model.HomeOffset = dto.HomeOffset;
            model.HomeTimeOut = dto.HomeTimeOut;
            model.JogVelocity = dto.JogVelocity;
            model.JogAcc = dto.JogAcc;
            model.JogDec = dto.JogDec;
            model.RunAcc = dto.RunAcc;
            model.RunDec = dto.RunDec;
            model.RunVelocity = dto.RunVelocity;
            model.StopTime = dto.StopTime;
            model.PositiveSoftLimitEnable = dto.PositiveSoftLimitEnable;
            model.NegitiveSoftLimitEnable = dto.NegitiveSoftLimitEnable;
            model.PositiveSoftLimit = dto.PositiveSoftLimit;
            model.NegitiveSoftLimit = dto.NegitiveSoftLimit;
            model.LocationTimeOut = dto.LocationTimeOut;
            return new DataResult<AppAxisDto>(_mapper.Map<AppAxisDto>(model));
        }

        public DataResult<bool> WriteObjectDictionary(string parameterName, string index, string subindex, ushort length, int value)
        {
            var axis = AppAxises.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(parameterName));
            if (axis == null) return new DataResult<bool>("轴不存在");
            axis.WriteObjectDictionary(index, subindex, length, value);
            return new DataResult<bool>(true);
        }
    }
}
