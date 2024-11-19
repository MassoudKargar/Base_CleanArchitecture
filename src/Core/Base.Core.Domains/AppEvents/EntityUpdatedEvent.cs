namespace Base.Core.Domains.AppEvents;

public class EntityUpdatedEvent<TEntity, TId>(TEntity entity) : BaseEvent<TEntity, TId>(entity)
    where TEntity : BaseEntity<TId>
    where TId : struct;