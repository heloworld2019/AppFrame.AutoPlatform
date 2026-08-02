using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameConfig.Interfaces
{
    /// <summary>
    /// 系统命令配置接口
    /// </summary>
    public interface IAppCommandConfiguration
    {
        void LoadCommandConfig(string configName = "");
        void SaveCommandConfig();
    }
}
