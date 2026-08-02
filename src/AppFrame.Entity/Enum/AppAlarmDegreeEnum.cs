using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Entity.Enum
{
    /// <summary>
    /// 报警等级
    /// </summary>
    public enum AppAlarmDegreeEnum
    {
        [Display(Name = "none")]
        None,
        [Display(Name = "提示")]
        Info,
        [Display(Name = "警告")]
        Warn,
        [Display(Name = "错误")]
        Error,
    }
}
