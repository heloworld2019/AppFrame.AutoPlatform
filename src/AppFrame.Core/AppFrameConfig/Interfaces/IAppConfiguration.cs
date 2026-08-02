using AppFrame.Core.AppFrameConfig.Models;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameConfig.Interfaces
{
    /// <summary>
    /// 系统配置接口
    /// </summary>
    public interface IAppConfiguration
    {
        void LoadConfig(string configName = "");
        void SaveConfig();
        void CreateConfig(string configName);
        void RemoveConfig(string configName);
        void RenameConfig(string oldName,string newName);
    }
}
