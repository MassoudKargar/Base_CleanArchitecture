namespace Heris.Utility.Exceptions;

/// <summary>
/// استثنایی برای نشان دادن درخواست نامعتبر (Bad Request) که از کلاس AppException ارث‌بری می‌کند.
/// </summary>
public class BadRequestException : AppException
{
    /// <summary>
    /// سازنده پیش‌فرض که کد وضعیت API و HTTP را به مقدار BadRequest تنظیم می‌کند.
    /// </summary>
    public BadRequestException()
        : base(ApiResultStatusCode.BadRequest, HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا را مشخص می‌کند و کد وضعیت API و HTTP را به مقدار BadRequest تنظیم می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    public BadRequestException(string message)
        : base(ApiResultStatusCode.BadRequest, message, HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    /// سازنده‌ای که داده‌های اضافی مرتبط با استثنا را مشخص می‌کند.
    /// </summary>
    /// <param name="additionalData">داده‌های اضافی</param>
    public BadRequestException(object additionalData)
        : base(ApiResultStatusCode.BadRequest, null, HttpStatusCode.BadRequest, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public BadRequestException(string message, object additionalData)
        : base(ApiResultStatusCode.BadRequest, message, HttpStatusCode.BadRequest, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و استثنای داخلی را مشخص می‌کند و کد وضعیت API و HTTP را به مقدار BadRequest تنظیم می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="exception">استثنای داخلی</param>
    public BadRequestException(string message, Exception exception)
        : base(ApiResultStatusCode.BadRequest, message, exception, HttpStatusCode.BadRequest)
    {
    }
}
