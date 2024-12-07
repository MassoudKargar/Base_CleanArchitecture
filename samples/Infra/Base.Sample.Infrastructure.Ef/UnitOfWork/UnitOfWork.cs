using Base.Core.Contracts.Data;
using Base.Sample.Infrastructure.Ef.Context;
using System.Security.Cryptography;

namespace Base.Sample.Infrastructure.Ef.UnitOfWork;

/// <summary>
/// کلاس واحد کار (Unit of Work) برای مدیریت تراکنش‌ها و عملیات‌های پایگاه داده.
/// این کلاس از متدهای موجود در BaseDbContext برای آغاز، تأیید و برگشت تراکنش‌ها استفاده می‌کند.
/// </summary>
public class UnitOfWork(BaseDbContext dbContext) : IUnitOfWork
{
    /// <summary>
    /// شروع یک تراکنش جدید.
    /// </summary>
    public void BeginTransaction()
    {
        dbContext.BeginTransaction();
    }

    /// <summary>
    /// تأیید تغییرات انجام‌شده در پایگاه داده.
    /// </summary>
    /// <returns>تعداد تغییرات اعمال‌شده در پایگاه داده</returns>
    public int Commit()
    {
        var result = dbContext.SaveChanges();
        return result;
    }

    /// <summary>
    /// تأیید تغییرات انجام‌شده در پایگاه داده به صورت غیرهمزمان.
    /// </summary>
    /// <returns>تعداد تغییرات اعمال‌شده در پایگاه داده</returns>
    public async Task<int> CommitAsync()
    {
        var result = await dbContext.SaveChangesAsync();
        return result;
    }

    /// <summary>
    /// تأیید تراکنش جاری.
    /// </summary>
    public void CommitTransaction()
    {
        dbContext.SaveChanges();
        dbContext.CommitTransaction();
    }

    /// <summary>
    /// بازگشت تراکنش جاری.
    /// </summary>
    public void RollbackTransaction()
    {
        dbContext.RollbackTransaction();
    }
}
