namespace Base.Core.Domains.AppEvents;

public class BaseEvent<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="entity">Entity</param>
    public BaseEvent(TEntity entity) => Entity = entity;

    /// <summary>
    /// Entity
    /// </summary>
    public TEntity Entity { get; }
}
