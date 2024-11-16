namespace Base.Core.Contracts.AppEvents;

/// <summary>
/// Consumer interface
/// </summary>
/// <typeparam name="TEvent"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>
public interface IConsumer<in TEvent,TEntity,TId>
    where TEvent : BaseEvent<TEntity,TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    /// <summary>
    /// Handle event
    /// </summary>
    /// <param name="eventMessage">Event</param>
    Task HandleEventAsync(TEvent eventMessage);
    void HandleEvent(TEvent eventMessage);
}
