using Heris.Application.BaseMediatR.Commands;

namespace Heris.Application.BaseMediatR.Handlers;

/// <summary>
/// کلاس پایه برای پردازش کوئری‌ها
/// این کلاس برای انجام عملیات‌های مختلف مانند دریافت تمام داده‌ها یا دریافت اطلاعات یک موجودیت خاص بر اساس شناسه استفاده می‌شود.
/// </summary>
/// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
/// <typeparam name="TQuery">نوع کوئری</typeparam>
/// <typeparam name="TRequest">نوع درخواست کوئری</typeparam>
/// <typeparam name="TResponse">نوع پاسخ کوئری</typeparam>
/// <typeparam name="TEntity">نوع موجودیت</typeparam>
public abstract class AbstractGetByIdQueryHandler<TId, TQuery, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
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
        // پردازش درخواست برای دریافت یک موجودیت خاص بر اساس شناسه
        var baseResult = await Service.GetAsync(request.Id, cancellationToken: cancellationToken);
        var result = Mapper.Map<TEntity, TResponse>(baseResult);
        return result;
    }
}