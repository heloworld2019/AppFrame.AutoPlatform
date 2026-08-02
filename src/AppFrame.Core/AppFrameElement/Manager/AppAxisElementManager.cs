using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppAxisElementManager : AppListBase<AppAxisElement>
    {
        private static AppAxisElementManager _instance;
        private AppAxisElementManager()
        {

        }
        public static AppAxisElementManager Instance => Create();
        private static AppAxisElementManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppAxisElementManager());
            }
        }
        public override AppAxisElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
