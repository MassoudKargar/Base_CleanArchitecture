namespace Base.Infrastructure.SqlContext;
public abstract class UnitOfWork<TDbContext>(TDbContext dbContext) : IUnitOfWork, ITransientLifetime
    where TDbContext : BaseDbContext
{
    public void BeginTransaction()
    {
        dbContext.BeginTransaction();
    }

    public int Commit()
    {
        var result = dbContext.SaveChanges();
        return result;
    }

    public async Task<int> CommitAsync()
    {
        var result = await dbContext.SaveChangesAsync();
        return result;
    }

    public void CommitTransaction()
    {
        dbContext.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        dbContext.RollbackTransaction();
    }
}

