using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppAttribute.Validate
{
    /// <summary>
    /// 校验特性 抽象类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class ValidateBaseAttribute: Attribute
    {
        public virtual string ErrorInfo { get; set; }
        public abstract bool Validate(object value);
    }
}
