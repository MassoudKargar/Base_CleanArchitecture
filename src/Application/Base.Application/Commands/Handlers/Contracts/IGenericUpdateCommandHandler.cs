using MediatR;

namespace Base.Application.Commands.Handlers.Contracts
{
    public interface IGenericUpdateCommandHandler<TId, TViewModel, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : Base.Application.Commands.Generics.GenericUpdateCommand<TId, TViewModel, TResponse>
        where TId : struct
    {
    }
}
