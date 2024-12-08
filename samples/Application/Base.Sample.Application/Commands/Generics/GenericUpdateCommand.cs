using MediatR;

namespace Base.Sample.Application.Commands.Generics
{
    public class GenericUpdateCommand<TId, TViewModel, TResponse> : IRequest<TResponse>
        where TId : struct
    {
        public TId Id { get; }

        public TViewModel Model { get; }

        public GenericUpdateCommand(TId id, TViewModel model)
        {
            Id = id;
            Model = model;
        }
    }
}
