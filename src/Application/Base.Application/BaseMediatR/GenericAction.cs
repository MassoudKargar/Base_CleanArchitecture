namespace Base.Application.BaseMediatR;
/// <summary>
/// این enum نمایانگر انواع عملیات‌های عمومی است که می‌توانند بر روی موجودیت‌ها انجام شوند.
/// </summary>
public enum GenericAction : byte
{
    /// <summary>
    /// عملیات دریافت تمام داده‌ها
    /// </summary>
    GetAll = 0,

    /// <summary>
    /// عملیات دریافت موجودیت بر اساس شناسه
    /// </summary>
    GetById = 1,

    /// <summary>
    /// عملیات درج یک موجودیت جدید
    /// </summary>
    Insert = 2,

    /// <summary>
    /// عملیات به‌روزرسانی یک موجودیت موجود
    /// </summary>
    Update = 3,

    /// <summary>
    /// عملیات حذف یک موجودیت
    /// </summary>
    Delete = 4,
}
