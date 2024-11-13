using Microsoft.EntityFrameworkCore;
namespace Base.Infrastructure;

public class GenericService<TEntity, TId>(IGenericRepository<TEntity, TId> repository)
    : IGenericService<TEntity, TId>, ITransientLifetime
    where TEntity : BaseEntity<TId>, new()
    where TId : struct
{
    public virtual IQueryable<TEntity> GetAll(bool addAsNoTracking, CancellationToken cancellationToken)
    {
        var baseResult = repository.GetAllAsync(addAsNoTracking, cancellationToken);
        return baseResult;
    }

    public virtual async Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken)
    {
        var baseResult = await repository.GetAsync(id, cancellationToken);
        return baseResult;
    }

    public virtual async Task InsertAsync(TEntity model, CancellationToken cancellationToken)
    {
        await repository.InsertAsync(model, cancellationToken: cancellationToken);
    }

    public virtual async Task UpdateAsync(TId id, TEntity model, CancellationToken cancellationToken)
    {
        repository.Update(model);
    }

    public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        var profile = await repository.GetAsync(id, cancellationToken);
        if (profile == null)
        {
            throw new NullReferenceException();
        }
        repository.Delete(profile);
    }
}