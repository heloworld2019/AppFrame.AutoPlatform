using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppNetClientManager : AppListBase<AppNetClientElement>
    {
        private static AppNetClientManager _instance;
        private AppNetClientManager()
        {

        }
        public static AppNetClientManager Instance => Create();
        private static AppNetClientManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppNetClientManager());
            }
        }
        public override AppNetClientElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
