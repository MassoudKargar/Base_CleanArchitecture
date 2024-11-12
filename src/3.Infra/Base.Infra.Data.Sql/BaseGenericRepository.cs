namespace Base.Infra.Data.Sql;

class BaseGenericRepository<TEntity, TId>(BaseDbContext dbContext)
    : IGenericRepository<TEntity, TId>, IUnitOfWork
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    protected readonly BaseDbContext _dbContext = dbContext;
    private readonly DbSet<TEntity> _entity = dbContext.Set<TEntity>();
    public void Delete(TId id)
    {
        var entity = _entity.Find(id);
        _entity.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _entity.Remove(entity);
    }

    public void DeleteGraph(TId id)
    {
        var entity = GetGraph(id);
        if (entity is not null && !entity.Id.Equals(default))
            _entity.Remove(entity);
    }

    #region insert

    public void Insert(TEntity entity)
    {
        _entity.Add(entity);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _entity.AddAsync(entity);
    }
    #endregion

    #region Get Single Item
    public TEntity Get(TId id)
    {
        return _entity.Find(id);
    }

    public async Task<TEntity> GetAsync(TId id)
    {
        return await _entity.FindAsync(id);
    }

    #endregion

    #region Get single item with graph
    public TEntity GetGraph(TId id)
    {
        var graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = _entity.AsQueryable();
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
        IQueryable<TEntity> query = _entity.AsQueryable();
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
        return _entity.Any(expression);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _entity.AnyAsync(expression);
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

    #region Update

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    #endregion

}