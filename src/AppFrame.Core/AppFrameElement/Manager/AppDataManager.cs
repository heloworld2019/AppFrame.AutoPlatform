using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppDataManager : AppListBase<AppDataElement>
    {
        private static AppDataManager _instance;
        private AppDataManager()
        {

        }
        public static AppDataManager Instance => Create();
        private static AppDataManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppDataManager());
            }
        }
        public override AppDataElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
