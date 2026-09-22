using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.AppAttribute.Validate
{
    public class AppStringRangeAttribute : ValidateBaseAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public override string ErrorInfo
        {
            get => base.ErrorInfo ?? $"字符串长度范围应为{MinLength}-{MaxLength}";
            set => base.ErrorInfo = value;
        }
        public override bool Validate(object value)
        {
            var length = value.ToString().Length;
            return length >= MinLength && length <= MaxLength;
        }
    }
}
