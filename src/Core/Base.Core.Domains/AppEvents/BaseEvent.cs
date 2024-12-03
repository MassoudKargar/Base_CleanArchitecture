namespace Base.Core.Domains.AppEvents;
/// <summary>
/// کلاس پایه‌ای برای رویدادها که شامل یک موجود و یک شناسه است.
/// </summary>
/// <typeparam name="TEntity">نوع موجود</typeparam>
/// <typeparam name="TId">نوع شناسه</typeparam>
public class BaseEvent<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    /// <summary>
    /// می‌کنструктор کلاس را ایجاد می‌کند و موجود را تنظیم می‌کند.
    /// </summary>
    /// <param name="entity">وجود مرتبط با رویداد</param>
    public BaseEvent(TEntity entity) => Entity = entity;

    /// <summary>
    /// موجود که مرتبط با این رویداد است.
    /// </summary>
    public TEntity Entity { get; }
}
