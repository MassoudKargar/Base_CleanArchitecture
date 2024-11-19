namespace Base.EndPoints.Web.Controllers;

public class GenericController<TEntity, TId, TListViewModel, TUpdateViewModel, TUpdateValidator, TInsertViewModel, TInsertValidator, TSelectViewModel>(
    IGenericService<TEntity, TId> service, ILogger logger) : BaseController
    where TEntity : BaseEntity<TId>, new()
    where TInsertViewModel : BaseDto<TInsertViewModel, TEntity, TId>, new()
    where TUpdateViewModel : BaseDto<TUpdateViewModel, TEntity, TId>, new()
    where TSelectViewModel : BaseDto<TSelectViewModel, TEntity, TId>, new()
    where TListViewModel : BaseDto<TListViewModel,TEntity, TId>, new()
    where TInsertValidator : AbstractValidator<TInsertViewModel>, new()
    where TUpdateValidator : AbstractValidator<TUpdateViewModel>, new()
    where TId : struct
{
    protected IMapper Mapper => HttpContext.MapperDispatcher();
    [HttpGet]
    public Task<IEnumerable<TListViewModel>> GetAllAsync(CancellationToken cancellationToken)
    {
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