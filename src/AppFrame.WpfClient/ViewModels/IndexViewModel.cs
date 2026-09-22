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
using ImTools;
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
using System.Windows.Threading;

namespace AppFrame.WpfClient.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private readonly AppServiceProvider _appIoc;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICommandService _commandService;
        private readonly IAppConfigService _appConfigService;

        private DispatcherTimer _simTimer; // 模拟定时器，用于模拟数据更新



        public IndexViewModel(AppServiceProvider appIoc)  // 返回指定类型的服务实例    
        {
            _appIoc = appIoc;
            _productService = _appIoc.Resolve<IProductService>();  // 解析 IProductService 接口的实现类,返回指定类型的服务实例
            _mapper = _appIoc.Resolve<IMapper>();
            _commandService = _appIoc.Resolve<ICommandService>();
            _appConfigService = _appIoc.Resolve<IAppConfigService>();
            GetProduct();
            StartSimulateRealTimeData();
        }

        private AppProductVo _dailyProduct;
        private ObservableCollection<DataBar> _dataBars;
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





        // =============================张增加 模拟数据=================================================
        #region 新增模拟实时数据

        /// <summary>
        /// 开启模拟实时数据更新（模拟控制器/业务服务推送）
        /// </summary>
        private void StartSimulateRealTimeData()
        {
            _simTimer = new DispatcherTimer();
            _simTimer.Interval = TimeSpan.FromSeconds(0.2); //1秒刷新一次
            _simTimer.Tick += SimTimer_Tick;
            _simTimer.Start();
        }

        /// <summary>
        /// 定时器Tick回调，模拟业务数据变化
        /// 只做UI层数据组装赋值，不写业务判断、不写硬件采集
        /// </summary>
        private readonly Random _rand = new Random();
        private void SimTimer_Tick(object sender, EventArgs e)
        {
            //集合为空直接跳过，防止页面初始化未完成时报错
            if (DataBars == null || DataBars.Count <= 0)
                return;

            //====模拟变化的业务数值（模拟从服务/控制器拿到的数据）====
            double simRunCycle = _rand.NextDouble() * 25;       //运行周期 0~25
            int simDailyTotal = _rand.Next(300, 900);           //今日生产
            int simOk = _rand.Next(280, 850);                   //良品
            int simNg = simDailyTotal - simOk;                          //次品

            //赋值到DataBar，复用项目现有的枚举索引方式
            DataBars[(int)DataBarTitleEnum.运行周期].Content = simRunCycle.ToString("F2");
            DataBars[(int)DataBarTitleEnum.今日生产].Content = simDailyTotal.ToString();
            DataBars[(int)DataBarTitleEnum.良品统计].Content = simOk.ToString();
            DataBars[(int)DataBarTitleEnum.次品统计].Content = simNg.ToString();
        }

        /// <summary>
        /// 页面销毁资源释放，Prism IDestructible调用
        /// </summary>
          public void Cleanup()
          {
              if (_simTimer != null)
              {
                  _simTimer.Stop();
                  _simTimer.Tick -= SimTimer_Tick;
                  _simTimer = null;
              }
          }
        #endregion

        //=============================张增加 模拟数据=================================================






































        //UI 适配、命令、DispatcherTimer、调用应用服务；把 DTO 转成 UI 可绑定 Observable 集合；UI 状态控制  禁止写核心业务规则；❌禁止直接写 PLC/Modbus/OpcUA 采集代码；❌不要写 if 业务判断逻辑







        private void GetProduct()
        {
            DataBars = new ObservableCollection<DataBar>()
            {
                new DataBar("运行周期","0","\ue662","#1c92d2","#4BC0C8"),
                new DataBar("今日生产","0","\ue61a","#4568DC","#3498db"),
                new DataBar("良品统计","0","\ue602","#267871","#11998e"),
                new DataBar("次品统计","0","\ue7cc","#F2994A","#F2C94C"),
            };
























            var result = _productService.GetDailyProduct();
            if (result.Statu)
            {
                DailyProduct = _mapper.Map<AppProductVo>(result.Data);
                DataBars[(int)DataBarTitleEnum.今日生产].Content = DailyProduct.DailyProgress.ToString();
                DataBars[(int)DataBarTitleEnum.良品统计].Content = DailyProduct.OkNums.ToString();
                DataBars[(int)DataBarTitleEnum.次品统计].Content = DailyProduct.NgNums.ToString();
            }




        //   /初始化列表  
        StationInfos = new ObservableCollection<DeviceStationInfo>()
            {
                new DeviceStationInfo("工位一",DeviceStationState.Runing,"等待下游要料中","工位正常运行"),
                new DeviceStationInfo("工位二",DeviceStationState.Alarming,"等待人工上料中","料盒物料已用完"),
                new DeviceStationInfo("工位三",DeviceStationState.Runing,"等待上游放料中","工位正常运行"),
                new DeviceStationInfo("工位四",DeviceStationState.Runing,"等待上游放料中","工位正常运行"),
                new DeviceStationInfo("工位五",DeviceStationState.Alarming,"等待搬运轴放料中","搬运轴异常"),
                new DeviceStationInfo("工位六",DeviceStationState.Runing,"等待上游放料中","工位正常运行"),
                new DeviceStationInfo("工位七",DeviceStationState.Runing,"等待上游放料中","工位正常运行"),
                new DeviceStationInfo("工位八",DeviceStationState.Runing,"等待上游放料中","工位正常运行"),

              
            };





        }
    }
}
