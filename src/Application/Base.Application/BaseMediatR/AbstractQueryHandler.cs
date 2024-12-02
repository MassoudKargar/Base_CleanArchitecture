namespace Base.Application.BaseMediatR;

public abstract class AbstractQueryHandler<TId, TQuery, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
    IGenericQueryHandler<TId, TQuery, TRequest, TResponse>
    where TId : struct
    where TRequest : GenericQuery<TId, TQuery, TResponse>
    where TEntity : BaseEntity<TId>, new()
    where TResponse : class
{
    private IGenericRepository<TEntity, TId> Service { get; } = service;
    private IMapper Mapper { get; } = mapper;

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        switch (request.GenericActionData)
        {
            case GenericAction.GetAll:
                {
                    var tDataQuerySettings = new ODataQuerySettings();

                    var getAllData = Service.GetAllAsync(cancellationToken: cancellationToken);
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
                    var baseResult = await Service.GetAsync(request.Id, cancellationToken);
                    var result = Mapper.Map<TEntity, TResponse>(baseResult);
                    return result;
                }
            default:
                return default;
        }
    }
}