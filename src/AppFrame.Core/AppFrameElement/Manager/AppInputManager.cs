using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameElement.Manager
{
    public class AppInputManager : AppListBase<AppInputElement>
    {
        private static AppInputManager _instance;
        private AppInputManager()
        {

        }
        public static AppInputManager Instance => Create();
        private static AppInputManager Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppInputManager());
            }
        }
        public override AppInputElement this[string name] => _items.FirstOrDefault(p => p.ParameterName.Equals(name));
    }
}
