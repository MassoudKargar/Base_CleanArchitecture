using Heris.Application.BaseMediatR.Commands;
using Heris.Application.BaseMediatR.Handlers;

namespace Heris.Application.BaseMediatR.Handlers;

/// <summary>
/// کلاس پایه برای پردازش فرمان‌ها
/// این کلاس برای انجام عملیات‌های مختلف مانند درج، بروزرسانی و حذف در یک موجودیت خاص استفاده می‌شود.
/// </summary>
/// <typeparam name="TId">نوع شناسه موجودیت</typeparam>
/// <typeparam name="TViewModel">نوع مدل نمایش داده‌شده</typeparam>
/// <typeparam name="TRequest">نوع درخواست فرمان</typeparam>
/// <typeparam name="TEntity">نوع موجودیت</typeparam>
/// <typeparam name="TResponse">خروجی</typeparam>
public abstract class AbstractInsertCommandHandler<TId, TViewModel, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> genericRepository, IMapper mapper) :
    IGenericCommandHandler<TId, TViewModel, TRequest, TResponse>
    where TId : struct
    where TViewModel : BaseDto<TViewModel, TEntity, TId>, new()
    where TRequest : GenericCommand<TId, TViewModel, TResponse>
    where TEntity : BaseEntity<TId>, new()
{
    private IGenericRepository<TEntity, TId> GenericRepository { get; } = genericRepository;
    private IMapper Mapper { get; } = mapper;

    /// <summary>
    /// پردازش فرمان بر اساس نوع عملیات (درج، بروزرسانی، حذف)
    /// </summary>
    /// <param name="request">درخواست فرمان</param>
    /// <param name="cancellationToken">توکن لغو عملیات</param>
    /// <returns>پاسخ فرمان</returns>
    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        // پردازش عملیات درج
        var entity = request.Model.ToEntity(Mapper);
        await GenericRepository.InsertAsync(entity, cancellationToken: cancellationToken);
        var result = await GenericRepository.GetAsync(entity.Id, cancellationToken);
        return Mapper.Map<TEntity, TResponse>(result);
    }
}