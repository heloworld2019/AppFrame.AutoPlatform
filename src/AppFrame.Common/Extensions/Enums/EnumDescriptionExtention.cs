using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

// 此方法用于获取枚举值的描述信息，使用了扩展方法的方式，可以直接在枚举值上调用 GetDescription() 方法来获取描述信息。该方法首先获取枚举值的类型，然后通过反射获取对应的字段信息，接着判断该字段是否定义了 DescriptionAttribute 特性，如果定义了，则获取该特性的实例并返回其 Description 属性的值；如果没有定义，则返回枚举值的字符串表示。
namespace AppFrame.Common.Extensions.Enums
{
    public static class EnumDescriptionExtention
    {
        public static string GetDescription(this Enum enumName) // 根据枚举值获取描述
        {
            Type type = enumName.GetType();     
            FieldInfo field = type.GetField(enumName.ToString());      // 获取枚举值对应的字段信息
            if (field.IsDefined(typeof(DescriptionAttribute), true))   // 判断字段是否定义了 DescriptionAttribute 特性
            {
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute), true); // 获取 DescriptionAttribute 特性实例
                return descriptionAttribute.Description;
            }
            return enumName.ToString();
        }
    }
}
