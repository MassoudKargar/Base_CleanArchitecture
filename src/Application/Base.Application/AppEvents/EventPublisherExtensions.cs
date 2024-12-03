namespace Base.Application.AppEvents;
/// <summary>
/// اکستنشن‌های ناشر رویداد
/// </summary>
public static class EventPublisherExtensions
{
    /// <summary>
    /// زمانی که موجودیت وارد می‌شود
    /// </summary>
    /// <typeparam name="TEntity">نوع موجودیت</typeparam>
    /// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
    /// <param name="eventPublisher">ناشر رویداد</param>
    /// <param name="entity">موجودیت</param>
    public static async Task EntityInsertedAsync<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        await eventPublisher.PublishAsync<EntityInsertedEvent<TEntity, TId>, TEntity, TId>(new EntityInsertedEvent<TEntity, TId>(entity));
    }

    /// <summary>
    /// زمانی که موجودیت وارد می‌شود (نسخه همزمان)
    /// </summary>
    /// <typeparam name="TEntity">نوع موجودیت</typeparam>
    /// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
    /// <param name="eventPublisher">ناشر رویداد</param>
    /// <param name="entity">موجودیت</param>
    public static void EntityInserted<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        eventPublisher.Publish<EntityInsertedEvent<TEntity, TId>, TEntity, TId>(new EntityInsertedEvent<TEntity, TId>(entity));
    }

    /// <summary>
    /// زمانی که موجودیت به‌روزرسانی می‌شود
    /// </summary>
    /// <typeparam name="TEntity">نوع موجودیت</typeparam>
    /// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
    /// <param name="eventPublisher">ناشر رویداد</param>
    /// <param name="entity">موجودیت</param>
    public static async Task EntityUpdatedAsync<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        await eventPublisher.PublishAsync<EntityUpdatedEvent<TEntity, TId>, TEntity, TId>(new EntityUpdatedEvent<TEntity, TId>(entity));
    }

    /// <summary>
    /// زمانی که موجودیت به‌روزرسانی می‌شود (نسخه همزمان)
    /// </summary>
    /// <typeparam name="TEntity">نوع موجودیت</typeparam>
    /// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
    /// <param name="eventPublisher">ناشر رویداد</param>
    /// <param name="entity">موجودیت</param>
    public static void EntityUpdated<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        eventPublisher.Publish<EntityUpdatedEvent<TEntity, TId>, TEntity, TId>(new EntityUpdatedEvent<TEntity, TId>(entity));
    }

    /// <summary>
    /// زمانی که موجودیت حذف می‌شود
    /// </summary>
    /// <typeparam name="TEntity">نوع موجودیت</typeparam>
    /// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
    /// <param name="eventPublisher">ناشر رویداد</param>
    /// <param name="entity">موجودیت</param>
    public static void EntityDeleted<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        eventPublisher.Publish<EntityDeletedEvent<TEntity, TId>, TEntity, TId>(new EntityDeletedEvent<TEntity, TId>(entity));
    }

    /// <summary>
    /// زمانی که موجودیت حذف می‌شود (نسخه همزمان)
    /// </summary>
    /// <typeparam name="TEntity">نوع موجودیت</typeparam>
    /// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
    /// <param name="eventPublisher">ناشر رویداد</param>
    /// <param name="entity">موجودیت</param>
    public static async Task EntityDeletedAsync<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        await eventPublisher.PublishAsync<EntityDeletedEvent<TEntity, TId>, TEntity, TId>(new EntityDeletedEvent<TEntity, TId>(entity));
    }
}
