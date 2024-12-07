using Base.Application.BaseMediatR;

public abstract class AbstractUpdateCommandHandler<TId, TViewModel, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
    IGenericUpdateCommandHandler<TId, TViewModel, TRequest, TResponse>
    where TId : struct
    where TViewModel : BaseDto<TViewModel, TEntity, TId>, new()
    where TRequest : GenericUpdateCommand<TId, TViewModel, TResponse>
    where TEntity : BaseEntity<TId>, new()
{
    private IGenericRepository<TEntity, TId> Service { get; } = service;
    private IMapper Mapper { get; } = mapper;

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {

        var model = Mapper.Map<TViewModel, TEntity>(request.Model);

        model.Id = request.Id;

        service.Update(model);

        return default;
    }
}
