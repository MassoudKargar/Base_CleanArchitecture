using Base.Utility.Exceptions;

namespace Base.Application.BaseMediatR;
/// <summary>
/// کلاس پایه برای پردازش فرمان‌ها
/// این کلاس برای انجام عملیات‌های مختلف مانند درج، بروزرسانی و حذف در یک موجودیت خاص استفاده می‌شود.
/// </summary>
/// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
/// <typeparam name="TViewModel">نوع مدل نمایش داده‌شده</typeparam>
/// <typeparam name="TRequest">نوع درخواست فرمان</typeparam>
/// <typeparam name="TResponse">نوع پاسخ فرمان</typeparam>
/// <typeparam name="TEntity">نوع موجودیت</typeparam>
public abstract class AbstractCommandHandler<TId, TViewModel, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
    IGenericCommandHandler<TId, TViewModel, TRequest, TResponse>
    where TId : struct
    where TViewModel : BaseDto<TViewModel, TEntity, TId>, new()
    where TRequest : GenericCommand<TId, TViewModel, TResponse>
    where TEntity : BaseEntity<TId>, new()
{
    private IGenericRepository<TEntity, TId> Service { get; } = service;
    private IMapper Mapper { get; } = mapper;

    /// <summary>
    /// پردازش فرمان بر اساس نوع عملیات (درج، بروزرسانی، حذف)
    /// </summary>
    /// <param name="request">درخواست فرمان</param>
    /// <param name="cancellationToken">توکن لغو عملیات</param>
    /// <returns>پاسخ فرمان</returns>
    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        switch (request.GenericActionData)
        {
            case GenericAction.Insert:
                {
                    // پردازش عملیات درج
                    var result = Mapper.Map<TViewModel, TEntity>(request.Model);
                    await service.InsertAsync(result, cancellationToken: cancellationToken);
                    return default;
                }
            case GenericAction.Update:
                {
                    // پردازش عملیات بروزرسانی
                    var result = Mapper.Map<TViewModel, TEntity>(request.Model);
                    result.Id = request.Id;
                    service.Update(result);
                    return default;
                }
            case GenericAction.Delete:
                {
                    // پردازش عملیات حذف
                    var result = await Service.GetAsync(request.Id, cancellationToken: cancellationToken);
                    if (result is null)
                    {
                        throw new NotFoundException();
                    }
                    service.UpdateToDeleted(result);
                    return default;
                }
            default:
                return default;
        }
    }
}
