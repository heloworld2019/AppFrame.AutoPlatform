using AppFrame.Core.AppFrameIndustrialControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Infrastructrue.AppFrameIndustrialControl
{
    public class AppAxisBusFactory : IAppAxisBusFactory
    {
        public IAppAxisBus CreateAppAxisMaster(ushort cardId)
        {
            return new AppAxisBus(cardId);
        }
    }
}
