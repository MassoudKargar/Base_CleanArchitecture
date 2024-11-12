using Base.Core.Domains.Entities;
using Base.Core.RequestResponse.Common;
using Base.Core.RequestResponse.Endpoints;
using Base.Extensions.Logger.Abstractions;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Base.EndPoints.Web.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class BaseController : Controller
{
    protected BaseServices BaseServices => HttpContext.BaseApplicationContext();

}
public class BaseGenericController<TEntity,TId, TDto>(
    IValidator<TDto> validator,
    ILogger logger,
    IGenericRepository<TEntity,TId> repository) 
    : BaseController
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    [HttpGet]
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);
    }
    [HttpGet("{id}")]
    public virtual async Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await repository.GetAsync(id, cancellationToken);
    }
    [HttpPost]
    public virtual async Task<BaseResult<TEntity>> AddAsync(TDto dto, CancellationToken cancellationToken)
    {
        var result = Validate<TDto, BaseResult<TEntity>>(dto, validator);
        TEntity e = BaseServices.MapperFacade.Map<TDto, TEntity>(dto);
        await repository.InsertAsync(e, cancellationToken);
        await repository.CommitAsync();
        result._data = e;
        return result;
    }
    

    [HttpPut("{id}")]
    public virtual async Task<CommandResult> UpdateAsync(TId id, TDto dto, CancellationToken cancellationToken)
    {
        var result = Validate<TDto, CommandResult>(dto, validator);
        if (!await repository.ExistAsync(id, cancellationToken))
        {
            throw new NullReferenceException();
        }

        TEntity entity = BaseServices.MapperFacade.Map<TDto, TEntity>(dto);
        entity.Id = id;
        repository.Update(entity);
        repository.Commit();
        return result;
    }


    [HttpDelete("{id}")]
    public virtual async Task DeleteAsync(TId id, CancellationToken cancellationToken)
    {
        var profile = await repository.GetAsync(id, cancellationToken);
        if (profile == null)
        {
            throw new NullReferenceException();
        }
        repository.Delete(profile);
        repository.Commit();
    }

    private TValidationResult Validate<TDto, TValidationResult>(TDto entity, IValidator<TDto> validator) 
        where TValidationResult : ApplicationServiceResult, new()
    {
        TValidationResult res = null;

        if (validator != null)
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                res = new()
                {
                    Status = ApplicationServiceStatus.ValidationError
                };
                throw new ValidationException("",validationResult.Errors);
            }
        }
        else
        {
            logger.LogInformation(BaseEventId.CommandValidation, "There is not any validator for {CommandType}", entity.GetType());
        }
        return res;
    }
}