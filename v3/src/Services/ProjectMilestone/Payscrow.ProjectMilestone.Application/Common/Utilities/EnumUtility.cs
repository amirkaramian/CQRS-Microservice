using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Payscrow.ProjectMilestone.Application.Common.Utilities
{
    public static class EnumUtility
    {
        /// <summary>
        /// Tries to parse a string into an enum honoring EnumMemberAttribute if present
        /// </summary>
        public static bool TryParseWithMemberName<TEnum>(string value, out TEnum result) where TEnum : struct
        {
            result = default;

            if (string.IsNullOrEmpty(value))
                return false;

            Type enumType = typeof(TEnum);

            foreach (string name in Enum.GetNames(typeof(TEnum)))
            {
                if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    result = Enum.Parse<TEnum>(name);
                    return true;
                }

                EnumMemberAttribute memberAttribute
                    = enumType.GetField(name).GetCustomAttribute(typeof(EnumMemberAttribute)) as EnumMemberAttribute;

                if (memberAttribute is null)
                    continue;

                if (memberAttribute.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    result = Enum.Parse<TEnum>(name);
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Gets the enum value from a string honoring the EnumMemberAttribute if present 
        /// </summary>
        public static TEnum? GetEnumValueOrDefault<TEnum>(string value) where TEnum : struct
        {
            if (TryParseWithMemberName(value, out TEnum result))
                return result;

            return default;
        }
    }
}
