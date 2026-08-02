using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppOutputManager : AppListBase<AppOutputElement>
    {
        private static AppOutputManager _instance;
        private AppOutputManager()
        {

        }
        public static AppOutputManager Instance => Create();
        private static AppOutputManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppOutputManager());
            }
        }
        public override AppOutputElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
