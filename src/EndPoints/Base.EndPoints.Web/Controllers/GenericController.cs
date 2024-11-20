namespace Base.EndPoints.Web.Controllers;

public class GenericController<TEntity, TId, TListViewModel, TUpdateViewModel, TInsertViewModel, TSelectViewModel>(ILogger logger) : BaseController
    where TEntity : BaseEntity<TId>, new()
    where TInsertViewModel : BaseDto<TInsertViewModel, TEntity, TId>, new()
    where TUpdateViewModel : BaseDto<TUpdateViewModel, TEntity, TId>, new()
    where TSelectViewModel : BaseDto<TSelectViewModel, TEntity, TId>, new()
    where TListViewModel : BaseDto<TListViewModel, TEntity, TId>, new()
    where TId : struct
{
    protected IMediator Mediator => HttpContext.MediatRDispatcher();

    [HttpGet]
    public Task<IEnumerable<TListViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetAll {typeof(TEntity).FullName}");
        var result = Mediator.Send(new GenericQuery<TId, IEnumerable<TListViewModel>>(default, GenericAction.GetAll), cancellationToken);
        return result;
    }

    [HttpGet("{id}")]
    public Task<TSelectViewModel> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {

        logger.LogInformation($"GetById {typeof(TEntity).FullName}");
        var result = Mediator.Send(new GenericQuery<TId, TSelectViewModel>(id, GenericAction.GetById), cancellationToken);
        return result;
    }

    [HttpPost]
    public Task<Result> AddAsync([FromBody] TInsertViewModel dto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Insert {typeof(TEntity).FullName}");
        var result = Mediator.Send(new GenericCommand<TId, TInsertViewModel, Result>(default, dto, GenericAction.Insert), cancellationToken);
        return result;
    }


    [HttpPost("{id}")]
    public Task<Result> UpdateAsync(TId id, [FromBody] TUpdateViewModel dto, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Update {typeof(TEntity).FullName}");
        var result = Mediator.Send(new GenericCommand<TId, TUpdateViewModel, Result>(id, dto, GenericAction.Update), cancellationToken);
        return result;
    }


    [HttpPost("{id}")]
    public Task<Result> DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Delete {typeof(TEntity).FullName} => Id :{id}");
        var result = Mediator.Send(new GenericCommand<TId, Unit, Result>(id, default, GenericAction.Delete), cancellationToken);
        return result;
    }

}