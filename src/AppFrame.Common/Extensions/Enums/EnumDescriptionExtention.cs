using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.Extensions.Enums
{
    public static class EnumDescriptionExtention
    {
        public static string GetDescription(this Enum enumName)
        {
            Type type = enumName.GetType();
            FieldInfo field = type.GetField(enumName.ToString());
            if (field.IsDefined(typeof(DescriptionAttribute), true))
            {
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute), true);
                return descriptionAttribute.Description;
            }
            return enumName.ToString();
        }
    }
}
