using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameIndustrialControl.Models
{
    public class AppAxisProvider: AppListBase<IAppAxis>
    {
        private static AppAxisProvider _instance;
        private AppAxisProvider()
        {

        }
        public static AppAxisProvider Instance => Create();
        private static AppAxisProvider Create()
        {
            lock (_locker)
            {
                return _instance ?? (_instance = new AppAxisProvider());
            }
        }
        public override IAppAxis this[string name] => _items.FirstOrDefault(p => p.AppAxisConfig.ParameterName.Equals(name));  //  前一个=>是表达式体 ，后一个=>是lambda表达式，前一个是索引器重载，后一个是lambda表达式的语法
    }
}
