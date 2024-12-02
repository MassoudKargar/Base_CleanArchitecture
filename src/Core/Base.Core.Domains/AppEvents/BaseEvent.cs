namespace Base.Core.Domains.AppEvents;


/// <summary>
/// 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>
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
