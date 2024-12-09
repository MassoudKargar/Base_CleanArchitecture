namespace Heris.Utility.Exceptions;
/// <summary>
/// استثنایی برای نشان دادن خطای "عدم مجوز دسترسی" (Unauthorized) که از کلاس AppException ارث‌بری می‌کند.
/// </summary>
public class UnauthorizedException : AppException
{
    /// <summary>
    /// سازنده پیش‌فرض که کد وضعیت API را به مقدار Unauthorized تنظیم می‌کند.
    /// </summary>
    public UnauthorizedException()
        : base(ApiResultStatusCode.Unauthorized)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا.</param>
    public UnauthorizedException(string message)
        : base(ApiResultStatusCode.Unauthorized, message)
    {
    }

    /// <summary>
    /// سازنده‌ای که داده‌های اضافی مرتبط با خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="additionalData">داده‌های اضافی مرتبط با خطا.</param>
    public UnauthorizedException(object additionalData)
        : base(ApiResultStatusCode.Unauthorized, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا.</param>
    /// <param name="additionalData">داده‌های اضافی مرتبط با خطا.</param>
    public UnauthorizedException(string message, object additionalData)
        : base(ApiResultStatusCode.Unauthorized, message, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و استثنای داخلی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا.</param>
    /// <param name="exception">استثنای داخلی.</param>
    public UnauthorizedException(string message, Exception exception)
        : base(ApiResultStatusCode.Unauthorized, message, exception)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا، استثنای داخلی و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا.</param>
    /// <param name="exception">استثنای داخلی.</param>
    /// <param name="additionalData">داده‌های اضافی مرتبط با خطا.</param>
    public UnauthorizedException(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.Unauthorized, message, exception, additionalData)
    {
    }
}
