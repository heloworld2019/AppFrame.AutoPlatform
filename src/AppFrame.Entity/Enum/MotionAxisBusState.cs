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
    /// 总线状态
    /// </summary>
    public enum MotionAxisBusState
    {
        [Display(Name = "总线正常")]
        Normal,
        [Display(Name = "总线错误")]
        Error,
    }
}
