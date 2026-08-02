using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Entity.Dto;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AutoMappers
{
    internal class AutoMapperProfile : MapperConfigurationExpression
    {
        public AutoMapperProfile()
        {
            CreateMap<AppVendorElement, AppVendorDto>().ReverseMap();
            CreateMap<AppProductElement, AppProductDto>().ReverseMap();
        }
    }
}
