using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Manager;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppVendorManager : AppListBase<AppVendorElement>
    {
        private static AppVendorManager _instance;
        private AppVendorManager()
        {

        }
        public static AppVendorManager Instance => Create();
        private static AppVendorManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppVendorManager());
            }
        }
        public override AppVendorElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
