namespace Heris.Core.Contracts.Data;

/// <summary>
/// رابط عمومی برای مدیریت عملیات پایه‌ای دیتابیس.
/// </summary>
/// <typeparam name="TEntity">نوع موجودیت که باید با دیتابیس کار کند.</typeparam>
/// <typeparam name="TId">نوع شناسه موجودیت.</typeparam>
public interface IGenericRepository<TEntity, in TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    /// <summary>
    /// یک موجودیت را بر اساس شناسه از دیتابیس بازیابی می‌کند.
    /// </summary>
    /// <param name="id">شناسه موجودیت مورد نظر.</param>
    /// <returns>موجودیت بازیابی‌شده.</returns>
    TEntity Get(TId id);

    /// <summary>
    /// به صورت ناهمزمان یک موجودیت را بر اساس شناسه از دیتابیس بازیابی می‌کند.
    /// </summary>
    Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken);

    /// <summary>
    /// تمام موجودیت‌ها را به صورت ناهمزمان بازیابی می‌کند.
    /// </summary>
    IQueryable<TEntity> GetAllAsync(bool addAsNoTracking = true, bool isDeleted = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// تمام موجودیت‌ها را به همراه ویژگی‌های مرتبط مشخص شده بازیابی می‌کند.
    /// </summary>
    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);


    /// <summary>
    /// بررسی وجود موجودیت با شرط خاص.
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);

    /// <summary>
    /// موجودیت جدیدی را در دیتابیس درج می‌کند.
    /// </summary>
    void Insert(TEntity entity, bool isCommit = true);

    /// <summary>
    /// به صورت ناهمزمان موجودیت جدیدی را در دیتابیس درج می‌کند.
    /// </summary>
    Task InsertAsync(TEntity entity, bool isCommit = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// موجودیت موجود را به‌روزرسانی می‌کند.
    /// </summary>
    void Update(TEntity entity, bool isCommit = true);

    /// <summary>
    /// موجودیت را به عنوان حذف شده علامت‌گذاری می‌کند.
    /// </summary>
    void UpdateToDeleted(TEntity entity, bool isCommit = true);

    /// <summary>
    /// موجودیتی را بر اساس شناسه حذف می‌کند.
    /// </summary>
    void Delete(TId id, bool isCommit = true);

    /// <summary>
    /// موجودیت دریافت شده را از دیتابیس حذف می‌کند.
    /// </summary>
    void Delete(TEntity entity, bool isCommit = true);
}
