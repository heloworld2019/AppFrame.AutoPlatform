using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppFrame.Common.AppAttribute.Validate
{
    public class AppRegexAttribute : ValidateBaseAttribute
    {
        public override string ErrorInfo
        {
            get => base.ErrorInfo ?? "属性不符合规则";
            set => base.ErrorInfo = value;
        }
        public string RegexText { get; set; }
        public override bool Validate(object value)
        {
            return Regex.Match(value.ToString(), RegexText).Success;
        }
    }
}
