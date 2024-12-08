using Base.Application.BaseMediatR;
using Base.Application.Commands.Generics;
using Base.Application.Commands.Handlers.Contracts;
using Base.Utility.Exceptions;

public abstract class GenericDeleteCommandHandler<TId, TViewModel, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
    IGenericDeleteCommandHandler<TId, TViewModel, TRequest, TResponse>
    where TId : struct
    where TViewModel : BaseDto<TViewModel, TEntity, TId>, new()
    where TRequest : GenericDeleteCommand<TId, TViewModel, TResponse>
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

        var result = await Service.GetAsync(request.Id, cancellationToken: cancellationToken);

        if (result is null)
        {
            throw new NotFoundException();
        }

        service.UpdateToDeleted(result);
        return default;
    }
}