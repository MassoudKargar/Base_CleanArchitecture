namespace Base.Application.BaseMediatR;

/// <summary>
/// کلاس پایه برای پردازش کوئری‌ها
/// این کلاس برای انجام عملیات‌های مختلف مانند دریافت تمام داده‌ها یا دریافت اطلاعات یک موجودیت خاص بر اساس شناسه استفاده می‌شود.
/// </summary>
/// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
/// <typeparam name="TQuery">نوع کوئری</typeparam>
/// <typeparam name="TRequest">نوع درخواست کوئری</typeparam>
/// <typeparam name="TResponse">نوع پاسخ کوئری</typeparam>
/// <typeparam name="TEntity">نوع موجودیت</typeparam>
public abstract class AbstractQueryHandler<TId, TQuery, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
    IGenericQueryHandler<TId, TQuery, TRequest, TResponse>
    where TId : struct
    where TRequest : GenericQuery<TId, TQuery, TResponse>
    where TEntity : BaseEntity<TId>, new()
    where TResponse : class
{
    private IGenericRepository<TEntity, TId> Service { get; } = service;
    private IMapper Mapper { get; } = mapper;

    /// <summary>
    /// پردازش درخواست کوئری بر اساس نوع عملیات (دریافت تمام داده‌ها یا دریافت اطلاعات بر اساس شناسه)
    /// </summary>
    /// <param name="request">درخواست کوئری</param>
    /// <param name="cancellationToken">توکن لغو عملیات</param>
    /// <returns>پاسخ کوئری</returns>
    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        switch (request.GenericActionData)
        {
            case GenericAction.GetAll:
                {
                    // پردازش درخواست برای دریافت تمام داده‌ها
                    var tDataQuerySettings = new ODataQuerySettings();

                    var getAllData = Service.GetAllAsync(cancellationToken: cancellationToken);

                    // اعمال فیلترها و تنظیمات مختلف بر روی داده‌ها
                    if (request.QueryOptions?.OrderBy != null)
                    {
                        getAllData = request.QueryOptions.OrderBy.ApplyTo(getAllData);
                    }
                    if (request.QueryOptions?.Skip != null)
                    {
                        getAllData = request.QueryOptions.Skip.ApplyTo(getAllData, tDataQuerySettings);
                    }
                    if (request.QueryOptions?.Top != null)
                    {
                        getAllData = request.QueryOptions.Top.ApplyTo(getAllData, tDataQuerySettings);
                    }
                    if (request.QueryOptions?.Filter != null)
                    {
                        getAllData = request.QueryOptions.Filter.ApplyTo(getAllData, tDataQuerySettings) as IQueryable<TEntity>;
                    }

                    var getAllResult = Mapper.Map<IQueryable<TEntity>, TResponse>(getAllData);
                    return getAllResult;
                }
            case GenericAction.GetById:
                {
                    // پردازش درخواست برای دریافت یک موجودیت خاص بر اساس شناسه
                    var baseResult = await Service.GetAsync(request.Id, cancellationToken: cancellationToken);
                    var result = Mapper.Map<TEntity, TResponse>(baseResult);
                    return result;
                }
            default:
                return default;
        }
    }
}
