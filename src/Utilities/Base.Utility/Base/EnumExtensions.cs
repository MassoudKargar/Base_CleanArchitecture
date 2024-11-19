using System.ComponentModel.DataAnnotations;

namespace Base.Utility.Base;
public static class EnumExtensions
{
    public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException();

        return Enum.GetValues(input.GetType()).Cast<T>();
    }

    public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : Enum
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException();

        foreach (Enum value in Enum.GetValues(input.GetType()))
            if (input.HasFlag(value))
                yield return (T)value;
    }

    public static string? ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
    {
        Assert.NotNull(value, nameof(value));

        var attribute = value.GetType().GetField(value.ToString())
            ?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

        if (attribute == null)
            return value.ToString();

        var propValue = attribute.GetType().GetProperty(property.ToString())?.GetValue(attribute, null);
        return propValue?.ToString();
    }

    public static Dictionary<int, string> ToDictionary(this Enum value)
        => Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(Convert.ToInt32, q => q.ToDisplay());
}

public enum DisplayProperty
{
    Description,
    GroupName,
    Name,
    Prompt,
    ShortName,
    Order
}
