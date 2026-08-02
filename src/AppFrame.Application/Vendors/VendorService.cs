using AppFrame.Common.AppResult;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Entity.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Vendors
{
    public class VendorService : GlobalObjectManager, IVendorService
    {
        private readonly IMapper _mapper;

        public VendorService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public DataResult<AppVendorDto> GetAppVendor()
        {
            //var dto = new AppVendorDto();
            //var model = _mapper.Map<AppVendorElement>(dto);
            var model = AppConfigs.AppVendors.FirstOrDefault(p => p.IsCurrent);
            if (model == null)
                return new DataResult<AppVendorDto>("厂商信息不存在");
            var result = _mapper.Map<AppVendorDto>(model);
            return new DataResult<AppVendorDto>(result);
        }
    }
}
