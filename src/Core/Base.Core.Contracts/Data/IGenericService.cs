namespace Base.Core.Contracts.Data;

public interface IGenericService<TEntity, in TId>
    where TEntity : BaseEntity<TId>, new()
    where TId : struct
{
    IQueryable<TEntity> GetAll(bool addAsNoTracking, CancellationToken cancellationToken);
    Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken);
    Task InsertAsync(TEntity model, CancellationToken cancellationToken);
    Task UpdateAsync(TId id, TEntity model, CancellationToken cancellationToken);
    Task DeleteAsync(TId id, CancellationToken cancellationToken);
}