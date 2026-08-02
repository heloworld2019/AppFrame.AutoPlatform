using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppModbusRtuManager : AppListBase<AppModbusRtuElement>
    {
        private static AppModbusRtuManager _instance;
        private AppModbusRtuManager()
        {

        }
        public static AppModbusRtuManager Instance => Create();
        private static AppModbusRtuManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppModbusRtuManager());
            }
        }
        public override AppModbusRtuElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
