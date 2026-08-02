using AppFrame.Common.AppResult;
using AppFrame.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Vendors
{
    public interface IVendorService
    {
        DataResult<AppVendorDto> GetAppVendor();
    }
}
