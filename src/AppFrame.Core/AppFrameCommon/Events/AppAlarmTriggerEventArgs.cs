using AppFrame.Core.AppFrameElement.Models;
using AppFrame.Core.AppFrameEventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameCommon.Events
{
    public class AppAlarmTriggerEventArgs : AppEventArgsBase<AppAlarmElement>
    {
        public AppAlarmTriggerEventArgs(AppAlarmElement appAlarmElement)
        {
            Value = appAlarmElement;
        }
    }
}
