using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Manager;
using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameIndustrialControl.Models
{
    public class AppSolenoidProvider : AppListBase<IAppSolenoid>
    {
        private static AppSolenoidProvider _instance;
        private AppSolenoidProvider()
        {

        }
        public static AppSolenoidProvider Instance => Create();
        private static AppSolenoidProvider Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppSolenoidProvider());
            }
        }
        public override IAppSolenoid this[string name] => _items.FirstOrDefault(p => p.AppSolenoidConfig.ParameterName.Equals(name));
    }
}
