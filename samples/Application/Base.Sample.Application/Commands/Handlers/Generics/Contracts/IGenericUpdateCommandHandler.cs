using Base.Sample.Application.Commands.Generics;
using MediatR;

namespace Base.Sample.Application.Commands.Handlers.Generics.Contracts
{
    public interface IGenericUpdateCommandHandler<TId, TViewModel, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : GenericUpdateCommand<TId, TViewModel, TResponse>
        where TId : struct
    {
    }
}
