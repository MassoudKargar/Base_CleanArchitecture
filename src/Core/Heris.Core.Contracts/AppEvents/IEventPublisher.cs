namespace Heris.Core.Contracts.AppEvents;
/// <summary>
/// نمایانگر ناشر رویداد است.
/// </summary>
public partial interface IEventPublisher
{
    /// <summary>
    /// انتشار رویداد به مصرف‌کنندگان به صورت غیرهمزمان.
    /// </summary>
    /// <typeparam name="TEvent">نوع رویداد که از <see cref="BaseEvent{TEntity, TId}"/> ارث‌بری می‌کند.</typeparam>
    /// <typeparam name="TEntity">نوع موجودیتی که رویداد به آن مربوط می‌شود و از <see cref="BaseEntity{TId}"/> ارث‌بری می‌کند.</typeparam>
    /// <typeparam name="TId">نوع شناسه که باید نوع ساختار داده‌ای باشد.</typeparam>
    /// <param name="event">شیء رویداد که قرار است منتشر شود.</param>
    /// <returns>تسک غیرهمزمان برای انتشار رویداد.</returns>
    Task PublishAsync<TEvent, TEntity, TId>(TEvent @event)
        where TEvent : BaseEvent<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct;

    /// <summary>
    /// انتشار رویداد به مصرف‌کنندگان به صورت همزمان.
    /// </summary>
    /// <typeparam name="TEvent">نوع رویداد که از <see cref="BaseEvent{TEntity, TId}"/> ارث‌بری می‌کند.</typeparam>
    /// <typeparam name="TEntity">نوع موجودیتی که رویداد به آن مربوط می‌شود و از <see cref="BaseEntity{TId}"/> ارث‌بری می‌کند.</typeparam>
    /// <typeparam name="TId">نوع شناسه که باید نوع ساختار داده‌ای باشد.</typeparam>
    /// <param name="event">شیء رویداد که قرار است منتشر شود.</param>
    void Publish<TEvent, TEntity, TId>(TEvent @event)
        where TEvent : BaseEvent<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct;
}
