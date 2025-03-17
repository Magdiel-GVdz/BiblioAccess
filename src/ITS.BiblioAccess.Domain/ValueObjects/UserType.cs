using System.ComponentModel;

namespace ITS.BiblioAccess.Domain.ValueObjects;

public enum UserType
{
    [Description("Estudiante")]
    Student,
    [Description("Docente")]
    Docent,
    [Description("No Docente")]
    Admin,
    [Description("Visitante")]
    Visitor
}

public static class UserTypeExtensions
{
    public static string GetEnumDescription(this UserType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute != null ? attribute.Description : value.ToString();
    }
}
