using Heris.Application.BaseMediatR.Commands;

namespace Heris.Application.BaseMediatR.Handlers;

public abstract class AbstractGetAllQueryHandler<TId, TQuery, TRequest, TResponse, TEntity>(
    IGenericRepository<TEntity, TId> service,
    IMapper mapper) :
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
    public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
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

        if (getAllData != null)
        {
            var getAllResult = Mapper.Map<IQueryable<TEntity>, TResponse>(getAllData);
            return Task.FromResult(getAllResult);
        }
        return new Task<TResponse>(default!);
    }
}