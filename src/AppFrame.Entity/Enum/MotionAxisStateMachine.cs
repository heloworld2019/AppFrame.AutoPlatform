using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppFrame.Entity.Enum
{
    /// <summary>
    /// 轴状态机
    /// </summary>
    public enum MotionAxisStateMachine
    {
        [Display(Name = "轴处于未启动状态")]
        AxisNotReady = 0,
        [Display(Name = "轴处于启动禁止状态")]
        AxisSatrtDisable,
        [Display(Name = "轴处于准备启动状态")]
        AxisSatrtReady,
        [Display(Name = "轴处于启动状态")]
        AxisStarted,
        [Display(Name = "轴处于操作使能状态")]
        AxisOpEnable,
        [Display(Name = "轴处于停止状态")]
        AxisQuickStop,
        [Display(Name = "轴处于错误触发状态")]
        AxisErrorActive,
        [Display(Name = "轴处于错误状态")]
        AxisErrorring,
    }
}
