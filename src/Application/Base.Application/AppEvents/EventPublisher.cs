using Base.Core.Contracts.AppEvents;
using Base.Core.Domains.AppEvents;
using Base.Core.Domains.Entities;
using Base.Extensions.DependencyInjection.Abstractions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Base.Application.AppEvents;

/// <summary>
/// Represents the event publisher implementation
/// </summary>
public partial class EventPublisher(ILogger<EventPublisher> logger, IServiceProvider serviceProvider) : IEventPublisher, ITransientLifetime
{
    private ILogger<EventPublisher> Logger { get; } = logger;
    private IServiceProvider ServiceProvider { get; } = serviceProvider;
    #region Methods

    /// <summary>
    /// Publish event to consumers
    /// </summary>
    /// <typeparam name="TEvent">Type of event</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <param name="event">Event object</param>
    public virtual async Task PublishAsync<TEvent, TEntity, TId>(TEvent @event)
        where TEvent : BaseEvent<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        //get all event consumers
        var consumers = ResolveAll<IConsumer<TEvent, TEntity, TId>>().ToList();
        foreach (var consumer in consumers)
        {
            //try to handle published event
            await consumer.HandleEventAsync(@event);
        }

    }

    public virtual void Publish<TEvent, TEntity, TId>(TEvent @event)
        where TEvent : BaseEvent<TEntity, TId>
        where TEntity : BaseEntity<TId>
        where TId : struct
    {
        //get all event consumers
        var consumers = ResolveAll<IConsumer<TEvent, TEntity, TId>>().ToList();
        foreach (var consumer in consumers)
        {
            consumer.HandleEventAsync(@event);
        }
    }

    #endregion

    protected IServiceProvider GetServiceProvider(IServiceScope scope = null)
    {
        if (scope == null)
        {
            var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }
        return scope.ServiceProvider;
    }

    public virtual IEnumerable<T> ResolveAll<T>()
    {
        return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
    }


}