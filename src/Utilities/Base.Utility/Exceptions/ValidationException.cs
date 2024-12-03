namespace Base.Utility.Exceptions;
/// <summary>
/// استثنایی که برای نشان دادن خطاهای اعتبارسنجی (Validation) استفاده می‌شود.
/// </summary>
public class SampleValidationException : AppException
{
    /// <summary>
    /// لیستی از پیام‌های خطا که در هنگام اعتبارسنجی ایجاد شده‌اند.
    /// </summary>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// سازنده‌ای که یک نتیجه اعتبارسنجی FluentValidation را دریافت کرده و پیام‌های خطا را تنظیم می‌کند.
    /// </summary>
    /// <param name="validationResult">نتیجه اعتبارسنجی شامل خطاها.</param>
    public SampleValidationException(FluentValidation.Results.ValidationResult validationResult)
        : base(ApiResultStatusCode.BadRequest, HttpStatusCode.BadRequest)
    {
        Errors.Capacity = validationResult.Errors.Select(s => s.ErrorMessage).Count();
        Errors.AddRange(validationResult.Errors.Select(s => s.ErrorMessage));
    }

    /// <summary>
    /// سازنده پیش‌فرض که کد وضعیت API و وضعیت HTTP را به مقدار BadRequest تنظیم می‌کند.
    /// </summary>
    public SampleValidationException()
        : base(ApiResultStatusCode.BadRequest, HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا.</param>
    public SampleValidationException(string message)
        : base(ApiResultStatusCode.BadRequest, message, HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    /// سازنده‌ای که داده‌های اضافی مرتبط با خطا را مشخص می‌کند.
    /// </summary>
    /// <param name="additionalData">داده‌های اضافی مرتبط با خطا.</param>
    public SampleValidationException(object additionalData)
        : base(ApiResultStatusCode.BadRequest, string.Empty, HttpStatusCode.BadRequest, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا.</param>
    /// <param name="additionalData">داده‌های اضافی مرتبط با خطا.</param>
    public SampleValidationException(string message, object additionalData)
        : base(ApiResultStatusCode.BadRequest, message, HttpStatusCode.BadRequest, additionalData)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا و استثنای داخلی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا.</param>
    /// <param name="exception">استثنای داخلی.</param>
    public SampleValidationException(string message, Exception exception)
        : base(ApiResultStatusCode.BadRequest, message, exception, HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    /// سازنده‌ای که پیام خطا، استثنای داخلی و داده‌های اضافی را مشخص می‌کند.
    /// </summary>
    /// <param name="message">پیام خطا.</param>
    /// <param name="exception">استثنای داخلی.</param>
    /// <param name="additionalData">داده‌های اضافی مرتبط با خطا.</param>
    public SampleValidationException(string message, Exception exception, object additionalData)
        : base(ApiResultStatusCode.BadRequest, message, HttpStatusCode.BadRequest, exception, additionalData)
    {
    }
}
