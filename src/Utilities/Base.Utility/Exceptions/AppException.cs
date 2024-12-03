namespace Base.Utility.Exceptions;
/// <summary>
/// استثنای سفارشی برنامه که شامل داده‌های اضافی، کد وضعیت HTTP و کد وضعیت API است.
/// </summary>
public class AppException : Exception
{
    /// <summary>
    /// کد وضعیت HTTP مرتبط با استثنا را دریافت یا تنظیم می‌کند.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }

    /// <summary>
    /// کد وضعیت نتیجه API مرتبط با استثنا را دریافت یا تنظیم می‌کند.
    /// </summary>
    public ApiResultStatusCode ApiStatusCode { get; set; }

    /// <summary>
    /// داده‌های اضافی مرتبط با استثنا را دریافت یا تنظیم می‌کند.
    /// </summary>
    public object AdditionalData { get; set; }

    /// <summary>
    /// سازنده پیش‌فرض که کد وضعیت API را به مقدار پیش‌فرض NotAcceptable تنظیم می‌کند.
    /// </summary>
    public AppException()
        : this(ApiResultStatusCode.NotAcceptable)
    {
    }

    /// <summary>
    /// سازنده‌ای که کد وضعیت API را مشخص می‌کند.
    /// </summary>
    /// <param name="statusCode">کد وضعیت API</param>
    public AppException(ApiResultStatusCode statusCode)
        : this(statusCode, string.Empty)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    public AppException(string message)
        : this(ApiResultStatusCode.NotAcceptable, message)
    {
    }

    /// <summary>
    /// سازنده‌ای که کد وضعیت API و پیام خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="statusCode">کد وضعیت API</param>
    /// <param name="message">پیام خطا</param>
    public AppException(ApiResultStatusCode statusCode, string message)
        : this(statusCode, message, HttpStatusCode.NotAcceptable)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public AppException(string message, object additionalData)
        : this(ApiResultStatusCode.NotAcceptable, message, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که کد وضعیت API و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="statusCode">کد وضعیت API</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public AppException(ApiResultStatusCode statusCode, object additionalData)
        : this(statusCode, string.Empty, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که کد وضعیت API، پیام خطا و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="statusCode">کد وضعیت API</param>
    /// <param name="message">پیام خطا</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public AppException(ApiResultStatusCode statusCode, string message, object additionalData)
        : this(statusCode, message, HttpStatusCode.NotAcceptable, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که کد وضعیت API، پیام خطا و کد وضعیت HTTP را مشخص می‌کند.
    /// </summary>
    /// <param name="statusCode">کد وضعیت API</param>
    /// <param name="message">پیام خطا</param>
    /// <param name="httpStatusCode">کد وضعیت HTTP</param>
    public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
        : this(statusCode, message, httpStatusCode, string.Empty)
    {
    }

    /// <summary>
    /// سازنده‌ای که تمامی اطلاعات از جمله کد وضعیت API، پیام خطا، کد وضعیت HTTP و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="statusCode">کد وضعیت API</param>
    /// <param name="message">پیام خطا</param>
    /// <param name="httpStatusCode">کد وضعیت HTTP</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
        : this(statusCode, message, httpStatusCode, null, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و استثنای داخلی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="exception">استثنای داخلی</param>
    public AppException(string message, Exception exception)
        : this(ApiResultStatusCode.NotAcceptable, message, exception)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا، استثنای داخلی و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا</param>
    /// <param name="exception">استثنای داخلی</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public AppException(string message, Exception exception, object additionalData)
        : this(ApiResultStatusCode.NotAcceptable, message, exception, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که کد وضعیت API، پیام خطا و استثنای داخلی را مشخص می‌کند.
    /// </summary>
    /// <param name="statusCode">کد وضعیت API</param>
    /// <param name="message">پیام خطا</param>
    /// <param name="exception">استثنای داخلی</param>
    public AppException(ApiResultStatusCode statusCode, string message, Exception exception)
        : this(statusCode, message, HttpStatusCode.NotAcceptable, exception)
    {
    }

    
    public AppException(ApiResultStatusCode statusCode, string message, Exception exception, object additionalData)
        : this(statusCode, message, HttpStatusCode.NotAcceptable, exception, additionalData)
    {
    }

    public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
        : this(statusCode, message, httpStatusCode, exception, null)
    {
    }
    /// <summary>
    /// سازنده‌ای که تمامی اطلاعات از جمله کد وضعیت API، پیام خطا، کد وضعیت HTTP، استثنای داخلی و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="statusCode">کد وضعیت API</param>
    /// <param name="message">پیام خطا</param>
    /// <param name="httpStatusCode">کد وضعیت HTTP</param>
    /// <param name="exception">استثنای داخلی</param>
    /// <param name="additionalData">داده‌های اضافی</param>
    public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object? additionalData)
        : base(message, exception)
    {
        ApiStatusCode = statusCode;
        HttpStatusCode = httpStatusCode;
        AdditionalData = additionalData;
    }
}

