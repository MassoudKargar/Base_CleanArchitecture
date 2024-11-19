namespace Base.EndPoints.Web.Controllers;

interface IBaseHandler
{

}

public class ACommnad : CustomeRequest<int>
{

}

public class CustomeRequest<TResopnse> : IRequest<TResopnse>
{
    public Action action { get; set; }
}

public enum Action
{
    GetAll = 0,
}
public interface IGenericHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public class GetAllUser : BaseEntity<int>
{
    public int id { get; set; }
}

public class GetAllUseriewModl : BaseDto<GetAllUseriewModl, GetAllUser, int>
{

}
public class GetAllUsersCommand : CustomeRequest<List<int>>
{
    private readonly GetAllUseriewModl _request;

    public GetAllUsersCommand(GetAllUseriewModl request)
    {
        _request = request;
    }
}
public class GetAllUsersCommandHandler : AbstractGenericHandler<GetAllUsersCommand, List<int>, GetAllUseriewModl, GetAllUser, int>
{
    public GetAllUsersCommandHandler(IGenericService<GetAllUser, int> port, GetAllUseriewModl request) : base(port)
    {

    }
}

public abstract class AbstractGenericHandler<TRequest, TResponse, TModel, TEntity, TId> : IGenericHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TEntity : BaseEntity<TId>, new()
    where TModel : BaseDto<TModel, TEntity, TId>, new()
    where TId : struct

{
    private readonly IGenericService<TEntity, TId> _service;

    public AbstractGenericHandler(IGenericService<TEntity, TId> service)
    {
        _service = service;
    }
    public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var t = typeof(TModel);
        throw new NotImplementedException();
    }

}
internal class AHandler : IRequestHandler<ACommnad,int>
{
    public Task<int> Handle(ACommnad request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public class BaseHandler<TRequest> : IRequestHandler<TRequest, int> where TRequest : IRequest, IRequest<int>
{
    public Task<int> Handle(TRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public class GenericController<TEntity, TId, TListViewModel, TUpdateViewModel, TUpdateValidator, TInsertViewModel, TInsertValidator, TSelectViewModel>(
    IGenericService<TEntity, TId> service, ILogger logger) : BaseController
    where TEntity : BaseEntity<TId>, new()
    where TInsertViewModel : BaseDto<TInsertViewModel, TEntity, TId>, new()
    where TUpdateViewModel : BaseDto<TUpdateViewModel, TEntity, TId>, new()
    where TSelectViewModel : BaseDto<TSelectViewModel, TEntity, TId>, new()
    where TListViewModel : BaseDto<TListViewModel, TEntity, TId>, new()
    where TInsertValidator : AbstractValidator<TInsertViewModel>, new()
    where TUpdateValidator : AbstractValidator<TUpdateViewModel>, new()
    where TId : struct
{
    protected IMapper Mapper => HttpContext.MapperDispatcher();
    protected IMediator Mediator => HttpContext.MediatRDispatcher();
    [HttpGet]
    public Task<IEnumerable<TListViewModel>> GetAllAsync( CancellationToken cancellationToken)
    {
         Mediator.Send(new GetAllUsersCommand(new GetAllUseriewModl()));


        logger.LogInformation($"GetAll {typeof(TEntity).FullName}");
        logger.LogInformation(DateTime.Now.ToString());
        var baseResult = service.GetAll(true, cancellationToken);
        var result = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TListViewModel>>(baseResult);
        return Task.FromResult(result);
    }

    [HttpGet("{id}")]
    public async Task<TSelectViewModel> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Get {typeof(TEntity).Assembly.GetName().Name} => Id :{id}");
        var baseResult = await service.GetAsync(id, cancellationToken);
        var result = Mapper.Map<TEntity, TSelectViewModel>(baseResult);
        return result;
    }

    [HttpPost]
    public async Task AddAsync([FromBody] TInsertViewModel dto, CancellationToken cancellationToken)
    {
        await ValidatedAsync<TInsertValidator, TInsertViewModel>(dto, cancellationToken);
        logger.LogInformation($"Insert {typeof(TEntity).FullName}");
        var result = Mapper.Map<TInsertViewModel, TEntity>(dto);
        result.CreationDate = DateTime.Now;
        await service.InsertAsync(result, cancellationToken);
    }


    [HttpPost("{id}")]
    public async Task UpdateAsync(TId id, TUpdateViewModel dto, CancellationToken cancellationToken)
    {
        await ValidatedAsync<TUpdateValidator, TUpdateViewModel>(dto, cancellationToken);
        logger.LogInformation($"Update {typeof(TEntity).FullName} => Id :{id}");
        var result = Mapper.Map<TUpdateViewModel, TEntity>(dto);
        result.ModificationDate = DateTime.Now;
        await service.UpdateAsync(id, result, cancellationToken);
    }


    [HttpPost("{id}")]
    public Task DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Delete {typeof(TEntity).FullName} => Id :{id}");
        return service.DeleteAsync(id, cancellationToken);
    }

    [NonAction]
    private async Task ValidatedAsync<TValidator, TData>(TData abstractValidator, CancellationToken cancellationToken) where TValidator : AbstractValidator<TData>, new()
    {
        #region Validator
        var validator = new TValidator();
        var validationResult = await validator.ValidateAsync(abstractValidator, cancellationToken);
        if (validationResult.IsValid is false)
        {
            throw new SampleValidationException(validationResult);
        }
        #endregion
    }
}