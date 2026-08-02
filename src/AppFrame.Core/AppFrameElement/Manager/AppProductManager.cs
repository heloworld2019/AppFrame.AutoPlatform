using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppProductManager : AppListBase<AppProductElement>
    {
        private static AppProductManager _instance;
        private AppProductManager()
        {

        }
        public static AppProductManager Instance => Create();
        private static AppProductManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppProductManager());
            }
        }
        public override AppProductElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
