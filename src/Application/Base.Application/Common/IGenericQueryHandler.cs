using MediatR;

namespace Base.Application.Common;

public interface IGenericQueryHandler<TId, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : GenericQuery<TId, TResponse>
    where TId : struct
{

}