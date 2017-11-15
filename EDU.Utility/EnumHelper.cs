using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.ComponentModel;

namespace EZY.EDU.Utility
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(System.ComponentModel.DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
