using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppAttribute.Filter
{
    /// <summary>
    /// 可以标记此特性 过滤不需要保存到配置文件的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfigFilterAttribute : Attribute
    {
        public ConfigFilterAttribute()
        {

        }
    }
}
