using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Entity.Dto;
using AppFrame.WpfClient.Models;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.WpfClient.Common.AutoMappers
{
    public class AutoMapperProfile : MapperConfigurationExpression
    {
        public AutoMapperProfile()
        {

            //CreateMap<源类型, 目标类型>().ReverseMap();
            //CreateMap < A,B >：A → B 对象转换
            //.ReverseMap()：同时支持 B → A 双向转换



            CreateMap<AppVendorElement, AppVendorDto>().ReverseMap(); // 领域业务实体 ，业务层，承载设备真实数据（AppVendorElement）与数据传输对象（AppVendorDto）之间的映射关系
            CreateMap<AppProductElement, AppProductDto>().ReverseMap();
            CreateMap<AppProductDto, AppProductVo>().ReverseMap();

            CreateMap<AppInputElement, AppInputDto>().ReverseMap(); //  输入转输入
            CreateMap<AppOutputElement, AppOutputDto>().ReverseMap(); // 输出转输出
            CreateMap<AppAlarmElement, AppAlarmDto>().ReverseMap();  // 报警转报警
            CreateMap<AppDataElement, AppDataDto>().ReverseMap();   // 数据转数据


            CreateMap<AppInputDto, AppInputVo>().ReverseMap();
            CreateMap<AppOutputDto, AppOutputVo>().ReverseMap();
            CreateMap<AppAlarmDto, AppAlarmVo>().ReverseMap();   //报警 映射
            CreateMap<AppDataDto, AppDataVo>().ReverseMap();

            CreateMap<AppSolenoidElement, AppSolenoidDto>().ReverseMap(); // 流转载体
            CreateMap<AppSolenoidDto, AppSolenoidVo>().ReverseMap();

            CreateMap<AppAxisElement, AppAxisDto>().ReverseMap();
            CreateMap<AppAxisDto, AppAxisVo>().ReverseMap();
            CreateMap<AppAxisRealtimeDataDto, AppAxisRealtimeDataVo>().ReverseMap();
        }
    }
}
