using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppNetServerManager : AppListBase<AppNetServerElement>
    {
        private static AppNetServerManager _instance;
        private AppNetServerManager()
        {

        }
        public static AppNetServerManager Instance => Create();
        private static AppNetServerManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppNetServerManager());
            }
        }
        public override AppNetServerElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
