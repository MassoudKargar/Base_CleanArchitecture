namespace Base.Application.BaseMediatR;

/// <summary>
/// کلاسی برای ارسال درخواست‌های عمومی به سیستم که شامل گزینه‌های فیلتر، مرتب‌سازی، صفحه‌بندی و نوع عملیات می‌باشد.
/// این کلاس معمولاً برای درخواست‌های داده‌ای استفاده می‌شود که شامل فیلترها و دیگر تنظیمات مربوط به داده‌ها می‌باشد.
/// </summary>
/// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
/// <typeparam name="TQuery">نوع مدل داده‌ای برای فیلتر، مرتب‌سازی و صفحه‌بندی</typeparam>
/// <typeparam name="TResponse">نوع پاسخ عملیات</typeparam>
public class GenericQuery<TId, TQuery, TResponse> : IRequest<TResponse>
    where TId : struct
{
    /// <summary>
    /// شناسه موجودیت که برای انجام عملیات بر روی آن استفاده می‌شود
    /// </summary>
    public TId Id { get; }

    /// <summary>
    /// تنظیمات OData برای فیلتر، مرتب‌سازی و صفحه‌بندی داده‌ها
    /// </summary>
    public ODataQueryOptions<TQuery>? QueryOptions { get; }

    /// <summary>
    /// نوع عملیاتی که باید انجام شود (دریافت تمام داده‌ها، دریافت بر اساس شناسه و ...)
    /// </summary>
    public GenericAction GenericActionData { get; }

    /// <summary>
    /// سازنده کلاس GenericQuery
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="queryOptions">تنظیمات OData برای درخواست</param>
    /// <param name="genericAction">نوع عملیات (دریافت داده‌ها، فیلتر کردن و ...)</param>
    public GenericQuery(TId id, ODataQueryOptions<TQuery>? queryOptions, GenericAction genericAction)
    {
        Id = id;
        QueryOptions = queryOptions;
        GenericActionData = genericAction;
    }
}
