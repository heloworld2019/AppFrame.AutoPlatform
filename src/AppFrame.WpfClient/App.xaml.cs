using AppFrame.Application.Configs;
using AppFrame.Infrastructrue.DependencyInjection;
using AppFrame.WpfClient.Common.DependencyInjection;
using AppFrame.WpfClient.Common.Interfaces;
using AppFrame.WpfClient.ViewModels;
using AppFrame.WpfClient.Views;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AppFrame.WpfClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        private IAppConfigService _appConfigService;
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }
        protected override void OnInitialized()
        {
            if(Current.MainWindow.DataContext is IStartupConfiguration startup)
            {
                startup.StartConfig();
            }
            base.OnInitialized();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<AppServiceProvider>();

            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<OperationView, OperationViewModel>();
            containerRegistry.RegisterForNavigation<ParameterSettingView, ParameterSettingViewModel>();
            containerRegistry.RegisterForNavigation<DataStatisticsView, DataStatisticsViewModel>();
            containerRegistry.RegisterForNavigation<LoggerView, LoggerViewModel>();
            containerRegistry.RegisterForNavigation<UserManagerView, UserManagerViewModel>();
            containerRegistry.RegisterForNavigation<SystemConfigView, SystemConfigViewModel>();
            containerRegistry.RegisterForNavigation<VersionManagerView, VersionManagerViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AppIoc _appIoc = Container.Resolve<AppIoc>();
            _appConfigService = _appIoc.Resove<IAppConfigService>();
            _appConfigService.SaveConfig();
            _appConfigService.SaveTaskConfig();
        }
    }
}
