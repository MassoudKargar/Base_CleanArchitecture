
using Base.Application.BaseMediatR;
using Base.Core.Domains.Entities;
using Base.Sample.Application.Commands.Generics;
using Base.Sample.Application.Commands.Handlers.Generics.Contracts;
using Base.Sample.Application.RequestResponse.Responses;

namespace Base.Sample.Application.Commands.Handlers.Generics
{
    public abstract class GenericCreateCommandHandler<TId, TViewModel, TRequest, TResponse, TEntity>(IGenericRepository<TEntity, TId> service, IMapper mapper) :
    IGenericCreateCommandHandler<TId, TViewModel, TRequest, TResponse>
    where TId : struct
    where TViewModel : BaseDto<TViewModel, TEntity, TId>, new()
    where TRequest : GenericCreateCommand<TId, TViewModel, TResponse>
    where TEntity : BaseEntity<TId>, new()
    where TResponse : BaseCommandResult, new()
    {
        private IGenericRepository<TEntity, TId> Service { get; } = service;
        private IMapper Mapper { get; } = mapper;

        /// <summary>
        /// پردازش فرمان بر اساس نوع عملیات (درج، بروزرسانی، حذف)
        /// </summary>
        /// <param name="request">درخواست فرمان</param>
        /// <param name="cancellationToken">توکن لغو عملیات</param>
        /// <returns>پاسخ فرمان</returns>
        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            // پردازش عملیات درج
            var result = Mapper.Map<TViewModel, TEntity>(request.Model);
            await service.InsertAsync(result, cancellationToken: cancellationToken);
            return new TResponse();
        }
    }

}
