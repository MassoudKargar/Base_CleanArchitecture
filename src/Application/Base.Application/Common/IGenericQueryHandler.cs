using MediatR;

namespace Base.Application.Common;

public interface IGenericQueryHandler<TId, TQuery, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : GenericQuery<TId, TQuery, TResponse>
    where TId : struct
    where TResponse : class
{

}