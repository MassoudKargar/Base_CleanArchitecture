using Base.Infrastructure.SqlContext;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Base.Infrastructure.EfRepositories;

public class GenericRepository<TEntity, TId>
    : IGenericRepository<TEntity, TId>, IUnitOfWork, ITransientLifetime
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    public GenericRepository(BaseDbContext dbContext)
    {
        Context = dbContext;
        Entities = Context.Set<TEntity>();

    }

    private BaseDbContext Context { get; }
    private DbSet<TEntity> Entities { get; }



    #region insert

    public virtual void Insert(TEntity entity, bool isCommit)
    {
        Entities.Add(entity);
        if (isCommit)
        {
            Commit();
        }
    }

    public virtual async Task InsertAsync(TEntity entity, bool isCommit = true, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
        if (isCommit)
        {
            await CommitAsync();
        }
    }
    #endregion

    #region Update

    public virtual void Update(TEntity entity, bool isCommit = true)
    {
        Context.Entry(entity).State = EntityState.Modified;
        if (isCommit)
        {
            Commit();
        }
    }

    #endregion

    #region Delete

    public virtual void Delete(TId id, bool isCommit = true)
    {
        var entity = Entities.Find(id);
        Entities.Remove(entity);
        if (isCommit)
        {
            Commit();
        }
    }

    public virtual void Delete(TEntity entity, bool isCommit = true)
    {
        Entities.Remove(entity);
        if (isCommit)
        {
            Commit();
        }
    }

    public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties) => 
        includeProperties.Aggregate(GetAllAsync(), (current, includeProperty) =>
            current.Include<TEntity, object>(includeProperty));

    public virtual void DeleteGraph(TId id, bool isCommit = true)
    {
        var entity = GetGraph(id);
        if (entity is not null && !entity.Id.Equals(default))
            Entities.Remove(entity);
        if (isCommit)
        {
            Commit();
        }
    }


    #endregion

    #region Get Item

    public virtual IQueryable<TEntity> GetAllAsync(bool addAsNoTracking = true, CancellationToken cancellationToken = default)
    {
        if (addAsNoTracking)
        {
            return Entities.AsNoTracking().AsQueryable();
        }
        else
        {
            return Entities.AsQueryable();
        }
    }
    public virtual TEntity Get(TId id)
    {
        return Entities.Find(id);
    }

    public virtual async Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    #endregion

    #region Get single item with graph
    public virtual TEntity GetGraph(TId id)
    {
        var graphPath = Context.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = Entities.AsQueryable();
        var temp = graphPath.ToList();
        foreach (var item in graphPath)
        {
            query = query.Include(item);
        }
        return query.FirstOrDefault(c => c.Id.Equals(id));
    }


    public virtual async Task<TEntity> GetGraphAsync(TId id, CancellationToken cancellationToken)
    {
        var graphPath = Context.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = Entities.AsQueryable();
        foreach (var item in graphPath)
        {
            query = query.Include(item);
        }
        return await query.FirstOrDefaultAsync(c => c.Id.Equals(id), cancellationToken);
    }

    #endregion

    #region Exists


    public virtual async Task<bool> ExistAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }

    public virtual async Task<bool> ExistAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }

    public virtual async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }
    public virtual async Task<bool> ExistAsync<TId>(TId id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }

    public virtual bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return Entities.Any(expression);
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return await Entities.AnyAsync(expression, cancellationToken);
    }
    #endregion

    #region Transaction management
    public int Commit()
    {
        return Context.SaveChanges();
    }

    public Task<int> CommitAsync()
    {
        return Context.SaveChangesAsync();
    }
    public void BeginTransaction()
    {
        Context.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Context.CommitTransaction();
    }
    public void RollbackTransaction()
    {
        Context.RollbackTransaction();
    }

    #endregion


}