using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppSolenoidElementManager : AppListBase<AppSolenoidElement>
    {
        private static AppSolenoidElementManager _instance;
        private AppSolenoidElementManager()
        {

        }
        public static AppSolenoidElementManager Instance => Create();
        private static AppSolenoidElementManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppSolenoidElementManager());
            }
        }
        public override AppSolenoidElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
