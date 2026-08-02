using AppFrame.Core.AppFrameCommon.Global;
using AppFrame.Core.AppFrameConfig.Interfaces;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Configs
{
    public class AppConfigService : GlobalObjectManager, IAppConfigService
    {
        private readonly IAppConfiguration _appConfiguration;
        private readonly IAppSolenoidFactory _factory;
        private readonly IAppCommandConfiguration _commandConfiguration;

        public AppConfigService(IAppConfiguration appConfiguration, IAppSolenoidFactory factory, IAppCommandConfiguration commandConfiguration)
        {
            _appConfiguration = appConfiguration;
            _factory = factory;
            _commandConfiguration = commandConfiguration;
        }
        public void CreateConfig(string configName)
        {
            _appConfiguration.CreateConfig(configName);
        }

        public void LoadConfig(string configName = "")
        {
            _appConfiguration.LoadConfig(configName);
            //实例化 阀
            if (string.IsNullOrWhiteSpace(configName))
            {
                AppSolenoids.Clear();
                foreach (var item in AppConfigs.AppSolenoidConfigs)
                {
                    AppSolenoids.Add(_factory.CreateSolenoid(item));
                }
            }
        }

        public void LoadTaskConfig(string configName = "")
        {
            _commandConfiguration.LoadCommandConfig(configName);
        }

        public void RemoveConfig(string configName)
        {
            _appConfiguration.RemoveConfig(configName);
        }

        public void RenameConfig(string oldName, string newName)
        {
            _appConfiguration.RenameConfig(oldName, newName);
        }

        public void SaveConfig()
        {
            _appConfiguration.SaveConfig();
        }

        public void SaveTaskConfig()
        {
            _commandConfiguration.SaveCommandConfig();
        }
    }
}
