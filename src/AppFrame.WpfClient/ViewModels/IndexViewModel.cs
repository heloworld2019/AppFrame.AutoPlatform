using AppFrame.AppFrameControls.Common.Global;
using AppFrame.Application.Commands;
using AppFrame.Application.Configs;
using AppFrame.Application.Products;
using AppFrame.Entity.Dto;
using AppFrame.Entity.Enum;
using AppFrame.Infrastructrue.DependencyInjection;
using AppFrame.WpfClient.Common;
using AppFrame.WpfClient.Common.DependencyInjection;
using AppFrame.WpfClient.Common.Enums;
using AppFrame.WpfClient.Models;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppFrame.WpfClient.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private readonly AppServiceProvider _appIoc;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICommandService _commandService;
        private readonly IAppConfigService _appConfigService;
        public IndexViewModel(AppServiceProvider appIoc)
        {
            _appIoc = appIoc;
            _productService = _appIoc.Resolve<IProductService>();
            _mapper = _appIoc.Resolve<IMapper>();
            _commandService = _appIoc.Resolve<ICommandService>();
            _appConfigService = _appIoc.Resolve<IAppConfigService>();
            GetProduct(); 
        }
        private ObservableCollection<DataBar> _dataBars;
        private AppProductVo _dailyProduct;
        private ObservableCollection<DeviceStationInfo> _stationInfos;
        /// <summary>
        /// 每日生产数据
        /// </summary>
        public AppProductVo DailyProduct
        {
            get { return _dailyProduct; }
            set { _dailyProduct = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 数据看板
        /// </summary>
        public ObservableCollection<DataBar> DataBars
        {
            get { return _dataBars; }
            set { _dataBars = value;  RaisePropertyChanged(); }
        }
        /// <summary>
        /// 工位运行信息
        /// </summary>
        public ObservableCollection<DeviceStationInfo> StationInfos
        {
            get { return _stationInfos; }
            set { _stationInfos = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 复位命令
        /// </summary>
        public ICommand ResetCommand => new DelegateCommand(Reset);

        private void Reset()
        {

        }

        private void GetProduct()
        {
            DataBars = new ObservableCollection<DataBar>()
            {
                new DataBar("运行周期","9.57 s","\ue662","#1c92d2","#4BC0C8"),
                new DataBar("今日生产","327","\ue61a","#4568DC","#3498db"),
                new DataBar("良品统计","326","\ue602","#267871","#11998e"),
                new DataBar("次品统计","1","\ue7cc","#F2994A","#F2C94C"),
            };
            var result = _productService.GetDailyProduct();
            if (result.Statu)
            {
                DailyProduct = _mapper.Map<AppProductVo>(result.Data);
                DataBars[(int)DataBarTitleEnum.今日生产].Content = DailyProduct.DailyProgress.ToString();
                DataBars[(int)DataBarTitleEnum.良品统计].Content = DailyProduct.OkNums.ToString();
                DataBars[(int)DataBarTitleEnum.次品统计].Content = DailyProduct.NgNums.ToString();
            }
            StationInfos = new ObservableCollection<DeviceStationInfo>()
            {
                new DeviceStationInfo("模拟工位一",DeviceStationState.Runing,"等待下游要料中","工位正常运行"),
                new DeviceStationInfo("模拟工位二",DeviceStationState.Alarming,"等待人工上料中","料盒物料已用完"),
                new DeviceStationInfo("模拟工位三",DeviceStationState.Runing,"等待上游放料中","工位正常运行"),
                new DeviceStationInfo("模拟工位四",DeviceStationState.Runing,"等待上游放料中","工位正常运行"),
                new DeviceStationInfo("模拟工位五",DeviceStationState.Alarming,"等待搬运轴放料中","搬运轴异常"),
                new DeviceStationInfo("模拟工位六",DeviceStationState.Runing,"等待上游放料中","工位正常运行"),
            };
        }
    }
}
