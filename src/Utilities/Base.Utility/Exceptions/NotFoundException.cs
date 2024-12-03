namespace Base.Utility.Exceptions;
/// <summary>
/// استثنایی برای نشان دادن خطای "یافت نشد" (Not Found) که از کلاس AppException ارث‌بری می‌کند.
/// </summary>
public class NotFoundException : AppException
{
    /// <summary>
    /// سازنده پیش‌فرض که کد وضعیت API و HTTP را به مقدار NotFound تنظیم می‌کند.
    /// </summary>
    public NotFoundException()
        : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا را مشخص می‌کند و کد وضعیت API و HTTP را به مقدار NotFound تنظیم می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    public NotFoundException(string message)
        : base(ApiResultStatusCode.NotFound, message, HttpStatusCode.NotFound)
    {
    }

    /// <summary>
    /// سازنده‌ای که داده‌های اضافی مرتبط با خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="additionalData">داده‌های اضافی</param>
    public NotFoundException(object additionalData)
        : base(ApiResultStatusCode.NotFound, null, HttpStatusCode.NotFound, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public NotFoundException(string message, object additionalData)
        : base(ApiResultStatusCode.NotFound, message, HttpStatusCode.NotFound, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و استثنای داخلی را مشخص می‌کند و کد وضعیت API و HTTP را به مقدار NotFound تنظیم می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="exception">استثنای داخلی</param>
    public NotFoundException(string message, Exception exception)
        : base(ApiResultStatusCode.NotFound, message, exception, HttpStatusCode.NotFound)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا، استثنای داخلی و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="exception">استثنای داخلی</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public NotFoundException(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.NotFound, message, HttpStatusCode.NotFound, exception, additionalData)
    {
    }
}
