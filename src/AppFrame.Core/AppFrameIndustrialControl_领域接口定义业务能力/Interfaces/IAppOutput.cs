using AppFrame.Core.AppFrameElement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameIndustrialControl_领域接口定义业务能力.Interfaces
{
    internal interface IAppOutput
    {
        //关联静态配置实体
        AppOutputElement OutputConfig { get; }
        //硬件实时反馈值
        bool RealValue { get; }

        //DO业务动作契约
        void SetOn();
        void SetOff();
        void SetValue(bool val);




    }
}
