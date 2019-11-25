using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EMIS.PatientFlow.Kiosk.Helper
{
    public static class DisplayNameAttribute
    {
        public static string GetDisplayName(this System.Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = System.Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }
    }
}
