namespace Heris.Core.Contracts.AppEvents;

/// <summary>
/// رابط مصرف‌کننده برای پردازش رویدادها.
/// </summary>
/// <typeparam name="TEvent">نوع رویداد که از کلاس پایه <see cref="BaseEvent{TEntity, TId}"/> ارث‌بری می‌کند.</typeparam>
/// <typeparam name="TEntity">نوع موجودیتی که رویداد به آن مربوط می‌شود و از <see cref="BaseEntity{TId}"/> ارث‌بری می‌کند.</typeparam>
/// <typeparam name="TId">نوع شناسه که باید نوع ساختار داده‌ای باشد.</typeparam>
public interface IConsumer<in TEvent, TEntity, TId>
    where TEvent : BaseEvent<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    /// <summary>
    /// پردازش رویداد به صورت غیرهمزمان.
    /// </summary>
    /// <param name="eventMessage">پیام رویداد که باید پردازش شود.</param>
    /// <returns>تسک غیرهمزمان برای پردازش رویداد.</returns>
    Task HandleEventAsync(TEvent eventMessage);

    /// <summary>
    /// پردازش رویداد به صورت همزمان.
    /// </summary>
    /// <param name="eventMessage">پیام رویداد که باید پردازش شود.</param>
    void HandleEvent(TEvent eventMessage);
}
