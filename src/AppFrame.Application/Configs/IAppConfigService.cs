using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Application.Configs
{
    public interface IAppConfigService
    {
        void LoadConfig(string configName = "");
        void SaveConfig();
        void CreateConfig(string configName);
        void RemoveConfig(string configName);
        void RenameConfig(string oldName, string newName);
        void LoadTaskConfig(string configName = "");
        void SaveTaskConfig();
    }
}
