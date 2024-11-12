using Base.Core.Domains.Entities;

namespace Base.EndPoints.Web.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class BaseController : Controller
{
    protected BaseServices BaseServices => HttpContext.BaseApplicationContext();

}
public class BaseGenericController<TEntity,TId, TDto>(IGenericRepository<TEntity,TId> repository) : BaseController
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    [HttpGet]
    public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);
    }
    [HttpGet("{id}")]
    public virtual async Task<TEntity> GetById(TId id, CancellationToken cancellationToken)
    {
        return await repository.GetAsync(id, cancellationToken);
    }
    [HttpPost]
    public virtual async Task<TEntity> Add(TDto dto, CancellationToken cancellationToken)
    {
        TEntity e = BaseServices.MapperFacade.Map<TDto, TEntity>(dto);
        await repository.InsertAsync(e, cancellationToken);
        await repository.CommitAsync();
        return e;
    }
    

    [HttpPut("{id}")]
    public virtual async Task Update(TId id, TDto dto, CancellationToken cancellationToken)
    {
        if (!await repository.ExistAsync(id, cancellationToken))
        {
            throw new NullReferenceException();
        }
        TEntity entity = BaseServices.MapperFacade.Map<TDto, TEntity>(dto);
        entity.Id = id;
        repository.Update(entity);
        repository.Commit();
    }


    [HttpDelete("{id}")]
    public virtual async Task Delete(TId id, CancellationToken cancellationToken)
    {
        var profile = await repository.GetAsync(id, cancellationToken);
        if (profile == null)
        {
            throw new NullReferenceException();
        }
        repository.Delete(profile);
        repository.Commit();
    }
}