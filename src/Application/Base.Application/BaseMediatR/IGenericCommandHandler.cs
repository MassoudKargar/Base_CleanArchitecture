namespace Base.Application.BaseMediatR;

public interface IGenericCommandHandler<TId, TViewModel, in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : GenericCommand<TId, TViewModel, TResponse>
    where TId : struct;