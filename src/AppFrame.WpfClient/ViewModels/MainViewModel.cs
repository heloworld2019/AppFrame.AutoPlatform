using AppFrame.Application.Configs;
using AppFrame.Application.Vendors;
using AppFrame.Infrastructrue.DependencyInjection;
using AppFrame.WpfClient.Common;
using AppFrame.WpfClient.Common.DependencyInjection;
using AppFrame.WpfClient.Common.Interfaces;
using AppFrame.WpfClient.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppFrame.WpfClient.ViewModels
{
    public class MainViewModel : BindableBase, IStartupConfiguration
    {
        public MainViewModel(IRegionManager region, AppServiceProvider appIoc)
        {
            _region = region;
            _appIoc = appIoc;
            _appConfigService = _appIoc.Resolve<IAppConfigService>();
        }
        private readonly IRegionManager _region;
        private readonly AppServiceProvider _appIoc;
        private readonly IAppConfigService _appConfigService;
        private ObservableCollection<MenuBar> _menuBars;
        private string _currentViewIcon;
        private string _currentViewTitle;
        public string CurrentViewIcon
        {
            get { return _currentViewIcon; }
            set { _currentViewIcon = value; RaisePropertyChanged(); }
        }
        public string CurrentViewTitle
        {
            get { return _currentViewTitle; }
            set { _currentViewTitle = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return _menuBars; }
            set { _menuBars = value; RaisePropertyChanged(); }
        }
        public ICommand NavigationSwitchCommand => new DelegateCommand<MenuBar>(NavigationSwitch);

        private void NavigationSwitch(MenuBar menu)
        {
            if (menu == null || string.IsNullOrEmpty(menu.NavigateViewName)) return;
            _region.Regions[RegionNameManager.MainRegionName]?.RequestNavigate(menu.NavigateViewName);
            CurrentViewIcon = menu.Icon;
            CurrentViewTitle = menu.Title;
            foreach (var item in MenuBars)
            {
                item.TitleColor = "White";
            }
            menu.TitleColor = "#f46b45";
        }

        private void GeneralMenu()
        {
            MenuBars = new ObservableCollection<MenuBar>()
            {
                new MenuBar("\ue667","控制台","IndexView","#f46b45"),
                new MenuBar("\ue603","设备操作","OperationView","White"),
                new MenuBar("\ue69b","参数维护","ParameterSettingView", "White"),
                new MenuBar("\ue600","数据统计","DataStatisticsView", "White"),
                new MenuBar("\ue60c","日志信息","LoggerView", "White"),
                new MenuBar("\ue66e","用户管理","UserManagerView", "White"),
                new MenuBar("\ue619","系统设置","SystemConfigView", "White"),
                new MenuBar("\ue71f","版本","VersionManagerView", "White"),
                new MenuBar("\ue604","关于","AboutView", "White"),
            };
        }

        public void StartConfig()
        {
            GeneralMenu();
            _appConfigService.LoadConfig(); //加载系统配置
            _appConfigService.LoadTaskConfig(); //加载系统任务配置
            _region.Regions[RegionNameManager.MainRegionName]?.RequestNavigate("IndexView");
            CurrentViewIcon = "\ue667";
            CurrentViewTitle = "控制台";
        }
    }
}
