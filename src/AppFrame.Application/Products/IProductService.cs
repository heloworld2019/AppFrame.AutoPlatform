using AppFrame.Common.AppResult;
using AppFrame.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Products
{
    public interface IProductService
    {
        DataResult<AppProductDto> GetDailyProduct();
        DataResult<List<AppProductDto>> GetAllProducts();
        DataResult<AppProductDto> UpdateProductData(AppProductDto dto);
    }
}
