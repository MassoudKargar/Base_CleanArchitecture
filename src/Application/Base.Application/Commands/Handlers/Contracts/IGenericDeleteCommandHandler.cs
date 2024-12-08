namespace Base.Application.Commands.Handlers.Contracts
{
    public interface IGenericDeleteCommandHandler<TId, TViewModel, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : Base.Application.Commands.Generics.GenericDeleteCommand<TId, TViewModel, TResponse>
        where TId : struct
    {
    }

}
