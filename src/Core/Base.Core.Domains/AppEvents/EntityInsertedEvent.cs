namespace Base.Core.Domains.AppEvents;

public class EntityInsertedEvent<TEntity, TId>(TEntity entity) : BaseEvent<TEntity, TId>(entity)
    where TEntity : BaseEntity<TId>
    where TId : struct;