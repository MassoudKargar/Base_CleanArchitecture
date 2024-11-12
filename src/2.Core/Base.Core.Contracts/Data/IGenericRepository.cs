namespace Base.Core.Contracts.Data;

public interface IGenericRepository<TEntity, in TId> : IUnitOfWork
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
    Task<TEntity> GetAsync(TId id);
    #endregion

    #region Exists
    bool Exists(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    #endregion


    #region Insert
    /// <summary>
    /// داده‌های جدید را به دیتابیس اضافه می‌کند
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>
    void Insert(TEntity entity);

    /// <summary>
    /// داده‌های جدید را به دیتابیس اضافه می‌کند
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید به دیتابیس اضافه شود.</param>
    Task InsertAsync(TEntity entity);
    #endregion

    #region Update
    /// <summary>
    /// داده‌ را در دیتابیس تغیر میدهد 
    /// </summary>
    /// <param name="entity">نمونه داده‌ای که باید در دیتابیس تغیر کند.</param>
    void Update(TEntity entity);

    #endregion

    #region Delete
    /// <summary>
    /// یک شی را با شناسه حذف می کند
    /// </summary>
    /// <param name="id">شناسه</param>
    void Delete(TId id);

    /// <summary>
    /// حذف یک شی به همراه تمامی فرزندان آن را انجام می دهد
    /// </summary>
    /// <param name="id">شناسه</param>
    void DeleteGraph(TId id);

    /// <summary>
    /// یک شی را دریافت کرده و از دیتابیس حذف می‌کند
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);
    #endregion
}
