using Base.Infrastructure.Ef.Context;
using Base.Utility.Exceptions;

namespace Base.Infrastructure.Ef.Repositories;
/// <summary>
/// پیاده‌سازی یک مخزن عمومی برای انجام عملیات‌های CRUD بر روی موجودیت‌ها.
/// این کلاس از Entity Framework برای تعامل با پایگاه داده استفاده می‌کند.
/// </summary>
/// <typeparam name="TEntity">نوع موجودیت که در پایگاه داده ذخیره می‌شود.</typeparam>
/// <typeparam name="TId">نوع شناسه موجودیت.</typeparam>
public class GenericRepository<TEntity, TId>
    : IGenericRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    /// <summary>
    /// سازنده کلاس که با پایگاه داده مرتبط است.
    /// </summary>
    /// <param name="dbContext">کانتکست پایگاه داده</param>
    public GenericRepository(BaseDbContext dbContext)
    {
        Context = dbContext;
        Entities = Context.Set<TEntity>();
    }

    private BaseDbContext Context { get; }
    private DbSet<TEntity> Entities { get; }

    /// <summary>
    /// درج موجودیت در پایگاه داده.
    /// </summary>
    /// <param name="entity">موجودیت برای درج</param>
    /// <param name="isCommit">آیا تغییرات باید در پایگاه داده ذخیره شوند؟</param>
    public virtual void Insert(TEntity entity, bool isCommit)
    {
        Entities.Add(entity);
        if (isCommit)
        {
            Commit();
        }
    }

    /// <summary>
    /// درج موجودیت به صورت ناهمزمان در پایگاه داده.
    /// </summary>
    /// <param name="entity">موجودیت برای درج</param>
    /// <param name="isCommit">آیا تغییرات باید در پایگاه داده ذخیره شوند؟</param>
    /// <param name="cancellationToken">توکن لغو عملیات</param>
    public virtual async Task InsertAsync(TEntity entity, bool isCommit = true, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
        entity.CreationDate = DateTime.Now;
        entity.IsDeleted = false;
        if (isCommit)
        {
            await CommitAsync();
        }
    }

    /// <summary>
    /// بروزرسانی موجودیت در پایگاه داده.
    /// </summary>
    /// <param name="entity">موجودیت برای بروزرسانی</param>
    /// <param name="isCommit">آیا تغییرات باید در پایگاه داده ذخیره شوند؟</param>
    public virtual void Update(TEntity entity, bool isCommit = true)
    {
        Context.Entry(entity).State = EntityState.Modified;
        entity.ModificationDate = DateTime.Now;
        if (isCommit)
        {
            Commit();
        }
    }

    /// <summary>
    /// بروزرسانی وضعیت حذف شده (IsDeleted) برای موجودیت.
    /// </summary>
    /// <param name="entity">موجودیت برای بروزرسانی</param>
    /// <param name="isCommit">آیا تغییرات باید در پایگاه داده ذخیره شوند؟</param>
    public virtual void UpdateToDeleted(TEntity entity, bool isCommit = true)
    {
        Context.Entry(entity).State = EntityState.Modified;
        entity.ModificationDate = DateTime.Now;
        entity.IsDeleted = true;
        if (isCommit)
        {
            Commit();
        }
    }

    /// <summary>
    /// حذف موجودیت بر اساس شناسه.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="isCommit">آیا تغییرات باید در پایگاه داده ذخیره شوند؟</param>
    public virtual void Delete(TId id, bool isCommit = true)
    {
        Entities.Remove(Get(id));
        if (isCommit)
        {
            Commit();
        }
    }

    /// <summary>
    /// حذف موجودیت از پایگاه داده.
    /// </summary>
    /// <param name="entity">موجودیت برای حذف</param>
    /// <param name="isCommit">آیا تغییرات باید در پایگاه داده ذخیره شوند؟</param>
    public virtual void Delete(TEntity entity, bool isCommit = true)
    {
        Entities.Remove(entity);
        if (isCommit)
        {
            Commit();
        }
    }

    /// <summary>
    /// دریافت تمام موجودیت‌ها به همراه ویژگی‌های اضافی (شامل).
    /// </summary>
    /// <param name="includeProperties">ویژگی‌هایی که باید شامل شوند</param>
    /// <returns>یک کوئری که تمام موجودیت‌ها را به همراه ویژگی‌های اضافی باز می‌گرداند</returns>
    public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties) =>
        includeProperties.Aggregate(GetAllAsync(), (current, includeProperty) =>
            current.Include(includeProperty));

    /// <summary>
    /// دریافت تمام موجودیت‌ها از پایگاه داده به صورت ناهمزمان.
    /// </summary>
    /// <param name="addAsNoTracking">آیا باید از قابلیت NoTracking استفاده شود؟</param>
    /// <param name="isDeleted">آیا فقط موجودیت‌های غیرحذف شده باید بازگردانده شوند؟</param>
    /// <param name="cancellationToken">توکن لغو عملیات</param>
    /// <returns>یک کوئری که تمام موجودیت‌ها را باز می‌گرداند</returns>
    public virtual IQueryable<TEntity> GetAllAsync(bool addAsNoTracking = true, bool isDeleted = true, CancellationToken cancellationToken = default)
    {
        if (addAsNoTracking)
        {
            return isDeleted ? Entities.AsNoTracking().Where(e => e.IsDeleted == false).AsQueryable() : Entities.AsNoTracking().AsQueryable();
        }
        else
        {
            return isDeleted ? Entities.Where(e => e.IsDeleted == false).AsQueryable() : Entities.AsQueryable();
        }
    }

    /// <summary>
    /// دریافت موجودیتی با شناسه خاص.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <returns>موجودیتی با شناسه خاص</returns>
    /// <exception cref="NotFoundException">اگر موجودیت پیدا نشود</exception>
    public virtual TEntity Get(TId id)
    {
        var result = Entities.Find(id);
        if (result is null)
        {
            throw new NotFoundException();
        }
        return result;
    }

    /// <summary>
    /// دریافت موجودیتی با شناسه خاص به صورت ناهمزمان.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="cancellationToken">توکن لغو عملیات</param>
    /// <returns>موجودیتی با شناسه خاص</returns>
    /// <exception cref="NotFoundException">اگر موجودیت پیدا نشود</exception>
    public virtual async Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken)
    {
        var result = await Entities.FindAsync([id], cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException();
        }
        return result;
    }

    /// <summary>
    /// بررسی وجود موجودیت بر اساس شناسه.
    /// </summary>
    /// <param name="id">شناسه موجودیت</param>
    /// <param name="cancellationToken">توکن لغو عملیات</param>
    /// <returns>آیا موجودیت با شناسه خاص وجود دارد؟</returns>
    public virtual async Task<bool> ExistAsync(TId id, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(id, cancellationToken);
        Entities.Entry(entity).State = EntityState.Detached;
        return entity != null;
    }

    /// <summary>
    /// بررسی وجود موجودیت بر اساس یک شرط خاص.
    /// </summary>
    /// <param name="expression">شرط برای جستجو</param>
    /// <returns>آیا موجودیت‌هایی که شرط را برآورده می‌کنند وجود دارند؟</returns>
    public virtual bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        return Entities.Any(expression);
    }

    /// <summary>
    /// بررسی وجود موجودیت بر اساس یک شرط خاص به صورت ناهمزمان.
    /// </summary>
    /// <param name="expression">شرط برای جستجو</param>
    /// <param name="cancellationToken">توکن لغو عملیات</param>
    /// <returns>آیا موجودیت‌هایی که شرط را برآورده می‌کنند وجود دارند؟</returns>
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return await Entities.AnyAsync(expression, cancellationToken);
    }

    /// <summary>
    /// ذخیره تغییرات در پایگاه داده.
    /// </summary>
    /// <returns>تعداد تغییرات ذخیره‌شده</returns>
    public int Commit()
    {
        return Context.SaveChanges();
    }

    /// <summary>
    /// ذخیره تغییرات در پایگاه داده به صورت ناهمزمان.
    /// </summary>
    /// <returns>تعداد تغییرات ذخیره‌شده</returns>
    public Task<int> CommitAsync()
    {
        return Context.SaveChangesAsync();
    }
}
