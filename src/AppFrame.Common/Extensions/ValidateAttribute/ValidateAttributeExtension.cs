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

//  s扩展方法：直接对象.Validate()调用，使用简洁
//支持一个属性多个校验特性，循环遍历所有特性全部校验，收集全部错误，不是遇到第一个错误就直接返回
//同时读取 Public、NonPublic 属性，私有属性上的校验特性也能生效
//返回拼接后的错误字符串，适合弹窗、日志输出