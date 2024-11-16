namespace Base.Core.ApplicationServices.AppEvents;

/// <summary>
/// Event publisher extensions
/// </summary>
public static class EventPublisherExtensions
{
    /// <summary>
    /// Entity inserted
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <param name="eventPublisher">Event publisher</param>
    /// <param name="entity">Entity</param>
    public static async Task EntityInsertedAsync<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        await eventPublisher.PublishAsync<EntityInsertedEvent<TEntity, TId>,TEntity,TId>(new EntityInsertedEvent<TEntity, TId>(entity));
    }

    public static void EntityInserted<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        eventPublisher.Publish<EntityInsertedEvent<TEntity, TId>, TEntity, TId>(new EntityInsertedEvent<TEntity, TId>(entity));
    }


    /// <summary>
    /// Entity updated
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <param name="eventPublisher">Event publisher</param>
    /// <param name="entity">Entity</param>
    public static async Task EntityUpdatedAsync<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        await eventPublisher.PublishAsync<EntityUpdatedEvent<TEntity, TId>, TEntity, TId>(new EntityUpdatedEvent<TEntity, TId>(entity));
    }
    public static void EntityUpdated<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        eventPublisher.Publish<EntityUpdatedEvent<TEntity, TId>, TEntity, TId>(new EntityUpdatedEvent<TEntity, TId>(entity));
    }

    /// <summary>
    /// Entity deleted
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <param name="eventPublisher">Event publisher</param>
    /// <param name="entity">Entity</param>
    public static void EntityDeleted<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        eventPublisher.Publish<EntityDeletedEvent<TEntity, TId>, TEntity, TId>(new EntityDeletedEvent<TEntity, TId>(entity));
    }

    /// <summary>
    /// Entity deleted
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <param name="eventPublisher">Event publisher</param>
    /// <param name="entity">Entity</param>
    public static async Task EntityDeletedAsync<TEntity, TId>(this IEventPublisher eventPublisher, TEntity entity)
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        await eventPublisher.PublishAsync<EntityDeletedEvent<TEntity, TId>, TEntity, TId>(new EntityDeletedEvent<TEntity, TId>(entity));
    }
}