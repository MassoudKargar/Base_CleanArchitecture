namespace Base.Application.BaseMediatR;

public class GenericCommand<TId, TViewModel, TResponse>(TId id, TViewModel model, GenericAction genericAction) : IRequest<TResponse>
    where TId : struct
{
    public TId Id { get; } = id;
    public TViewModel Model { get; } = model;
    public GenericAction GenericActionData { get; } = genericAction;
}