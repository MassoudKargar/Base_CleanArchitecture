namespace Base.Core.Contracts.AppEvents;

/// <summary>
/// Represents an event publisher
/// </summary>
public partial interface IEventPublisher
{
    /// <summary>
    /// Publish event to consumers
    /// </summary>
    /// <typeparam name="TEvent">Type of event</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <param name="event">Event object</param>
    Task PublishAsync<TEvent, TEntity, TId>(TEvent @event)
        where TEvent : BaseEvent<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct;

    /// <summary>
    /// Publish event to consumers
    /// </summary>
    /// <typeparam name="TEvent">Type of event</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <param name="event">Event object</param>
    void Publish<TEvent, TEntity, TId>(TEvent @event)
        where TEvent : BaseEvent<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct;
}