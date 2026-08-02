using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppAlarmManager: AppListBase<AppAlarmElement>
    {
        private static AppAlarmManager _instance;
        private AppAlarmManager()
        {

        }
        public static AppAlarmManager Instance => Create();
        private static AppAlarmManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppAlarmManager());
            }
        }
        public override AppAlarmElement this[string name] => _items.FirstOrDefault(p=>p.ParameterName.Equals(name));
    }
}
