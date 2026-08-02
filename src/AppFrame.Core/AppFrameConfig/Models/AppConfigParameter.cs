using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Core.AppFrameConfig.Models
{
    /// <summary>
    /// 描述一个配置参数的基本属性
    /// 所有需要配置的参数都要实现此抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AppConfigParameter
    {
        public int Index { get; set; }
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public string ParameterCategoryName { get; set; }
        public string ParameterGroupName { get; set; }
        public string ParameterDescription { get; set; }        
        public bool Enable { get; set; }
    }
}
