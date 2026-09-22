using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppModbusTcpServerManager : AppListBase<AppModbusTcpServerElement>
    {
        private static AppModbusTcpServerManager _instance;
        private AppModbusTcpServerManager()
        {

        }
        public static AppModbusTcpServerManager Instance => Create();
        private static AppModbusTcpServerManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppModbusTcpServerManager());
            }
        }
        public override AppModbusTcpServerElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
