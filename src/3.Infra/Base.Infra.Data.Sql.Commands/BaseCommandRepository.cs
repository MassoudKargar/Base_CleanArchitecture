﻿namespace Base.Infra.Data.Sql.Commands;
public class BaseCommandRepository<TEntity, TDbContext, TId>(TDbContext dbContext)
    : ICommandRepository<TEntity, TId>, IUnitOfWork
    where TEntity : Entity<TId>
    where TDbContext : BaseCommandDbContext
    where TId : struct,
    IComparable,
    IComparable<TId>,
    IConvertible,
    IEquatable<TId>,
    IFormattable
{

    protected readonly TDbContext _dbContext = dbContext;


    public void Delete(TId id)
    {
        var entity = _dbContext.Set<TEntity>().Find(id);
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void DeleteGraph(TId id)
    {
        var entity = GetGraph(id);
        if (entity is not null && !entity.Id.Equals(default))
            _dbContext.Set<TEntity>().Remove(entity);
    }

    #region insert

    public void Insert(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }
    #endregion

    #region Get Single Item
    public TEntity Get(TId id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }

    public async Task<TEntity> GetAsync(TId id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    #endregion

    #region Get single item with graph
    public TEntity GetGraph(TId id)
    {
        var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
        var temp = graphPath.ToList();
        foreach (var item in graphPath)
        {
            query = query.Include(item);
        }
        return query.FirstOrDefault(c => c.Id.Equals(id));
    }


    public async Task<TEntity> GetGraphAsync(TId id)
    {
        var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
        foreach (var item in graphPath)
        {
            query = query.Include(item);
        }
        return await query.FirstOrDefaultAsync(c => c.Id.Equals(id));
    }

    #endregion

    #region Exists
    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return _dbContext.Set<TEntity>().Any(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(expression);
    }
    #endregion

    #region Transaction management
    public int Commit()
    {
        return _dbContext.SaveChanges();
    }

    public Task<int> CommitAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
    public void BeginTransaction()
    {
        _dbContext.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _dbContext.CommitTransaction();
    }
    public void RollbackTransaction()
    {
        _dbContext.RollbackTransaction();
    }
    #endregion
}


public class BaseCommandRepository<TEntity, TDbContext>(TDbContext dbContext)
    : BaseCommandRepository<TEntity, TDbContext, long>(dbContext)
    where TEntity : Entity
    where TDbContext : BaseCommandDbContext;