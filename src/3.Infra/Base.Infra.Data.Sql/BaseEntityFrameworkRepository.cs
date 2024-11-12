using Base.Extensions.DependencyInjection.Abstractions;

namespace Base.Infra.Data.Sql;

public class BaseEntityFrameworkRepository<TEntity, TDbContext, TId> : IGenericRepository<TEntity, TId>, IUnitOfWork, ITransientLifetime
    where TDbContext : BaseDbContext
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    public BaseEntityFrameworkRepository(TDbContext dbContext)
    {

        Context = dbContext;
        Entities = Context.Set<TEntity>();
    }

    private TDbContext Context { get; }
    private DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public void Delete(TId id)
    {
        var entity = Entities.Find(id);
        Entities.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public void DeleteGraph(TId id)
    {
        var entity = GetGraph(id);
        if (entity is not null && !entity.Id.Equals(default))
            Entities.Remove(entity);
    }

    #region insert

    public void Insert(TEntity entity)
    {
        Entities.Add(entity);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }
    #endregion

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await TableNoTracking.ToListAsync(cancellationToken);
    }
    #region Get Single Item
    public TEntity Get(TId id)
    {
        return Entities.Find(id);
    }

    public async Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    #endregion

    #region Get single item with graph
    public TEntity GetGraph(TId id)
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


    public async Task<TEntity> GetGraphAsync(TId id, CancellationToken cancellationToken)
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


    public async Task<bool> ExistAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }

    public async Task<bool> ExistAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }
    public async Task<bool> ExistAsync<TId>(TId id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }

    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return Entities.Any(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
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

    #region Update

    public void Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    #endregion

}