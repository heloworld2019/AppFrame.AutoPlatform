using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppFrame.Common.Extensions.Enums
{
    public static class EnumDisplayExtension
    {
        private static string GetDisplay(Enum enumName, string memberName)
        {
            Type type = enumName.GetType();
            FieldInfo field = type.GetField(enumName.ToString());
            if (field.IsDefined(typeof(DisplayAttribute), true))
            {
                DisplayAttribute displayAttribute = (DisplayAttribute)field.GetCustomAttribute(typeof(DisplayAttribute), true);
                var properties = displayAttribute.GetType().GetProperties();
                var prop = properties.FirstOrDefault(p => p.Name.Equals(memberName));
                if (prop != null)
                    return prop.GetValue(displayAttribute).ToString();
            }
            return enumName.ToString();
        }
        public static string GetDisplayDescription(this Enum enumName)
        {
            return GetDisplay(enumName, "Description");
        }
        public static string GetName(this Enum enumName)
        {
            return GetDisplay(enumName, "Name");
        }
        public static string GetGroupName(this Enum enumName)
        {
            return GetDisplay(enumName, "GroupName");
        }
        public static string GetPrompt(this Enum enumName)
        {
            return GetDisplay(enumName, "Prompt");
        }
        public static string GetShortName(this Enum enumName)
        {
            return GetDisplay(enumName, "ShortName");
        }
        public static int GetOrder(this Enum enumName)
        {
            int.TryParse(GetDisplay(enumName, "Order"), out int result);
            return result;
        }
        public static bool GetAutoGenerateField(this Enum enumName)
        {
            bool.TryParse(GetDisplay(enumName, "AutoGenerateField"), out bool result);
            return result;
        }
        public static bool GetAutoGenerateFilter(this Enum enumName)
        {
            bool.TryParse(GetDisplay(enumName, "AutoGenerateFilter"), out bool result);
            return result;
        }
    }
}
