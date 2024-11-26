namespace Base.Application.BaseMediatR;

public class GenericQuery<TId, TQuery, TResponse>(TId id, ODataQueryOptions<TQuery>? queryOptions, GenericAction genericAction) : IRequest<TResponse>
    where TId : struct
{
    public TId Id { get; } = id;
    public ODataQueryOptions<TQuery>? QueryOptions { get; } = queryOptions;
    public GenericAction GenericActionData { get; } = genericAction;
}