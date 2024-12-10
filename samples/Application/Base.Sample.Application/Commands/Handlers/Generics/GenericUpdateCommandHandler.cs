
using Base.Core.Domains.Entities;
using Base.Sample.Application.Commands.Generics;
using Base.Sample.Application.Commands.Handlers.Generics.Contracts;
using Base.Sample.Application.RequestResponse.Responses;

namespace Base.Sample.Application.Commands.Handlers.Generics
{
    public abstract class GenericUpdateCommandHandler<TId, TViewModel, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
        IGenericUpdateCommandHandler<TId, TViewModel, TRequest, TResponse>
        where TId : struct
        where TViewModel : BaseDto<TViewModel, TEntity, TId>, new()
        where TRequest : GenericUpdateCommand<TId, TViewModel, TResponse>
        where TEntity : BaseEntity<TId>, new()
        where TResponse : BaseCommandResult, new()

    {
        private IGenericRepository<TEntity, TId> Service { get; } = service;
        private IMapper Mapper { get; } = mapper;

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var model = Mapper.Map<TViewModel, TEntity>(request.Model);

            model.Id = request.Id;

            service.Update(model);

            return new TResponse();
        }
    }

}
