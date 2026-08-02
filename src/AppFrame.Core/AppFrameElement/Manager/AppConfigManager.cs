using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    /// <summary>
    /// 管理系统所有的配置
    /// </summary>
    public sealed class AppConfigManager
    {
        private static readonly object _lock = new object();
        private static AppConfigManager _instance;
        private AppConfigManager()
        {

        }
        public static AppConfigManager Instance => Create();
        private static AppConfigManager Create()
        {
            lock (_lock)
            {
                return _instance ?? (_instance = new AppConfigManager());
            }
        }
        public AppConfigInfoManager AppConfigInfo { get; private set; } = AppConfigInfoManager.Instance;
        public AppInputManager AppInput { get; private set; } = AppInputManager.Instance;
        public AppAlarmManager AppAlarm { get; private set; } = AppAlarmManager.Instance;
        public AppOutputManager AppOutput { get; private set; } = AppOutputManager.Instance;
        public AppDataManager AppData { get; private set; } = AppDataManager.Instance;
        public AppModbusRtuManager AppModbusRtu { get; private set; } = AppModbusRtuManager.Instance;
        public AppModbusTcpClientManager AppModbusTcpClient { get; private set; } = AppModbusTcpClientManager.Instance;
        public AppModbusTcpServerManager AppModbusTcpServer { get; private set; } = AppModbusTcpServerManager.Instance;
        public AppNetClientManager AppNetClient { get; private set; } = AppNetClientManager.Instance;
        public AppNetServerManager AppNetServer { get; private set; } = AppNetServerManager.Instance;
        public AppAxisElementManager AppAxisConfigs { get; private set; } = AppAxisElementManager.Instance;
        public AppSolenoidElementManager AppSolenoidConfigs { get; private set; } = AppSolenoidElementManager.Instance;
        public AppProductManager AppProducts { get; private set; } = AppProductManager.Instance;
        public AppVendorManager AppVendors { get; private set; } = AppVendorManager.Instance;
    }
}
