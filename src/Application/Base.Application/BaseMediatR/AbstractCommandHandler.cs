using Base.Utility.Exceptions;

namespace Base.Application.BaseMediatR;

public abstract class AbstractCommandHandler<TId, TViewModel, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
    IGenericCommandHandler<TId, TViewModel, TRequest, TResponse>
    where TId : struct
    where TViewModel : BaseDto<TViewModel, TEntity, TId>, new()
    where TRequest : GenericCommand<TId, TViewModel, TResponse>
    where TEntity : BaseEntity<TId>, new()
{
    private IGenericRepository<TEntity, TId> Service { get; } = service;
    private IMapper Mapper { get; } = mapper;

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        switch (request.GenericActionData)
        {
            case GenericAction.Insert:
                {
                    var result = Mapper.Map<TViewModel, TEntity>(request.Model);
                    await service.InsertAsync(result, cancellationToken: cancellationToken);
                    return default;
                }
            case GenericAction.Update:
                {
                    var result = Mapper.Map<TViewModel, TEntity>(request.Model);
                    result.Id = request.Id;
                    service.Update(result);
                    return default;
                }
            case GenericAction.Delete:
                {
                    var result = await Service.GetAsync(request.Id, cancellationToken);
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