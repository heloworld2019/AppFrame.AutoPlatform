using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppAttribute.Validate
{
    public class AppRequiredAttribute : ValidateBaseAttribute
    {
        public override string ErrorInfo
        {
            get => base.ErrorInfo ?? "属性不能为空";
            set => base.ErrorInfo = value;
        }
        public override bool Validate(object value)
        {
            if (value.GetType().Name.Equals("String")) return !string.IsNullOrWhiteSpace(value.ToString());
            return !(value == null);
        }
    }
}
