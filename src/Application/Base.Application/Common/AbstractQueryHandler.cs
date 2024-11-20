namespace Base.Application.Common;

public abstract class AbstractQueryHandler<TId, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
    IGenericQueryHandler<TId,TRequest, TResponse>
    where TId : struct
    where TRequest : GenericQuery<TId, TResponse>
    where TEntity : BaseEntity<TId>, new()
{
    private IGenericRepository<TEntity, TId> Service { get; } = service;
    private IMapper Mapper { get; } = mapper;

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        switch (request.GenericActionData)
        {
            case GenericAction.GetAll:
            {
                var getAllData = Service.GetAllAsync(true, cancellationToken);
                var getAllResult = Mapper.Map<IQueryable<TEntity>, TResponse>(getAllData);
                return getAllResult;
            }
            case GenericAction.GetById:
            {
                var baseResult = await Service.GetAsync(request.Id, cancellationToken);
                var result = Mapper.Map<TEntity, TResponse>(baseResult);
                return result;
            }
            default:
                return default;
        }
    }
}