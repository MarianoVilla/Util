using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GeneralUtil
{
    public static class EnumUtil
    {
        public static string ToDescriptionString<T>(this T enumVal) where T : Enum
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])enumVal
               .GetType()
               .GetField(enumVal.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
