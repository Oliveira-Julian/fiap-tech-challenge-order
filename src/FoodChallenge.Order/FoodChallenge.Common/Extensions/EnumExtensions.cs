using System.ComponentModel;
using System.Reflection;

namespace FoodChallenge.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field != null &&
            Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }

        return value.ToString();
    }

    public static string GetCode(this Enum value)
    {
        var type = value.GetType();
        var memInfo = type.GetMember(value.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : value.ToString();
    }
}
