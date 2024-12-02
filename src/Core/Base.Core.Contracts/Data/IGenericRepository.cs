namespace Base.Core.Contracts.Data;

public interface IGenericRepository<TEntity,in TId>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    #region Get
    /// <summary>
    /// یک شی را با شناسه از دیتابیس یافته و بازگشت می‌دهد.
    /// </summary>
    /// <param name="id">شناسه شی مورد نیاز</param>
    /// <returns>نمونه ساخته شده از شی</returns>
    TEntity Get(TId id);
    Task<TEntity> GetAsync(TId id, CancellationToken cancellationToken);
    #endregion

    #region Exists
    Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistAsync<TId>(TId id, CancellationToken cancellationToken);
    Task<bool> ExistAsync(long id, CancellationToken cancellationToken);
    Task<bool> ExistAsync(int id, CancellationToken cancellationToken);
    bool Exists(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
    #endregion

    IQueryable<TEntity> GetAllAsync(bool addAsNoTracking = true, bool isDeleted = true, CancellationToken cancellationToken = default);
    #region Insert

    /// <summary>
    /// داده‌های جدید را به دیتابیس اضافه می‌کند
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>
    /// <param name="isCommit">صدا زده شود یا نه  SaveChange مشخص میکند که آیا متود</param>
    void Insert(TEntity entity, bool isCommit = true);

    /// <summary>
    /// داده‌های جدید را به دیتابیس اضافه می‌کند
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>
    /// <param name="isCommit">صدا زده شود یا نه  SaveChange مشخص میکند که آیا متود</param>
    Task InsertAsync(TEntity entity, bool isCommit = true, CancellationToken cancellationToken = default);
    #endregion

    #region Update

    /// <summary>
    /// داده‌ را در دیتابیس تغیر میدهد 
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید در دیتابیس تغیر کند.</param>
    /// <param name="isCommit">صدا زده شود یا نه  SaveChange مشخص میکند که آیا متود</param>
    void Update(TEntity entity, bool isCommit = true);


    /// <summary>
    /// داده‌ را در دیتابیس به وضعیت خذف شده تغیر میدهد 
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید در دیتابیس تغیر کند.</param>
    /// <param name="isCommit">صدا زده شود یا نه  SaveChange مشخص میکند که آیا متود</param>
    void UpdateToDeleted(TEntity entity, bool isCommit = true);

    #endregion

    #region Delete

    /// <summary>
    /// یک شی را با شناسه حذف می کند
    /// </summary>
    /// <param name="id">شناسه</param>
    /// <param name="isCommit">صدا زده شود یا نه  SaveChange مشخص میکند که آیا متود</param>
    void Delete(TId id, bool isCommit = true);

    ///// <summary>
    ///// حذف یک شی به همراه تمامی فرزندان آن را انجام می دهد
    ///// </summary>
    ///// <param name="id">شناسه</param>
    //void DeleteGraph(TId id, bool isCommit = true);

    /// <summary>
    /// یک شی را دریافت کرده و از دیتابیس حذف می‌کند
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید از دیتابیس حذف شود.</param>
    /// <param name="isCommit">صدا زده شود یا نه  SaveChange مشخص میکند که آیا متود</param>
    void Delete(TEntity entity, bool isCommit = true);
    #endregion

    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
}
