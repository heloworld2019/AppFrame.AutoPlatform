using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppModbusTcpClientManager : AppListBase<AppModbusTcpClientElement>
    {
        private static AppModbusTcpClientManager _instance;
        private AppModbusTcpClientManager()
        {

        }
        public static AppModbusTcpClientManager Instance => Create();
        private static AppModbusTcpClientManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppModbusTcpClientManager());
            }
        }
        public override AppModbusTcpClientElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
