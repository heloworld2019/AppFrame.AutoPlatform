using AppFrame.Common.AppList;
using AppFrame.Core.AppFrameCommand.Interfaces;
using AppFrame.Core.AppFrameElement.Manager;
using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommand.Manager
{
    /// <summary>
    /// 命令管理器
    /// </summary>
    public class AppCommandManager : AppListBase<IAppCommand>
    {
        public AppCommandManager()
        {

        }
        public IAppCommand this[Guid guid] => _items.FirstOrDefault(p => p.CommandGuid.Equals(guid));
    }
}
