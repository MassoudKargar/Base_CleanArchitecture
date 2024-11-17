namespace Base.Infrastructure.SqlContext;

public class BaseDbContext : DbContext
{
    protected IDbContextTransaction _transaction;
    public BaseDbContext(DbContextOptions options) : base(options)
    {

    }

    public void BeginTransaction()
    {
        _transaction = Database.BeginTransaction();
    }

    public void RollbackTransaction()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        _transaction.Rollback();
    }

    public void CommitTransaction()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        _transaction.Commit();
    }
    public IEnumerable<string> GetIncludePaths(Type clrEntityType)
    {
        var entityType = Model.FindEntityType(clrEntityType);
        var includedNavigation = new HashSet<INavigation>();
        var stack = new Stack<IEnumerator<INavigation>>();
        while (true)
        {
            var entityNavigation = entityType.GetNavigations().Where(navigation => includedNavigation.Add(navigation)).ToList();
            if (entityNavigation.Count == 0)
            {
                if (stack.Count > 0)
                    yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
            }
            else
            {
                foreach (var inverseNavigation in entityNavigation.Select(navigation => navigation.Inverse).OfType<INavigation>())
                {
                    includedNavigation.Add(inverseNavigation);
                }

                stack.Push(entityNavigation.GetEnumerator());
            }
            while (stack.Count > 0 && !stack.Peek().MoveNext())
                stack.Pop();
            if (stack.Count == 0) break;
            entityType = stack.Peek().Current.TargetEntityType;
        }
    }
}
