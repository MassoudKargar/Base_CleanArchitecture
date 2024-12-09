namespace Heris.Utility;

/// <summary>
/// کلاسی پایه برای سرویس‌ها که یک شیء از نوع <see cref="IJsonSerializer"/> را ذخیره می‌کند.
/// </summary>
public class BaseServices
{
    /// <summary>
    /// شیء <see cref="IJsonSerializer"/> که برای سریال‌سازی و دی‌سریال‌سازی داده‌ها استفاده می‌شود.
    /// </summary>
    public readonly IJsonSerializer Serializer;

    /// <summary>
    /// سازنده کلاس که یک شیء از نوع <see cref="IJsonSerializer"/> را به عنوان ورودی دریافت می‌کند.
    /// </summary>
    /// <param name="serializer">شیء <see cref="IJsonSerializer"/> که برای سریال‌سازی و دی‌سریال‌سازی داده‌ها استفاده می‌شود.</param>
    public BaseServices(IJsonSerializer serializer)
    {
        Serializer = serializer;
    }
}
