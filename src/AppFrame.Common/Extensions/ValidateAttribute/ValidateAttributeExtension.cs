using AppFrame.Common.AppAttribute.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.Extensions.ValidateAttribute
{
    public static class ValidateAttributeExtension
    {
        public static string Validate<T>(this T t) where T: class
        {
            Type type = t.GetType();
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var errorInfos = new List<string>();
            foreach (var propertyInfo in propertyInfos)
            {
                if (!propertyInfo.IsDefined(typeof(ValidateBaseAttribute))) continue; //判断属性是否具有此特性
                foreach (ValidateBaseAttribute item in propertyInfo.GetCustomAttributes<ValidateBaseAttribute>())
                {
                    if (!item.Validate(propertyInfo.GetValue(t, null)))
                    {
                        errorInfos.Add($"{propertyInfo.Name}:{item.ErrorInfo}");
                    }
                }
            }
            if (errorInfos.Count > 0)
                return string.Join("/", errorInfos);
            return string.Empty;
        }
    }
}
