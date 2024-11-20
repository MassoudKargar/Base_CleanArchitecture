using MediatR;

namespace Base.Application.Common;

public interface IGenericCommandHandler<TId, TViewModel, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : GenericCommand<TId, TViewModel, TResponse>
    where TId : struct
{

}