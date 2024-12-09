namespace Heris.Infrastructure.SqlContext;
/// <summary>
/// کلاس کانتکست پایگاه داده پایه برای مدیریت ارتباط با پایگاه داده.
/// این کلاس از EF Core برای مدیریت تراکنش‌ها و عملیات‌های پایگاه داده استفاده می‌کند.
/// </summary>
public class BaseDbContext : DbContext
{
    /// <summary>
    /// تراکنش جاری پایگاه داده.
    /// </summary>
    protected IDbContextTransaction? Transaction;

    /// <summary>
    /// سازنده کلاس که تنظیمات DbContext را دریافت می‌کند.
    /// </summary>
    /// <param name="options">تنظیمات DbContext</param>
    public BaseDbContext(DbContextOptions options) : base(options)
    {
        Transaction = null;
    }

    /// <summary>
    /// شروع یک تراکنش جدید.
    /// </summary>
    public void BeginTransaction()
    {
        Transaction = Database.BeginTransaction();
    }

    /// <summary>
    /// بازگشت تراکنش جاری.
    /// </summary>
    /// <exception cref="NullReferenceException">اگر ابتدا متد `BeginTransaction()` فراخوانی نشده باشد.</exception>
    public void RollbackTransaction()
    {
        if (Transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        Transaction.Rollback();
    }

    /// <summary>
    /// تأیید تراکنش جاری.
    /// </summary>
    /// <exception cref="NullReferenceException">اگر ابتدا متد `BeginTransaction()` فراخوانی نشده باشد.</exception>
    public void CommitTransaction()
    {
        if (Transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        Transaction.Commit();
    }

    /// <summary>
    /// دریافت مسیرهای شامل‌شده (Include paths) برای یک نوع موجودیت خاص.
    /// </summary>
    /// <param name="clrEntityType">نوع موجودیت</param>
    /// <returns>یک مجموعه از مسیرهای شامل‌شده به صورت رشته</returns>
    public IEnumerable<string> GetIncludePaths(Type clrEntityType)
    {
        var entityType = Model.FindEntityType(clrEntityType);
        var includedNavigation = new HashSet<INavigation>();
        var stack = new Stack<IEnumerator<INavigation>>();

        while (true)
        {
            if (entityType == null)
            {
                break;
            }
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
