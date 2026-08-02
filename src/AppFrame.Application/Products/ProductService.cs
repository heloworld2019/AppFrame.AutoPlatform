using AppFrame.Common.AppResult;
using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Entity.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Products
{
    public class ProductService : GlobalObjectManager, IProductService
    {
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public DataResult<List<AppProductDto>> GetAllProducts()
        {
            var dots = new List<AppProductDto>();
            foreach (var item in AppConfigs.AppProducts)
            {
                dots.Add(_mapper.Map<AppProductDto>(item));
            }
            return new DataResult<List<AppProductDto>>(dots);
        }

        public DataResult<AppProductDto> GetDailyProduct()
        {
            var today = DateTime.UtcNow.ToString(ProductDateFormat);
            var model = AppConfigs.AppProducts.FirstOrDefault(p => p.Today.Equals(today));
            if (model != null)
            {
                return new DataResult<AppProductDto>(_mapper.Map<AppProductDto>(model));
            }
            else //不存在则根据最新日期生成一条数据
            {
                //找到最新日期的数据
                var maxdate = AppConfigs.AppProducts.Max(x => x.Today);
                var lastModel = AppConfigs.AppProducts.FirstOrDefault(p => p.Today.Equals(maxdate));
                var dto = _mapper.Map<AppProductDto>(lastModel);
                dto.Today = today;
                dto.DailyProgress = 0;
                dto.OkNums = 0;
                dto.NgNums = 0;
                int index = AppConfigs.AppProducts.Count;
                var newData = new AppProductElement(index, index, $"_{today}", AppFileInfo.AppProductGroupName, $"{today}的生产数据", AppFileInfo.AppProductCategoryName,
                                                  true, today, dto.ProductModel, dto.OrderNumber, dto.OrderPlan, dto.OrderProgress, dto.MonthlyPlan, dto.MonthlyProgress,
                                                  dto.DailyPlan, 1, 1, 1);
                AppConfigs.AppProducts.Add(newData);
                return new DataResult<AppProductDto>(dto);
            }
        }

        public DataResult<AppProductDto> UpdateProductData(AppProductDto dto)
        {
            var today = DateTime.UtcNow.ToString(ProductDateFormat);
            bool isToday = dto.Today.Equals(today); //判断是否是最新日期
            if (isToday)
            {
                var model = AppConfigs.AppProducts.FirstOrDefault(p => p.Today.Equals(dto.Today));
                if (model != null) 
                {
                    model.ProductModel = dto.ProductModel;
                    model.OrderNumber = dto.OrderNumber;
                    model.OrderPlan = dto.OrderPlan;
                    model.OrderProgress = dto.OrderProgress;
                    model.MonthlyPlan = dto.MonthlyPlan;
                    model.MonthlyProgress = dto.MonthlyProgress;
                    model.DailyPlan = dto.DailyPlan;
                    model.DailyProgress = dto.DailyProgress;
                    model.OkNums = dto.OkNums;
                    model.NgNums = dto.NgNums;    
                }
            }
            else //新增一条数据
            {
                int index = AppConfigs.AppProducts.Count;
                var newData = new AppProductElement(index, index, $"_{today}", AppFileInfo.AppProductGroupName, $"{today}的生产数据", AppFileInfo.AppProductCategoryName,
                                                  true, today, dto.ProductModel, dto.OrderNumber, dto.OrderPlan, dto.OrderProgress, dto.MonthlyPlan, dto.MonthlyProgress,
                                                  dto.DailyPlan, 1, 1, 1);
                AppConfigs.AppProducts.Add(newData);
                return new DataResult<AppProductDto>(_mapper.Map<AppProductDto>(newData));
            }
            return new DataResult<AppProductDto>(dto);
        }
    }
}
