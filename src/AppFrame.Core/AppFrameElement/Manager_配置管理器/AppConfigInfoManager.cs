using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppConfigInfoManager : AppListBase<AppConfigInfoElement>
    {
        private static AppConfigInfoManager _instance;
        private AppConfigInfoManager()
        {

        }
        public static AppConfigInfoManager Instance => Create();
        private static AppConfigInfoManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppConfigInfoManager());
            }
        }
        public override AppConfigInfoElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
