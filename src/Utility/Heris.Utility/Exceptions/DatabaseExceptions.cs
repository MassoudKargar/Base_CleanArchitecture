namespace Heris.Utility.Exceptions;
/// <summary>
/// استثنایی که برای نشان دادن خطاهای مربوط به پایگاه داده استفاده می‌شود.
/// از کلاس AppException ارث‌بری می‌کند.
/// </summary>
public class DatabaseExceptions : AppException
{
    /// <summary>
    /// سازنده پیش‌فرض که کد وضعیت API را به مقدار ServiceUnavailable تنظیم می‌کند.
    /// </summary>
    public DatabaseExceptions()
        : base(ApiResultStatusCode.ServiceUnavailable)
    {
    }

    /// <summary>
    /// سازنده‌ای که کد وضعیت API را مشخص می‌کند.
    /// </summary>
    /// <param name="apiResultStatusCode">کد وضعیت API</param>
    public DatabaseExceptions(ApiResultStatusCode apiResultStatusCode)
        : base(apiResultStatusCode)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    public DatabaseExceptions(string message)
        : base(ApiResultStatusCode.ServiceUnavailable, message)
    {
    }

    /// <summary>
    /// سازنده‌ای که داده‌های اضافی مرتبط با خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="additionalData">داده‌های اضافی</param>
    public DatabaseExceptions(object additionalData)
        : base(ApiResultStatusCode.ServiceUnavailable, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public DatabaseExceptions(string message, object additionalData)
        : base(ApiResultStatusCode.ServiceUnavailable, message, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و استثنای داخلی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="exception">استثنای داخلی</param>
    public DatabaseExceptions(string message, Exception exception)
        : base(ApiResultStatusCode.ServiceUnavailable, message, exception)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا، استثنای داخلی و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="exception">استثنای داخلی</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public DatabaseExceptions(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.ServiceUnavailable, message, exception, additionalData)
    {
    }
}
