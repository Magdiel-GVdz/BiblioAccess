using System.ComponentModel;

namespace ITS.BiblioAccess.Domain.ValueObjects;

public enum Gender
{
    [Description("Hombre")]
    Male,

    [Description("Mujer")]
    Female,

    [Description("NA")]
    NA
}

public static class GenderExtensions
{
    public static string GetEnumDescription(this Gender value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute != null ? attribute.Description : value.ToString();
    }
}
